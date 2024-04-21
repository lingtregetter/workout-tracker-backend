FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY *.props .
COPY *.sln .

# copy all the projects from host to image
# Base
COPY Base.BLL.Contracts/*.csproj ./Base.BLL.Contracts/
COPY Base.Contracts/*.csproj ./Base.Contracts/
COPY Base.DAL.Contracts/*.csproj ./Base.DAL.Contracts/
COPY Base.Domain.Contracts/*.csproj ./Base.Domain.Contracts/
COPY Base.BLL/*.csproj ./Base.BLL/
COPY Base.DAL/*.csproj ./Base.DAL/
COPY Base.DAL.EF/*.csproj ./Base.DAL.EF/
COPY Base.Domain/*.csproj ./Base.Domain/
COPY Base.Helpers/*.csproj ./Base.Helpers/

# App
COPY App.BLL.Contracts/*.csproj ./App.BLL.Contracts/
COPY App.DAL.Contracts/*.csproj ./App.DAL.Contracts/
COPY App.BLL/*.csproj ./App.BLL/
COPY App.BLL.DTO/*.csproj ./App.BLL.DTO/
COPY App.DAL.DTO/*.csproj ./App.DAL.DTO/
COPY App.DAL.EF/*.csproj ./App.DAL.EF/
COPY App.Domain/*.csproj ./App.Domain/
COPY App.Public.DTO/*.csproj ./App.Public.DTO/

COPY Tests/*.csproj ./Tests/

COPY WebApp/*.csproj ./WebApp/

RUN dotnet restore

# copy everything else and build app
# Base
COPY Base.BLL.Contracts/. ./Base.BLL.Contracts/
COPY Base.Contracts/. ./Base.Contracts/
COPY Base.DAL.Contracts/. ./Base.DAL.Contracts/
COPY Base.Domain.Contracts/. ./Base.Domain.Contracts/
COPY Base.BLL/. ./Base.BLL/
COPY Base.DAL/. ./Base.DAL/
COPY Base.DAL.EF/. ./Base.DAL.EF/
COPY Base.Domain/. ./Base.Domain/
COPY Base.Helpers/. ./Base.Helpers/

# App
COPY App.BLL.Contracts/. ./App.BLL.Contracts/
COPY App.DAL.Contracts/. ./App.DAL.Contracts/
COPY App.BLL/. ./App.BLL/
COPY App.BLL.DTO/. ./App.BLL.DTO/
COPY App.DAL.DTO/. ./App.DAL.DTO/
COPY App.DAL.EF/. ./App.DAL.EF/
COPY App.Domain/. ./App.Domain/
COPY App.Public.DTO/. ./App.Public.DTO/

COPY Tests/. ./Tests/

COPY WebApp/. ./WebApp/

WORKDIR /src/WebApp
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim-amd64 AS runtime
WORKDIR /app
EXPOSE 80

COPY --from=build /src/WebApp/out ./

ENTRYPOINT ["dotnet", "WebApp.dll"]