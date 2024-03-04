import { IOption } from "src/app/shared/interfaces/i-option";
import { OrderByOptionsEnum } from "../enums/order-by-options.enum";

export const ORDER_BY_OPTIONS: IOption[] = [
    {
        id: OrderByOptionsEnum.NameAsc,
        name: "Name ascending"
    },
    {
        id: OrderByOptionsEnum.NameDsc,
        name: "Name descending"
    },
    {
        id: OrderByOptionsEnum.CreationAsc,
        name: "Oldest"
    },
    {
        id: OrderByOptionsEnum.CreationDsc,
        name: "Newest"
    }
];