FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BindNineCore.Web/BindNineCore.Web.csproj", "BindNineCore.Web/"]
RUN dotnet restore "BindNineCore.Web/BindNineCore.Web.csproj"
COPY . .
WORKDIR "/src/BindNineCore.Web"
RUN dotnet build "BindNineCore.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BindNineCore.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BindNineCore.Web.dll"]
