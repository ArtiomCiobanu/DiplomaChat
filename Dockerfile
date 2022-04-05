#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore DiplomaChat_Master.sln
RUN dotnet build --no-restore DiplomaChat_Master.sln -c Debug -o /output/build

FROM build AS publish

RUN dotnet publish DiplomaChat_Master.sln -c Debug -o /output/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /output/publish .
ENTRYPOINT ["dotnet", "DiplomaChat.dll"]