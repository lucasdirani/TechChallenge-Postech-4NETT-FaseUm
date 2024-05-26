FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Postech.PhaseOne.GroupEight.TechChallenge.Api/Postech.PhaseOne.GroupEight.TechChallenge.Api.csproj", "src/Postech.PhaseOne.GroupEight.TechChallenge.Api/"]
RUN dotnet restore "src/Postech.PhaseOne.GroupEight.TechChallenge.Api/Postech.PhaseOne.GroupEight.TechChallenge.Api.csproj"
COPY . .
WORKDIR "/src/src/Postech.PhaseOne.GroupEight.TechChallenge.Api"
RUN dotnet build "Postech.PhaseOne.GroupEight.TechChallenge.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Postech.PhaseOne.GroupEight.TechChallenge.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Postech.PhaseOne.GroupEight.TechChallenge.Api.dll"]
