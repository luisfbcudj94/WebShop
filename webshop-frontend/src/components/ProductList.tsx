import React, { useEffect, useState } from 'react';
import { getProducts } from '../services/productService';
import { ProductDTO } from '../types';
import { Table, Spinner, Alert, Button, Form } from 'react-bootstrap';
import { useDispatch } from 'react-redux';
import { addToCart } from '../redux/cartSlice';

const ProductList: React.FC = () => {
  const [products, setProducts] = useState<ProductDTO[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [quantities, setQuantities] = useState<{ [key: string]: number }>({}); 
  const dispatch = useDispatch();

  useEffect(() => {
    const fetchProducts = async () => {
      try {
        const data = await getProducts();
        setProducts(data.items);
      } catch (error) {
        setError('Error fetching products');
      } finally {
        setLoading(false);
      }
    };

    fetchProducts();
  }, []);

  const handleQuantityChange = (productId: string, quantity: number) => {
    setQuantities(prev => ({
      ...prev,
      [productId]: quantity
    }));
  };

  const handleAddToCart = (product: ProductDTO) => {
    if (quantities[product.productId] > 0) {
      dispatch(addToCart({
        ...product,
        quantity: quantities[product.productId]
      }));
      setQuantities(prev => ({
        ...prev,
        [product.productId]: 0
      }));
    }
  };

  if (loading) return <Spinner animation="border" />;
  if (error) return <Alert variant="danger">{error}</Alert>;

  return (
    <div>
      <h1>Product List</h1>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Description</th>
            <th>Price</th>
            <th>Stock</th>
            <th>Image</th>
            <th>Quantity</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {products.map((product) => (
            <tr key={product.productId.toString()}>
              <td>{product.productId}</td>
              <td>{product.productName}</td>
              <td>{product.description}</td>
              <td>${product.price.toFixed(2)}</td>
              <td>{product.stockQuantity}</td>
              <td>
                {product.imageBase64 && (
                  <img
                    src={`data:image/png;base64,${product.imageBase64}`}
                    alt={product.productName}
                    style={{ width: '100px', height: 'auto' }}
                  />
                )}
              </td>
              <td>
                <Form.Control
                  type="number"
                  value={quantities[product.productId] || 0}
                  onChange={(e) => handleQuantityChange(product.productId, parseInt(e.target.value, 10))}
                  min="0"
                  max={product.stockQuantity}
                />
              </td>
              <td>
                <Button
                  variant="primary"
                  onClick={() => handleAddToCart(product)}
                >
                  Add to Cart
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </Table>
    </div>
  );
};

export default ProductList;

