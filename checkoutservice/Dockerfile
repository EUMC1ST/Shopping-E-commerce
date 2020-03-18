FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["checkoutservice.csproj", ""]
RUN dotnet restore "checkoutservice.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "checkoutservice.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "checkoutservice.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "checkoutservice.dll"]