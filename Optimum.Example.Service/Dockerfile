FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Optimum.Example.Service/Optimum.Example.Service.csproj", "Optimum.Example.Service/"]
RUN dotnet restore "Optimum.Example.Service/Optimum.Example.Service.csproj"
COPY . .
WORKDIR "/src/Optimum.Example.Service"
RUN dotnet build "Optimum.Example.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Optimum.Example.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Optimum.Example.Service.dll"]
