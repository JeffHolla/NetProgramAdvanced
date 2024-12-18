
# This stage is used to build the service project
# FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
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
# RUN rm -r /${APP_NAME}

FROM build as startup
WORKDIR /publish_app
# EXPOSE 5001
# ENV ASPNETCORE_URLS="https://+;http://+"

# Since /https folder is not mounted yet, we can't copy certs
# RUN cp /https/ipaddress* /etc/ssl/certs/ && update-ca-certificates && update-ca-certificates --fresh

#ENV HTTP_PORTS=8080
#ENV HTTPS_PORTS=8081

# RUN apk add openssl
# RUN dotnet dev-certs https --clean
# RUN dotnet dev-certs https --trust

RUN apt update && apt install mkcert 

CMD sleep 20s && dotnet ${APP_NAME}.dll

# final stage/image
# FROM mcr.microsoft.com/dotnet/aspnet:8.0.11-alpine3.20-amd64
# WORKDIR /publish_app
# COPY --from=build /publish_app ./
# ENTRYPOINT ["dotnet", "IdentityServer.dll"]
# ------------------------ ``