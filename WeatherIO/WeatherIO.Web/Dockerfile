FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WeatherIO.Web/WeatherIO.Web.csproj", "WeatherIO.Web/"]
COPY ["WeatherIO.Common/WeatherIO.Common.csproj", "WeatherIO.Common/"]
RUN dotnet restore "WeatherIO.Web/WeatherIO.Web.csproj"
COPY . .
RUN dotnet build "WeatherIO.Web/WeatherIO.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WeatherIO.Web/WeatherIO.Web.csproj" -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY ["WeatherIO.Web/nginx.conf", "/etc/nginx/nginx.conf"]
EXPOSE 80
