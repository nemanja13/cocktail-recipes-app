import { IOption } from "src/app/shared/interfaces/i-option";
import { IType } from "./i-type";

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

export interface ICocktailRecipeFilterRequest extends ICocktailRecipeFilterForm {
    page: number;
    perPage: number;
}

export interface ICocktailRecipeDropdownData {
    Types: IType[];
    OrderByOptions: IOption[];
}