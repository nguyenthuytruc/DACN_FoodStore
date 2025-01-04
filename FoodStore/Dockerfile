# Sử dụng hình ảnh ASP.NET Core runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Sử dụng hình ảnh SDK để build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Sao chép tệp dự án
# COPY FoodStore/FoodStore.csproj ./FoodStore/
COPY ["FoodStore.csproj", "./FoodStore/FoodStore.csproj"]

# Khôi phục các phụ thuộc
RUN dotnet restore ./FoodStore/FoodStore.csproj

# Sao chép toàn bộ mã nguồn
COPY . ./FoodStore

# Build ứng dụng
WORKDIR /src/FoodStore
RUN dotnet publish -c Release -o /app

# Runtime stage
FROM base AS final
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "FoodStore.dll"]
