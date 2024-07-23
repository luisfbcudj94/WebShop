import api from '../api';

const orderService = {
  addToCart: (orderId: string, data: any) => {
    return api.post(`Orders/${orderId}/addtocart`, data);
  }
};

export default orderService;