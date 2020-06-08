![Http Query Filter - Logo][logo]

# Http Query Filter

[![Build status](https://ci.appveyor.com/api/projects/status/id8it23ojpmcwlbb?svg=true)](https://ci.appveyor.com/project/junioro/http-query-filter)
[![Build Status](https://travis-ci.org/jroliveira/http-query-filter.svg?branch=master)](https://travis-ci.org/jroliveira/http-query-filter)
[![NuGet](https://img.shields.io/nuget/v/Http.Query.Filter.svg)](https://www.nuget.org/packages/Http.Query.Filter/)
[![NuGet](https://img.shields.io/nuget/dt/Http.Query.Filter.svg)](https://www.nuget.org/packages/Http.Query.Filter/)
[![CodeFactor](https://www.codefactor.io/repository/github/jroliveira/http-query-filter/badge)](https://www.codefactor.io/repository/github/jroliveira/http-query-filter)
[![Maintainability](https://api.codeclimate.com/v1/badges/62b3f82f9fc66560bbd7/maintainability)](https://codeclimate.com/github/jroliveira/http-query-filter/maintainability)
[![License: MIT](http://img.shields.io/badge/license-MIT-blue.svg)](LICENSE.txt)

Project-based filter system [LoopBack Querying data][loopback] developed by [IBM Company][ibm] and [Resource Query Language (RQL)][rql].

## Installing / Getting started

``` bash
# Install package
$ dotnet add package Http.Query.Filter
```

## Developing

### Built With

 - [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/)
 - [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
 - [Node.js](https://nodejs.org/en/)

### Pre requisites

Download and install:

 - [.NET Core SDK](https://www.microsoft.com/net/download)
 - [Node.js](https://nodejs.org/en/download/)

### Setting up Dev

``` bash
# Clone this repository
$ git clone https://github.com/jroliveira/http-query-filter.git

# Go into the repository
$ cd http-query-filter

# Download node packages and install Cake
$ npm install
```

### Building

``` bash
$ dotnet cake
```

or simulating ci 

``` bash
$ dotnet cake --target=Release --simulating-ci
```

or with docker

``` bash
$ docker build --tag http-query-filter .
```

### Testing

``` bash
$ dotnet cake
```

or

``` bash
$ dotnet test
```

### Releasing

You must create a file `ci-env.json` on the path `./build/` with the context below.  
This file is used to set the configuration to run the command below.

``` json
{
  "nuget": {
    "apiKey": "<your_nuget_api_key>"
  }
}
```

``` bash
$ dotnet cake --target=Release
```

## Api Reference

[Documentation](docs/README.md)

## Licensing

The code is available under the [MIT license](LICENSE.txt).

[loopback]: https://loopback.io/doc/en/lb4/Querying-data.html
[ibm]: http://www.ibm.com/
[logo]: docs/logo.png "Http Query Filter - Logo"
[rql]: https://github.com/persvr/rql
