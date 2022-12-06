FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WeddingServiceLab/WeddingServiceLab.csproj", "WeddingServiceLab/"]
COPY ["WeddingService.Bll/WeddingService.Bll.csproj", "WeddingService.Bll/"]
COPY ["WeddingService.Dal/WeddingService.Dal.csproj", "WeddingService.Dal/"]
RUN dotnet restore "WeddingServiceLab/WeddingServiceLab.csproj"
COPY . .
WORKDIR "/src/WeddingServiceLab"
RUN dotnet build "WeddingServiceLab.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WeddingServiceLab.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WeddingServiceLab.dll"]