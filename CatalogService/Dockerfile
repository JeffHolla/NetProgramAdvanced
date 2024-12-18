
# This stage is used to build the service project
# FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ENV APP_NAME=CatalogService
# ARG BUILD_CONFIGURATION=Release
ARG BUILD_CONFIGURATION=Debug
ENV ASPNETCORE_ENVIRONMENT=Development
COPY . /${APP_NAME}
WORKDIR /${APP_NAME}/src/Web
RUN dotnet restore \
    && dotnet build Web.csproj -v minimal -c ${BUILD_CONFIGURATION} \
    && dotnet publish Web.csproj -v minimal -o /publish_app -c ${BUILD_CONFIGURATION} 
WORKDIR /

# FROM build as cleanup
RUN rm -r /${APP_NAME}

FROM build as startup
WORKDIR /publish_app
EXPOSE 54283
EXPOSE 54284
ENV ASPNETCORE_URLS=https://+:54283
ENV ASPNETCORE_URLS=http://+:54284

# RUN apk add openssl
# RUN dotnet dev-certs https --clean
# RUN dotnet dev-certs https --trust

CMD dotnet ${APP_NAME}.Web.dll