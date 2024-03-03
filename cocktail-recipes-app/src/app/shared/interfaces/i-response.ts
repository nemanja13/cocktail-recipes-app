export interface IResponse<T> {
    totalCount?: number;
    currentPage?: number;
    lastPage?: number;
    itemsPerPage?: number;
    data: T[];
}
