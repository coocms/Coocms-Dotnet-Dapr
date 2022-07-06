#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["StateManagementDemo/StateManagementDemo.csproj", "StateManagementDemo/"]
RUN dotnet restore "StateManagementDemo/StateManagementDemo.csproj"
COPY . .
WORKDIR "/src/StateManagementDemo"
RUN dotnet build "StateManagementDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StateManagementDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StateManagementDemo.dll"]