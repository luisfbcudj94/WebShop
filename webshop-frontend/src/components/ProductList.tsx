// import React, { useEffect } from 'react';
// import { useDispatch, useSelector } from 'react-redux';
// import { RootState, AppDispatch } from '../redux/store';
// import { setProducts } from '../redux/productSlice';

// const ProductList: React.FC = () => {
//   const dispatch: AppDispatch = useDispatch();
//   const products = useSelector((state: RootState) => state.products.products);

//   useEffect(() => {
//     const fetchProducts = async () => {
//       const response = await fetch('/api/products'); 
//       const data = await response.json();
//       dispatch(setProducts(data));
//     };

//     fetchProducts();
//   }, [dispatch]);

//   return (
//     <div>
//       <h1>Product List</h1>
//       <ul>
//         {products.map(product => (
//           <li key={product.id}>
//             {product.name} - {product.price}
//             {/* Add button to add product to cart */}
//           </li>
//         ))}
//       </ul>
//     </div>
//   );
// };

// export default ProductList;

// src/components/ProductList.tsx
import React, { useEffect, useState } from 'react';
import { getProducts } from '../services/productService';
import { Product } from '../types';

const ProductList: React.FC = () => {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const products = await getProducts();
        setProducts(products);
      } catch (error) {
        setError('Error fetching products');
      } finally {
        setLoading(false);
      }
    };

    fetchProducts();
  }, []);

  if (loading) return <div>Loading...</div>;
  if (error) return <div>{error}</div>;

  return (
    <div>
      <h1>Product List</h1>
      <ul>
        {products.map((product) => (
          <li key={product.id}>
            <h2>{product.title}</h2>
            <p>{product.description}</p>
            <p>Price: ${product.price}</p>
            <p>Stock: {product.stockQuantity}</p>
          </li>
        ))}
      </ul>
    </div>
  );
};

export default ProductList;
