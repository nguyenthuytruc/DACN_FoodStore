<template>
	
	<div class="cart-container">
		<div class="cart-header">
			<router-link :to="`/order/${idTable}`">
				<button class="btn-back">
					<i class="fa fa-arrow-circle-left" aria-hidden="true"></i>
				</button>
			</router-link>
			<h4>Giỏ hàng bàn: {{ idTable }}</h4>
		</div>
		<div class="cart-content">
			<h5>Món ăn đã thêm</h5>
			<ul v-if="cartItems.length > 0" class="cart-list">
				<li v-for="item in cartItems" :key="item.id" class="cart-item">
					<div class="item-info">
						<div class="item-name">{{ item.name }}</div>
						<div class="item-price">{{ formatPrice( item.price * item.quantity ) }}</div>
					</div>
					<div class="quantity-controls">
						<button @click="decreaseQuantity(item)" :disabled="item.quantity === 1" class="btn-quantity">-</button>
						<span>{{ item.quantity }}</span>
						<button @click="increaseQuantity(item)" class="btn-quantity">+</button>
					</div>
					<button class="btn-delete" @click="removeItem(item.id)"><i class="fa-solid fa-trash"></i></button>
				</li>
			</ul>
			<p v-else>Giỏ hàng trống</p>
			<div class="total-price">
				<span>Tổng Cộng:</span> <span class="mx-2">{{ formatPrice(  cartItems.reduce((total, item) => total + item.price * item.quantity, 0) ) }}</span> 
			</div>
			<div class="cart-buttons">
			
				<button class="btn-confirm " @click="submitOrder()">Thanh Toán</button>
			</div>
		</div>
	</div>
</template>

<style scoped>
.cart-container {
	max-width: 500px;
	margin: auto;
	padding: 20px;
	background-color: #fff;
	border-radius: 8px;
	box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
	font-family: Arial, sans-serif;
}

.cart-header {
	display: flex;
	align-items: center;
	justify-content: space-between;
	margin-bottom: 20px;
}

h4 {
	color: #333;
	font-weight: 600;
	margin: 0;
}

h5 {
	margin-bottom: 10px;
	font-size: 1.1em;
	color: #444;
}

.cart-content {
	display: flex;
	flex-direction: column;
}

.cart-list {
	list-style-type: none;
	padding: 0;
	margin: 0;
}

.cart-item {
	display: flex;
	align-items: center;
	justify-content: space-between;
	padding: 10px;
	border-bottom: 1px solid #ddd;
}

.item-info {
	display: flex;
	flex-direction: column;
	flex-grow: 1;
	margin-right: 10px;
}

.item-name {
	font-weight: bold;
}

.item-price {
	color: #888;
	margin-top: 4px;
}

.quantity-controls {
	display: flex;
	align-items: center;
}

.btn-quantity {
	background-color: #e0e0e0;
	border: none;
	padding: 4px 8px;
	margin: 0 5px;
	border-radius: 4px;
	cursor: pointer;
	font-size: 14px;
}

.btn-quantity:disabled {
	background-color: #ccc;
}

.btn-delete {
	background-color: #e74c3c;
	color: white;
	border: none;
	border-radius: 4px;
	padding: 4px 8px;
	font-size: 12px;
	cursor: pointer;
	transition: background-color 0.3s;
}

.btn-delete:hover {
	background-color: #c0392b;
}

.total-price {
	display: flex;
	justify-content: flex-end;
	font-size: 16px;
	font-weight: bold;
	margin: 15px 0;
}

.cart-buttons {
	display: flex;
	justify-content: space-between;
}

.btn-cancel {
	background-color: #ddd;
	color: #333;
	border: none;
	padding: 8px 16px;
	border-radius: 4px;
	cursor: pointer;
}

.btn-confirm {
	background-color: #3498db;
	color: white;
	border: none;
	padding: 8px 16px;
	border-radius: 4px;
	cursor: pointer;
}

.btn-back {
	background: none;
	border: none;
	color: #3498db;
	cursor: pointer;
	font-size: 20px;
}

.btn-confirm:hover {
	background-color: #2980b9;
}

.btn-cancel:hover {
	background-color: #bbb;
}

</style>
<script setup>
import { ref, onMounted } from "vue";
import { useRoute } from "vue-router";
import axiosInstance from "../config/axios.js";
import { useToast } from "vue-toastification";

const route = useRoute();
const idTable = ref(route.params.idTable); // Lấy idTable từ route params
const cartItems = ref([]); // Danh sách món ăn trong giỏ hàng
const toast = useToast();
const orderHistory = ref([]);

