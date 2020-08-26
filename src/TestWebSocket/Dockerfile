FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine as build

COPY . ./build/

RUN dotnet restore ./build/TestWebSocket/TestWebSocket.csproj

WORKDIR /build/

RUN dotnet publish ./TestWebSocket/TestWebSocket.csproj -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine as main-site

WORKDIR /app
EXPOSE 80
EXPOSE 443
#COPY --from=build 

COPY --from=build /build/out .

ENTRYPOINT ["dotnet", "TestWebSocket.dll"]
