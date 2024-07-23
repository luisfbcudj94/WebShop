import React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { RootState, AppDispatch } from '../redux/store';
import { removeFromCart, updateQuantity } from '../redux/cartSlice';

const Cart: React.FC = () => {
  
  return (
    <div>
      <h1>Shopping Cart</h1>
    </div>
  );
};

export default Cart;