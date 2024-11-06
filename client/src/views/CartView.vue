<template>
  <div class="cart-container">
    <h4>Giỏ hàng bàn: {{ idTable }}</h4>
    <ul v-if="cartItems.length > 0" class="cart-list">
      <li v-for="item in cartItems" :key="item.id" class="cart-item">
        <div class="item-info">
          <div>{{ item.name }}</div>
          <div>Giá: {{ item.price * item.quantity }} VND</div>
        </div>
        <div class="quantity-controls">
          <button @click="decreaseQuantity(item)" :disabled="item.quantity === 1" class="btn btn-sm btn-secondary">-</button>
          <span>{{ item.quantity }}</span>
          <button @click="increaseQuantity(item)" class="btn btn-sm btn-secondary">+</button>
        </div>
        <button class="btn btn-sm btn-danger" @click="removeItem(item.id)"><i class="fa fa-trash" aria-hidden="true"></i></button>
      </li>
    </ul>
    <p v-else>Giỏ hàng trống</p>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import axiosInstance from '../config/axios.js';

const route = useRoute();
const idTable = ref(route.params.idTable); // Lấy idTable từ route params
const cartItems = ref([]); // Danh sách món ăn trong giỏ hàng

// Hàm lấy dữ liệu món ăn từ API theo ID
async function getFoodById(id) {
  try {
    const response = await axiosInstance.get('/food/' + id);
    return response.data;
  } catch (error) {
    console.error("Error fetching food data:", error);
  }
}

// Hàm lấy giỏ hàng và dữ liệu chi tiết của từng món ăn
async function getCartItems() {
  const cart = JSON.parse(sessionStorage.getItem('cart')) || {};

  const items = await Promise.all(
    Object.entries(cart).map(async ([id, quantity]) => {
      console.log(id); 
      const food = await getFoodById(id);
      return { id: parseInt(id), name: food.name, price: food.price, quantity };
    })
  );

  cartItems.value = items;
}

// Hàm tăng số lượng
function increaseQuantity(item) {
  item.quantity += 1;
  updateCartItem(item);
}

// Hàm giảm số lượng
function decreaseQuantity(item) {
  if (item.quantity > 1) {
    item.quantity -= 1;
    updateCartItem(item);
  }
}

// Hàm cập nhật giỏ hàng trong sessionStorage
function updateCartItem(item) {
  const cart = JSON.parse(sessionStorage.getItem('cart')) || {};
  cart[item.id] = item.quantity;
  sessionStorage.setItem('cart', JSON.stringify(cart));
}

// Hàm xóa món khỏi giỏ hàng
function removeItem(id) {
  cartItems.value = cartItems.value.filter(item => item.id !== id);
  const cart = JSON.parse(sessionStorage.getItem('cart')) || {};
  delete cart[id];
  sessionStorage.setItem('cart', JSON.stringify(cart));
}

// Khởi tạo dữ liệu khi component được mounted
onMounted(async () => {
  await getCartItems();
});
</script>

<style scoped>
.cart-container {
  max-width: 600px;
  margin: auto;
  padding: 20px;
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
}

h4 {
  color: #333;
  text-align: center;
  margin-bottom: 20px;
  font-weight: 600;
}

.cart-list {
  list-style-type: none;
  padding: 0;
  margin: 0;
}

.cart-item {
  padding: 10px;
  border-bottom: 1px solid #ddd;
  display: flex;
  align-items: center;
  justify-content: space-between;
  background-color: #f8f9fa;
  border-radius: 6px;
  margin-bottom: 10px;
}

.item-info {
  flex-grow: 1;
}

.quantity-controls {
  display: flex;
  align-items: center;
  gap: 4px;

}

.quantity-controls button {
  padding: 5px;
}

.quantity-controls span {
  font-weight: bold;
  padding: 0 10px;
}

.cart-item button.btn-danger {
  font-size: 12px;
  border: none;
  background-color: #e74c3c;
  color: #fff;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;
  margin-left: 8px;
}

.cart-item button.btn-danger:hover {
  background-color: #c0392b;
}
</style>
