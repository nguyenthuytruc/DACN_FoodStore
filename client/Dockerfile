# Sử dụng Node.js để build ứng dụng Vue.js
FROM node:20

# Đặt thư mục làm việc
WORKDIR /app

# Sao chép package.json và package-lock.json
COPY package*.json ./

# Cài đặt phụ thuộc
RUN npm install

# Sao chép mã nguồn
COPY . .

# Build ứng dụng
RUN npm run build

# Mở cổng để chạy server
EXPOSE 3000

# Khởi động ứng dụng
CMD ["npm", "run", "dev"]

