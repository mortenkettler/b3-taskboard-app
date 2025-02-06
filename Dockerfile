FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["TaskBoard.Api/TaskBoard.Api.csproj", "TaskBoard.Api/"]
COPY ["TaskBoard.Application/TaskBoard.Application.csproj", "TaskBoard.Application/"]
COPY ["TaskBoard.Domain/TaskBoard.Domain.csproj", "TaskBoard.Domain/"]
COPY ["TaskBoard.Infrastructure/TaskBoard.Infrastructure.csproj", "TaskBoard.Infrastructure/"]
RUN dotnet restore "TaskBoard.Api/TaskBoard.Api.csproj"

COPY . .
WORKDIR "/src/TaskBoard.Api"
RUN dotnet publish "TaskBoard.Api.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS="http://0.0.0.0:8080"
EXPOSE 8080
EXPOSE 443

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "TaskBoard.Api.dll"]
