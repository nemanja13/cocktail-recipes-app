import { Component, OnDestroy, OnInit } from '@angular/core';
import { RecipesRequestsService } from '../../services/requests/recipes-requests.service';
import { IRecipeDropdownData, IRecipeForm, IRecipeRequest } from '../../interfaces/i-recipe';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { IResponse } from 'src/app/shared/interfaces/i-response';
import { IType } from 'src/app/pages/client-pages/components/home/interfaces/i-type';
import { IMeasure } from '../../interfaces/i-measure';
import { IIngredient } from '../../interfaces/i-ingredient';
import { FormBuilderTypeSafe, FormGroupTypeSafe } from 'src/app/shared/helper/reactive-forms-helper';
import { FormArray, FormControl, Validators } from '@angular/forms';
declare let alertify: any;

@Component({
  selector: 'app-recipes-form',
  templateUrl: './recipes-form.component.html',
  styleUrls: ['./recipes-form.component.css']
})
export class RecipesFormComponent implements OnInit, OnDestroy {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private requestsService: RecipesRequestsService,
    private formBuilder: FormBuilderTypeSafe
  ) { }

  private subscription: Subscription = new Subscription();
  private id: number | null = null;

  public form: FormGroupTypeSafe<IRecipeForm> = this.initForm();
  public recipe: IRecipeForm | null = null;
  public isEdit: boolean = false;
  private ingredients: number[] = [];
  public dropdownData: IRecipeDropdownData = {
    Types: [],
    Ingredients: [],
    Measures: []
  };

  ngOnInit(): void {
    this.form = this.initForm();
    let routeParams = this.route.snapshot.paramMap;
    this.id = Number(routeParams.get('id'));

    this.getDataFromAllRequests(this.id);
  }

  getDataFromAllRequests(id?: number): void {
    this.subscription.add(
      this.requestsService.getDataFromAllRequests(id).subscribe({
        next: (data: any) => {
          this.dropdownData.Types = (data[0] as IResponse<IType>).data;
          this.dropdownData.Measures = (data[1] as IResponse<IMeasure>).data;
          this.dropdownData.Ingredients = (data[2] as IResponse<IIngredient>).data;

          this.recipe = (data[3] as IRecipeForm);
          if(this.recipe) {
            this.isEdit = true;
            this.setName(this.recipe.name);
            this.setImage(this.recipe.image);
            this.setInstructions(this.recipe.instructions);
            this.setType(this.recipe.typeId);
            this.setMeasure(this.recipe.measureId);
            this.setIngredients(this.dropdownData.Ingredients, this.recipe.ingredientIds);
          } else {
            this.form = this.initForm();
            this.isEdit = false;
            this.setIngredients(this.dropdownData.Ingredients);
          }
        }
      })
    );
  }
  
  initForm(): FormGroupTypeSafe<IRecipeForm> {
    return this.formBuilder.group<IRecipeForm>({
      name: this.formBuilder.control('', [Validators.required]),
      image: this.formBuilder.control(''),
      instructions: this.formBuilder.control('', [Validators.required]),
      typeId: this.formBuilder.control(null, [Validators.required, Validators.min(1)]),
      measureId: this.formBuilder.control(null, [Validators.required, Validators.min(1)]),
      ingredientIds: this.formBuilder.array([], [Validators.required])
    });
  }

  setName(name: string): void {
    this.form.getSafe(x => x.name).setValue(name);
  }

  setImage(image: string): void {
    this.form.getSafe(x => x.image).setValue(image);
  }
  
  setInstructions(instructions: string): void {
    this.form.getSafe(x => x.instructions).setValue(instructions);
  }

  setMeasure(measureId: number): void {
    this.form.getSafe(x => x.measureId).setValue(measureId);
  }

  setType(typeId: number): void {
    this.form.getSafe(x => x.typeId).setValue(typeId);
  }

  setIngredients(ingredients: any[], selectedIngredients?: number[]){
    let ingredientControl: FormControl = new FormControl(false);
    this.ingredients = ingredients.map(x => x.id);
    let formArray: FormArray = new FormArray<any>([]);
    for (let i = 0; i < ingredients.length; i++) {

      if (selectedIngredients) {
        if (selectedIngredients && selectedIngredients.includes(ingredients[i].id)) {
          ingredientControl = new FormControl(true);
        } else {
          ingredientControl = new FormControl(false);
        }
      }
      else {
        ingredientControl = new FormControl(false);
      }

      formArray.push(ingredientControl);
      this.form.setControlSafe(x => x.ingredientIds, formArray);
    }
  }

  submit(): void {
    if(this.form.getSafe(x => x.name).hasError('required')){
      alertify.error("Name is required");
      return;
    }
    if(!this.form.getSafe(x => x.image).value && !this.isEdit){
      alertify.error("Image is required");
      return;
    }
    if(this.form.getSafe(x => x.instructions).hasError('required')){
      alertify.error("Instructions are required");
      return;
    }
    if(this.form.getSafe(x => x.typeId).hasError('required') || this.form.getSafe(x => x.typeId).hasError('min')){
      alertify.error("Type is required");
      return;
    }
    if(this.form.getSafe(x => x.measureId).hasError('required') || this.form.getSafe(x => x.measureId).hasError('min')){
      alertify.error("Measure is required");
      return;
    }
    console.log(this.form.getSafe(x => x.ingredientIds).value)
    if(!(this.form.getSafe(x => x.ingredientIds).value as boolean[]).some(x => x)){
      alertify.error("At least one ingredient is required");
      return;
    }
    
    let dataToSend = this.prepareDataToSend();
    
    if(this.isEdit && this.id){
      this.requestsService.update(this.id, dataToSend).subscribe({
        next: data => {
          alertify.success("You have successfully changed the recipe");
          this.isEdit = false;
          this.router.navigateByUrl("/admin");
        },
        error: error => {
          if(error.errors){
            alertify.error(error.errors[0].ErrorMessage);
          }
        }
      });
    }else{
      this.requestsService.create(dataToSend).subscribe({
        next: data => {
          alertify.success("You have successfully added a recipe");
          this.router.navigateByUrl("/admin");
        },
        error: error => {
          if(error.errors){
            alertify.error(error.errors[0].ErrorMessage);
          }
        }
      });
    }
  }

  prepareDataToSend(): IRecipeRequest {
    let formValue: IRecipeForm = this.form.value as IRecipeForm;
    let selectedIngredients = this.ingredients.filter((value, index) => formValue.ingredientIds[index]);

    const dataToSend: IRecipeRequest = {
      name: formValue.name,
      image: formValue.image,
      instructions: formValue.instructions,
      typeId: formValue.typeId,
      measureId: formValue.measureId,
      ingredientIds: selectedIngredients
    };

    return dataToSend;
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
