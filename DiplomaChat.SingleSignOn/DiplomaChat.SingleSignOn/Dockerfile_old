#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 44317

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DiplomaChat.SingleSignOn/DiplomaChat.SingleSignOn.csproj", "DiplomaChat.SingleSignOn/"]
RUN dotnet restore "DiplomaChat.SingleSignOn/DiplomaChat.SingleSignOn.csproj"
COPY . .
WORKDIR "/src/DiplomaChat.SingleSignOn"
RUN dotnet build "DiplomaChat.SingleSignOn.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DiplomaChat.SingleSignOn.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DiplomaChat.SingleSignOn.dll"]