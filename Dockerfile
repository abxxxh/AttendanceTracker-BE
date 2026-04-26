# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["AttendanceTracker.Api/AttendanceTracker.Api.csproj", "AttendanceTracker.Api/"]
COPY ["AttendanceTracker.Application/AttendanceTracker.Application.csproj", "AttendanceTracker.Application/"]
COPY ["AttendanceTracker.Domain/AttendanceTracker.Domain.csproj", "AttendanceTracker.Domain/"]
COPY ["AttendanceTracker.Infrastructure/AttendanceTracker.Infrastructure.csproj", "AttendanceTracker.Infrastructure/"]

RUN dotnet restore "AttendanceTracker.Api/AttendanceTracker.Api.csproj"

COPY . .

WORKDIR "/src/AttendanceTracker.Api"
RUN dotnet build "AttendanceTracker.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AttendanceTracker.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AttendanceTracker.Api.dll"]
