<script setup lang="js">
import { ref, onMounted } from 'vue';
import router from '@/router';
import axiosInstance from '../config/axios.js';

const idTable = router.currentRoute.value.params['idTable'];
const listFood = ref([]);

// Hàm xử lý khi thêm vào giỏ hàng
function addToCart(idFood) {
  // Kiểm tra xem giỏ hàng đã tồn tại trong session chưa
  let cart = JSON.parse(sessionStorage.getItem('cart')) || {};

  // Nếu món ăn đã tồn tại trong giỏ hàng, tăng số lượng
  if (cart[idFood]) {
    cart[idFood] += 1;
  } else {
    // Nếu chưa có, khởi tạo số lượng là 1
    cart[idFood] = 1;
  }

   // Lưu lại giỏ hàng vào session
  sessionStorage.setItem('cart', JSON.stringify(cart));

  console.log("Giỏ hàng hiện tại:", cart);
}
onMounted(async () => {
  try {
    const response = await axiosInstance.get('/food'); // Thay thế bằng API thực tế của bạn
  
    listFood.value = response.data.$values;
  } catch (error) {
    console.error("Error fetching food data:", error);
  }
});
</script>

<template>
  <div class="container">
    <h4 class="text-center px-2 py-2 bg-success text-white">
      Cửa hàng Food Store
    </h4>
    <h5>Bàn: {{ idTable }}</h5>
    <div class="order--wrapper">
      <div class="order--header">
        <div class="search">
          <input type="text" class="form-control" placeholder="Tìm món ăn" />
          <button class="btn btn-danger">
            <i class="fa fa-search" aria-hidden="true"></i>
          </button>
        </div>
       <router-link :to="`/cart/${idTable}`">
  <button class="btn btn-sm btn-primary">Đi đến giỏ hàng</button>
</router-link>
      </div>
      <div class="order--main">
        <div class="list--food">
          <div v-for="food in listFood" :key="food.id" class="food">
            <img :src="`https://localhost:7093/${food.image}`" alt="Food Image" class="food-image" />
            <h5>{{ food.name }}</h5>
            <p>Giá tiền: {{ food.price }} VND</p>
            <button class="btn btn-sm btn-primary w-100" @click="addToCart(food.id)">
              <i class="fa fa-cart-arrow-down" aria-hidden="true"></i> Thêm vào giỏ
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style lang="css">
.order--wrapper {
  padding: 16px;
}

.order--header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 24px;
  margin-bottom: 16px;
}

.order--header .search {
  display: flex;
  align-items: center;
  width: 300px;
}

.order--main {
  padding: 8px 4px;
}

.order--main .list--food {
  display: grid;
  grid-template-columns: repeat(2, 1fr); /* 2 cột cho thiết bị nhỏ */
  gap: 12px;
}

.food {
  border: 1px solid #cecece;
  padding: 12px;
  text-align: center;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.food h5 {
  font-size: 1.1em;
  margin-bottom: 8px;
}

.food p {
  font-size: 1em;
  color: #555;
}

.food button {
  margin-top: 8px;
}

.food-image {
  width: 100px;
  height: 100px;
  object-fit: cover;
  border-radius: 6px;
  margin-bottom: 8px;
}



/* Tự động thay đổi số cột trên thiết bị lớn hơn */
@media (min-width: 768px) {
  .order--main .list--food {
    grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  }
}
</style>
