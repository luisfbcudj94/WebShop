export interface ProductDTO {
    productId: string;
    productName: string;
    productCode: string;
    description: string;
    price: number;
    stockQuantity: number;
    imageBase64?: string;
}

export interface PagedResult<T> {
        items: T[];
        hasPreviousPage: boolean;
        hasNextPage: boolean;
        totalCount: number;
    }