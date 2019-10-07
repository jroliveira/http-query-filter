FROM mcr.microsoft.com/dotnet/core/sdk:3.0

# install cakebuild 0.35.0
RUN dotnet tool install --global Cake.Tool --version 0.35.0
ENV PATH="${PATH}:/root/.dotnet/tools"

WORKDIR /build
COPY . .

RUN dotnet cake
