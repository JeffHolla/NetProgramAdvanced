
# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ENV APP_NAME=IdentityServer
# ARG BUILD_CONFIGURATION=Release
ARG BUILD_CONFIGURATION=Debug
ENV ASPNETCORE_ENVIRONMENT=Development
COPY . /${APP_NAME}
WORKDIR /${APP_NAME}/IdentityServer
RUN dotnet restore \
    && dotnet build IdentityServer.csproj -v minimal -c ${BUILD_CONFIGURATION} \
    && dotnet publish IdentityServer.csproj -v minimal -o /publish_app -c ${BUILD_CONFIGURATION} 
WORKDIR /

# FROM build as cleanup
RUN rm -r /${APP_NAME}

FROM build as startup
WORKDIR /publish_app
EXPOSE 5001
ENV ASPNETCORE_URLS=https://+:5001
#ENV HTTP_PORTS=8080
#ENV HTTPS_PORTS=8081

RUN dotnet dev-certs https

CMD dotnet ${APP_NAME}.dll