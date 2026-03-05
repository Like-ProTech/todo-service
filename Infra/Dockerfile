FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Todo-Service.csproj", "."]
RUN dotnet restore "Todo-Service.csproj"

COPY . .


RUN dotnet publish "Todo-Service.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Todo-Service.dll"]