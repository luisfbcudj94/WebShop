import React, { useEffect, useState } from 'react';
import { getProducts } from '../services/productService';
import { ProductDTO } from '../types';
import { Table, Spinner, Alert, Button, Form, Modal } from 'react-bootstrap';
import { useDispatch } from 'react-redux';
import { addToCart } from '../redux/cartSlice';

interface ProductWithQuantity extends ProductDTO {
  quantity: number;
}

const ProductList: React.FC = () => {
  const [products, setProducts] = useState<ProductWithQuantity[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [showSuccessModal, setShowSuccessModal] = useState<boolean>(false);
  const [showErrorModal, setShowErrorModal] = useState<boolean>(false);
  const [modalContent, setModalContent] = useState<string>('');
  const dispatch = useDispatch();

  useEffect(() => {
    let storedCustomerId = localStorage.getItem('customerId');
    
    if (!storedCustomerId) {
      storedCustomerId = '0040AD21-0CA5-408E-A202-61B1C82B1FD6';
      localStorage.setItem('customerId', storedCustomerId);
    }

    const fetchProducts = async () => {
      try {
        const data = await getProducts();
        const productsFromAPI = data.items;
        const existingCartItems = JSON.parse(localStorage.getItem('cart') || '[]');

        const productsWithQuantity = productsFromAPI.map(product => {
          const cartItem = existingCartItems.find((item: ProductWithQuantity) => item.productId === product.productId);
          return {
            ...product,
            quantity: cartItem ? cartItem.quantity : 0
          };
        });

        setProducts(productsWithQuantity);
      } catch (error) {
        setError('Error fetching products');
      } finally {
        setLoading(false);
      }
    };

    fetchProducts();
  }, []);

  const handleQuantityChange = (productId: string, quantity: number) => {
    setProducts(prevProducts => prevProducts.map(product => 
      product.productId === productId
        ? { ...product, quantity }
        : product
    ));
  };

  const handleAddToCart = (product: ProductWithQuantity) => {
    if (product.quantity > product.stockQuantity) {
      setModalContent('Quantity exceeds available stock.');
      setShowErrorModal(true);
      return;
    }

    if (product.quantity > 0) {
      dispatch(addToCart({
        ...product,
        quantity: product.quantity
      }));

      const existingCartItems = JSON.parse(localStorage.getItem('cart') || '[]');

      const productIndex = existingCartItems.findIndex((item: ProductWithQuantity) => item.productId === product.productId);

      if (productIndex >= 0) {
        existingCartItems[productIndex].quantity = product.quantity;
      } else {
        existingCartItems.push({
          productId: product.productId,
          productName: product.productName,
          price: product.price,
          quantity: product.quantity,
          imageBase64: product.imageBase64,
          description: product.description
        });
      }

      localStorage.setItem('cart', JSON.stringify(existingCartItems));

      setModalContent(`Added ${product.productName} to cart.`);
      setShowSuccessModal(true);

      setProducts(prevProducts => prevProducts.map(p => 
        p.productId === product.productId
          ? { ...p, quantity: product.quantity }
          : p
      ));
    } else {
      setModalContent('Quantity must be greater than 0.');
      setShowErrorModal(true);
    }
  };

  const handleCloseSuccessModal = () => setShowSuccessModal(false);
  const handleCloseErrorModal = () => setShowErrorModal(false);

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
                    style={{ width: '100px', height: 'auto', maxHeight: '200px' }}
                  />
                )}
              </td>
              <td>
                <Form.Control
                  type="number"
                  value={product.quantity}
                  onChange={(e) => handleQuantityChange(product.productId, Math.max(1, parseInt(e.target.value, 10)))}
                  min="1"
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

      <Modal show={showSuccessModal} onHide={handleCloseSuccessModal} centered>
        <Modal.Header closeButton>
          <Modal.Title>
            <span style={{ color: 'green' }}>Success</span>
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div style={{ display: 'flex', alignItems: 'center' }}>
            <span style={{ color: 'green', fontSize: '24px', marginRight: '10px' }}>✔️</span>
            {modalContent}
          </div>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="success" onClick={handleCloseSuccessModal}>
            Close
          </Button>
        </Modal.Footer>
      </Modal>

      <Modal show={showErrorModal} onHide={handleCloseErrorModal} centered>
        <Modal.Header closeButton>
          <Modal.Title>
            <span style={{ color: 'red' }}>Error</span>
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <div style={{ display: 'flex', alignItems: 'center' }}>
            <span style={{ color: 'red', fontSize: '24px', marginRight: '10px' }}>⚠️</span>
            {modalContent}
          </div>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleCloseErrorModal}>
            Close
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
};

export default ProductList;
