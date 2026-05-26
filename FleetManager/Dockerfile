# -----------------------------------------
# STAGE 1: The Base Runtime (What actually runs on the server)
# -----------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER app
WORKDIR /app
EXPOSE 5000

# -----------------------------------------
# STAGE 2: The Build Environment (Heavy, contains the compiler)
# -----------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy only the project file first to cache the NuGet restore step!
COPY ["FleetManager.csproj", "./"]
RUN dotnet restore "./FleetManager.csproj"

# Now copy the rest of your code and build it
COPY . .
RUN dotnet build "FleetManager.csproj" -c Release -o /app/build

# -----------------------------------------
# STAGE 3: Publish
# -----------------------------------------
FROM build AS publish
RUN dotnet publish "FleetManager.csproj" -c Release -o /app/publish /p:UseAppHost=false

# -----------------------------------------
# STAGE 4: Final Image Assembly
# -----------------------------------------
FROM base AS final
WORKDIR /app

# Copy the finished, compiled files from Stage 3 into our tiny Stage 1 box
COPY --from=publish /app/publish .

# Tell the server exactly how to start the app!
ENTRYPOINT ["dotnet", "FleetManager.dll"]