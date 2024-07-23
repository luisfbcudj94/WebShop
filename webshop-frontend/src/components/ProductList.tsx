import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { RootState, AppDispatch } from '../redux/store';
import { setProducts } from '../redux/productSlice';

const ProductList: React.FC = () => {
  const dispatch: AppDispatch = useDispatch();
  const products = useSelector((state: RootState) => state.products.products);

  useEffect(() => {
    // Fetch products from API and dispatch to store
    const fetchProducts = async () => {
      const response = await fetch('/api/products'); // Update with actual API endpoint
      const data = await response.json();
      dispatch(setProducts(data));
    };

    fetchProducts();
  }, [dispatch]);

  return (
    <div>
      <h1>Product List</h1>
      <ul>
        {products.map(product => (
          <li key={product.id}>
            {product.name} - {product.price}
            {/* Add button to add product to cart */}
          </li>
        ))}
      </ul>
    </div>
  );
};

export default ProductList;