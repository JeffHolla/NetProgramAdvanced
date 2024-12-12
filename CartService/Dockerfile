
# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
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
RUN rm -r /${APP_NAME}

FROM build as startup
WORKDIR /publish_app
EXPOSE 57931
EXPOSE 57932
ENV ASPNETCORE_URLS=https://+:57931
ENV ASPNETCORE_URLS=http://+:57932

RUN dotnet dev-certs https

CMD dotnet ${APP_NAME}.dll