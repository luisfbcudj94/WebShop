import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { AppDispatch } from '../redux/store';
import { removeFromCart, updateQuantity } from '../redux/cartSlice';
import { Table, Button, Alert, Modal } from 'react-bootstrap';
import { ProductDTO } from '../types';
import { FaTrash } from 'react-icons/fa';
import orderService from '../services/orderService';
import { generateGuid } from '../helpers/utils';

export interface CartItem extends ProductDTO {
  quantity: number;
}

const Cart: React.FC = () => {
  const [cartItems, setCartItems] = useState<CartItem[]>([]);
  const [showSuccessModal, setShowSuccessModal] = useState(false);
  const [showErrorModal, setShowErrorModal] = useState(false);
  const [modalContent, setModalContent] = useState('');
  const dispatch = useDispatch<AppDispatch>();

  useEffect(() => {
    const savedCartItems = JSON.parse(localStorage.getItem('cart') || '[]');
    setCartItems(savedCartItems);
  }, []);

  const handleRemove = (productId: string) => {
    dispatch(removeFromCart(productId));
    const updatedCartItems = cartItems.filter(item => item.productId !== productId);
    localStorage.setItem('cart', JSON.stringify(updatedCartItems));
    setCartItems(updatedCartItems);
  };

  const handleQuantityChange = (productId: string, quantity: number) => {
    if (quantity > 0) {
      dispatch(updateQuantity({ productId, quantity }));
      const updatedCartItems = cartItems.map(item =>
        item.productId === productId
          ? { ...item, quantity }
          : item
      );
      localStorage.setItem('cart', JSON.stringify(updatedCartItems));
      setCartItems(updatedCartItems);
    }
  };

  const handleUpdate = (productId: string, quantity: number) => {
    if (quantity > 0) {
      handleQuantityChange(productId, quantity);
    }
  };

  const handlePlaceOrder = async () => {
    const customerId = localStorage.getItem('customerId');

    if (!customerId) {
      setModalContent('Customer ID is missing. Please log in.');
      setShowErrorModal(true);
      return;
    }

    const orderId = generateGuid();

    const orderData = {
      OrderId: orderId,
      CustomerId: customerId,
      OrderDate: new Date(),
      OrderProducts: cartItems.map(item => ({
        OrderProductId: generateGuid(),
        ProductId: item.productId,
        Quantity: item.quantity
      }))
    };

    try {
      await orderService.addToCart(orderId, orderData);
      setModalContent('Order placed successfully!');
      setShowSuccessModal(true);
      localStorage.removeItem('cart');
      setCartItems([]);
    } catch (error) {
      setModalContent('Failed to place order.');
      setShowErrorModal(true);
    }
  };

  const handleCloseSuccessModal = () => setShowSuccessModal(false);
  const handleCloseErrorModal = () => setShowErrorModal(false);

  const totalQuantity = cartItems.reduce((total, item) => total + item.quantity, 0);
  const totalPrice = cartItems.reduce((total, item) => total + item.price * item.quantity, 0);

  return (
    <div style={{ display: 'flex' }}>
      <div style={{ flex: 0.6, paddingRight: '20px' }}>
        <h1>Shopping Cart</h1>
        {cartItems.length === 0 ? (
          <Alert variant="info">No products have been added to the cart.</Alert>
        ) : (
          <Table striped bordered hover>
            <thead>
              <tr>
                <th>Name</th>
                <th>Description</th>
                <th>Price</th>
                <th>Quantity</th>
                <th>Actions</th>
              </tr>
            </thead>
            <tbody>
              {cartItems.map(item => (
                <tr key={item.productId}>
                  <td>{item.productName}</td>
                  <td>{item.description}</td>
                  <td>${item.price.toFixed(2)}</td>
                  <td>
                    <input
                      type="number"
                      value={item.quantity}
                      onChange={(e) => handleQuantityChange(item.productId, parseInt(e.target.value, 10))}
                      min="1"
                    />
                  </td>
                  <td>
                    <Button
                      variant="danger"
                      onClick={() => handleRemove(item.productId)}
                      style={{ marginLeft: '5px' }}
                    >
                      <FaTrash />
                    </Button>
                  </td>
                </tr>
              ))}
            </tbody>
          </Table>
        )}
      </div>
      <div style={{ flex: 0.4, paddingLeft: '20px', borderLeft: '1px solid #ccc' }}>
        <h2>Cart Summary</h2>
        <p><strong>Total Items:</strong> {totalQuantity}</p>
        <p><strong>Total Price:</strong> ${totalPrice.toFixed(2)}</p>
        <Button
          variant="primary"
          onClick={handlePlaceOrder}
          disabled={cartItems.length === 0}
        >
          Place Order
        </Button>
      </div>

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
            <span style={{ color: 'red', fontSize: '24px', marginRight: '10px' }}>❌</span>
            {modalContent}
          </div>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="danger" onClick={handleCloseErrorModal}>
            Close
          </Button>
        </Modal.Footer>
      </Modal>
    </div>
  );
};

export default Cart;
