#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CNTT129_NetCore.csproj", "CNTT129_NetCore/"]
RUN dotnet restore "CNTT129_NetCore/CNTT129_NetCore.csproj"
COPY . .
WORKDIR "/src/CNTT129_NetCore"
RUN dotnet build "CNTT129_NetCore.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CNTT129_NetCore.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CNTT129_NetCore.dll"]