// Hàm lấy dữ liệu món ăn từ API theo ID
async function getFoodById(id) {
	try {
		const response = await axiosInstance.get("/food/" + id);
		return response.data;
	} catch (error) {
		console.error("Error fetching food data:", error);
	}
}

// Hàm lấy giỏ hàng và dữ liệu chi tiết của từng món ăn
async function getCartItems() {
	const cart = JSON.parse(sessionStorage.getItem("cart")) || {};

	const items = await Promise.all(
		Object.entries(cart).map(async ([id, quantity]) => {
			console.log(id);
			const food = await getFoodById(id);
			return {
				id: parseInt(id),
				name: food.name,
				price: food.price,
				quantity,
			};
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
	const cart = JSON.parse(sessionStorage.getItem("cart")) || {};
	cart[item.id] = item.quantity;
	sessionStorage.setItem("cart", JSON.stringify(cart));
}

// Hàm xóa món khỏi giỏ hàng
function removeItem(id) {
	cartItems.value = cartItems.value.filter((item) => item.id !== id);
	const cart = JSON.parse(sessionStorage.getItem("cart")) || {};
	delete cart[id];
	sessionStorage.setItem("cart", JSON.stringify(cart));
}

async function submitOrder() {
	// Lấy dữ liệu giỏ hàng từ sessionStorage
	const cart = JSON.parse(sessionStorage.getItem("cart")) || {};

	// Chuẩn bị dữ liệu OrderDTO
	const orderDTO = {
		tableId: route.params.idTable, // ID của bàn (có thể lấy từ giao diện)
		status: false, // Đặt mặc định là chưa xử lý
		totalPrice: 0, // Sẽ tính lại từ giỏ hàng
		statusPay: false, // Đặt mặc định là chưa thanh toán
		listOrderDetail: [], // Danh sách chi tiết món ăn
	};

	// Xử lý chi tiết giỏ hàng
	const items = await Promise.all(
		Object.entries(cart).map(async ([id, quantity]) => {
			const food = await getFoodById(id);
			const totalItemPrice = food.price * quantity;

			// Tính tổng giá của đơn hàng
			orderDTO.totalPrice += totalItemPrice;

			// Thêm chi tiết món ăn vào danh sách
			return {
				foodId: parseInt(id),
				quantity: quantity,
				price: food.price,
			};
		})
	);

	// Thêm listOrderDetail vào orderDTO
	orderDTO.listOrderDetail = items;
	// Gửi đơn hàng lên server
	try {
		const response = await axiosInstance.post("/order", orderDTO);
		if (response.status === 200) {
			console.log("Đơn hàng đã được gửi thành công!");
			// Sau khi gửi thành công, xóa giỏ hàng khỏi sessionStorage

			// Lấy thông tin order từ phản hồi server
			const orderInfo = response.data; // Giả sử server trả về thông tin đơn hàng sau khi tạo

			// Lấy lịch sử order từ sessionStorage hoặc tạo mảng mới
			const orderHistory =
				JSON.parse(sessionStorage.getItem("orderHistory")) || [];

			// Thêm thông tin order vào lịch sử
			orderHistory.push({
				orderId: orderInfo.id, // Giả sử id là mã đơn hàng từ server
				tableId: orderDTO.tableId,
				totalPrice: orderDTO.totalPrice,
				date: new Date().toISOString(), // Thời gian hiện tại khi đặt
				items: items, // Danh sách món ăn đã đặt
			});

			// Lưu lại lịch sử order vào sessionStorage
			sessionStorage.setItem(
				"orderHistory",
				JSON.stringify(orderHistory)
			);

			// Sau khi gửi thành công, xóa giỏ hàng khỏi sessionStorage
			sessionStorage.removeItem("cart");

			console.log(orderHistory);

			// Cập nhật giao diện hoặc điều hướng nếu cần
			this.toast.success("Đặt món thành công !", {
				position: "bottom-left",
				timeout: 1500,
				closeOnClick: true,
				pauseOnFocusLoss: true,
				pauseOnHover: true,
				draggable: true,
				draggablePercent: 0.42,
				showCloseButtonOnHover: false,
				hideProgressBar: true,
				closeButton: "button",
				icon: true,
				rtl: false,
			});
		}
	} catch (error) {
		console.error("Lỗi khi gửi đơn hàng:", error);
	}
};

function formatPrice(price) {
      // Định dạng giá tiền theo VND
      return price.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
};


// Khởi tạo dữ liệu khi component được mounted
onMounted(async () => {
	await getCartItems();
});
</script>
