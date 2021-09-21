#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Gaia.ToDoList.Api/Gaia.ToDoList.Api.csproj", "Gaia.ToDoList.Api/"]
RUN dotnet restore "Gaia.ToDoList.Api/Gaia.ToDoList.Api.csproj"
COPY . .
WORKDIR "/src/Gaia.ToDoList.Api"
RUN dotnet build "Gaia.ToDoList.Api.csproj" -c Release -o /app/build

RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh

FROM build AS publish
RUN dotnet publish "Gaia.ToDoList.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gaia.ToDoList.Api.dll"]