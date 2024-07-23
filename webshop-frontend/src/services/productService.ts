import api from '../api';
import { PagedResult, ProductDTO  } from '../types';

export const getProducts = async (): Promise<PagedResult<ProductDTO>> => {
    const response = await api.get('/Catalog/products');
    return response.data;
  };