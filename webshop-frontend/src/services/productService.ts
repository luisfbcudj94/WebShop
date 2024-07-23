import api from '../api';
import { Product } from '../types';

export const getProducts = async (): Promise<Product[]> => {
  const response = await api.get('/Catalog/products');
  console.log("RESPONSE: ",response);
//   return response.data;
    return response.data.items;
};