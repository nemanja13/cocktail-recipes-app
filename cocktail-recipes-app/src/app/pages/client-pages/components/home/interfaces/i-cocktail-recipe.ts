import { IOption } from "src/app/shared/interfaces/i-option";
import { IType } from "./i-type";
import { IBaseFilterRequest } from "src/app/shared/interfaces/i-base-filter-request";

export interface ICocktailRecipe {
    id: number;
    name: string;
    image: string;
    instructions: string;
    ingredients: string;
    type: string;
    measure: string;
}

export interface ICocktailRecipeFilterForm {
    keyword: string | null;
    typeId?: number | null;
    orderBy?: number | null;
}

export interface ICocktailRecipeFilterRequest extends ICocktailRecipeFilterForm, IBaseFilterRequest {
}

export interface ICocktailRecipeDropdownData {
    Types: IType[];
    OrderByOptions: IOption[];
}