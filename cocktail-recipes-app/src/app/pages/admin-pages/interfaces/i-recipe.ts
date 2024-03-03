import { IOption } from "src/app/shared/interfaces/i-option";

export interface IRecipe {
    id: number;
    name: string;
    image: string;
    instructions: string;
    ingredients: string;
    type: string;
    measure: string;
}

export interface IRecipeForm {
    name: string;
    image: string;
    instructions: string;
    ingredientIds: number[];
    typeId: number;
    measureId: number;
}

export interface IRecipeRequest extends IRecipeForm {
}

export interface IRecipeDropdownData {
    Types: IOption[];
    Measures: IOption[];
    Ingredients: IOption[];
}