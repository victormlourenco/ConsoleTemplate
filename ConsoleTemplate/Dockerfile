#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Test/ConsoleTemplate.csproj", "Test/"]
COPY ["ConsoleTemplate.Service/ConsoleTemplate.Service.csproj", "ConsoleTemplate.Service/"]
COPY ["ConsoleTemplate.Models/ConsoleTemplate.Model.csproj", "ConsoleTemplate.Models/"]
COPY ["ConsoleTemplate.Crosscutting.IoC/ConsoleTemplate.Crosscutting.IoC.csproj", "ConsoleTemplate.Crosscutting.IoC/"]
RUN dotnet restore "Test/ConsoleTemplate.csproj"
COPY . .
WORKDIR "/src/Test"
RUN dotnet build "ConsoleTemplate.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ConsoleTemplate.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConsoleTemplate.dll"]