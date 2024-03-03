import { Injectable } from '@angular/core';
import { UntypedFormGroup, UntypedFormControl, UntypedFormBuilder, AbstractControl, UntypedFormArray, AsyncValidatorFn, ValidatorFn } from '@angular/forms';

export type FormGroupControlsOf<T> = {
    [P in keyof T]: UntypedFormControl | UntypedFormGroup | UntypedFormArray; 
};

export abstract class FormGroupTypeSafe<T> extends UntypedFormGroup {
    //give the value a custom type s
    override value: T | null = null;
    keys: T | null = null;

    //create helper methods to achieve this syntax eg: this.form.getSafe(x => x.heroName).patchValue('Himan')
    public abstract getSafe(propertyFunction: (typeVal: T) => any): AbstractControl;
    public abstract setControlSafe(propertyFunction: (typeVal: T) => any, control: AbstractControl): void;
    //If you need more function implement declare them here but implement them on FormBuilderTypeSafe.group instantiation.

}

export class FormControlTypeSafe<T> extends UntypedFormControl {
    override value: T | null = null;
    keys: T | null = null;
}

export class FormArrayTypeSafe<T> extends UntypedFormArray {
    override value: T[] = [];
    public pushSafe(control: FormControlTypeSafe<T>){
        return super.push(control);
    }
}

@Injectable()
export class FormBuilderTypeSafe extends UntypedFormBuilder {
    //override group to be type safe
    
    override control<T>(formState: T, validator?: ValidatorFn | ValidatorFn[] | null, asyncValidator?: AsyncValidatorFn | AsyncValidatorFn[] | null): FormControlTypeSafe<T>{
        let control = new FormControlTypeSafe<T>(formState, validator, asyncValidator);
        let obj: any = {} as T;
        Object.keys(control).forEach(
            x => {
                obj[x] = x;
            }
        );
        control.keys = obj;
        return control;
    }

    override array<T>(controlsConfig: FormControlTypeSafe<T>[] | FormGroupTypeSafe<T>[], validator?: ValidatorFn | ValidatorFn[] | null, asyncValidator?: AsyncValidatorFn | AsyncValidatorFn[] | null): FormArrayTypeSafe<T> {
        return new FormArrayTypeSafe<T>(controlsConfig, validator,asyncValidator);
    }

    override group<T>(controlsConfig: FormGroupControlsOf<T>, extra?: {
        [key: string]: any;
    } | null): FormGroupTypeSafe<T> {/*NOTE the return FormGroupTypeSafe<T> */
        
        //instantiate group from angular type
        let gr = super.group(controlsConfig, extra) as FormGroupTypeSafe<T>;

        
        if (gr) {
            //implement getSafe method
            gr.getSafe = (propertyFunction: (typeVal: T) => any): AbstractControl => {
                let getStr = getPropertyName(propertyFunction);
                let p = gr.get(getStr) as UntypedFormGroup;
                return p;    
            }
          
            //implement setControlSafe 
            gr.setControlSafe = (propertyFunction: (typeVal: T) => any, control: AbstractControl): void => {
                let getStr = getPropertyName(propertyFunction);
                gr.setControl(getStr, control);
            }
            let obj: any = {} as T;
            Object.keys(controlsConfig).forEach(
                x => {
                    obj[x] = x;
                }
            );
            gr.keys = obj;
            
            //implement more functions as needed
           
        }

        return gr;
    }
}

export function getPropertyName(propertyFunction: Function): string {
    let properties: RegExpMatchArray | null = propertyFunction.toString().match(/(?![.])*([A-z0-9_]+)(?=>[};.])*/gi);
    var r = properties ? properties[properties.length-1] : "";                                    
    return r;
}