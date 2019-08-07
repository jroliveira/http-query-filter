FROM mcr.microsoft.com/dotnet/core/sdk:2.2

# install cakebuild 0.33.0
RUN dotnet tool install --global Cake.Tool --version 0.33.0
ENV PATH="${PATH}:/root/.dotnet/tools"

WORKDIR /build
COPY . .

RUN dotnet cake
