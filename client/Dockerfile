# -------------------------------------------
# STAGE 1: Build
# -------------------------------------------
FROM node:20 AS build

# Đặt thư mục làm việc
WORKDIR /app

# Sao chép file package.json và package-lock.json
COPY package*.json ./

# Cài đặt dependencies
RUN npm install

# Sao chép toàn bộ mã nguồn
COPY . .

# Build ứng dụng (tạo ra thư mục dist)
RUN npm run build


# -------------------------------------------
# STAGE 2: Production
# -------------------------------------------
FROM node:20 AS production

WORKDIR /app

# Copy thư mục dist từ stage build sang
COPY --from=build /app/dist /app/dist

# Cài đặt serve để chạy file tĩnh
RUN npm install -g serve

# Expose cổng 3000
EXPOSE 3000

# Dùng serve để chạy thư mục dist ở cổng 3000
CMD ["serve", "-s", "dist", "-l", "3000"]
