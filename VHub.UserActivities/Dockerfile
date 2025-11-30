FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
LABEL maintainer="DevBeavers"
WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "VHub.UserActivities.Host.dll"]