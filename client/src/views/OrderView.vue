<script lang="js" setup>

import router from '@/router';

const idTable = router.currentRoute.value.params['idTable'];
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
					<input type="text" class="form-control" />
					<button class="btn btn-danger">
						<i class="fa fa-search" aria-hidden="true"></i>
					</button>
				</div>
				<button class="btn btn-primary">
					<i class="fa fa-cart-arrow-down" aria-hidden="true"></i>
				</button>
			</div>
			<div class="order--main">
				<div class="list--food">
					<div class="food">
						<h5>Trà sữa</h5>
						<p>Giá tiền: 50.000</p>

						<button class="btn btn-sm btn-primary w-100">
							<i
								class="fa fa-cart-arrow-down"
								aria-hidden="true"></i>
						</button>
					</div>
					<div class="food">
						<h5>Trà sữa</h5>
						<p>Giá tiền: 50.000</p>

						<button class="btn btn-sm btn-primary w-100">
							<i
								class="fa fa-cart-arrow-down"
								aria-hidden="true"></i>
						</button>
					</div>
				</div>
			</div>
		</div>
	</div>
</template>
<style lang="css">
.order--wrapper {
}

.order--header {
	display: flex;
	align-items: center;
	justify-content: space-between;
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
	grid-template-columns: 1fr 1fr;
	gap: 12px;
}

.food {
	border: 1px solid #cecece;
	max-width: 200px;
}
</style>
