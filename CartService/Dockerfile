
# This stage is used to build the service project
# FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ENV APP_NAME=CartService
# ARG BUILD_CONFIGURATION=Release
ARG BUILD_CONFIGURATION=Debug
ENV ASPNETCORE_ENVIRONMENT=Development
COPY . /${APP_NAME}
WORKDIR /${APP_NAME}/CartService
RUN dotnet restore \
    && dotnet build CartService.csproj -v minimal -c ${BUILD_CONFIGURATION} \
    && dotnet publish CartService.csproj -v minimal -o /publish_app -c ${BUILD_CONFIGURATION} 
WORKDIR /

# FROM build as cleanup
# RUN rm -r /${APP_NAME}

FROM build as startup
WORKDIR /publish_app
# EXPOSE 57931
# EXPOSE 57932
# ENV ASPNETCORE_URLS=https://+:57931
# ENV ASPNETCORE_URLS=http://+:57932

# Since /https folder is not mounted yet, we can't copy certs
# RUN cp /https/ipaddress* /etc/ssl/certs/ && update-ca-certificates && update-ca-certificates --fresh

# RUN apk add openssl
# RUN dotnet dev-certs https --clean
# RUN dotnet dev-certs https --trust

RUN apt update && apt install mkcert 

CMD sleep 20s && dotnet ${APP_NAME}.dll