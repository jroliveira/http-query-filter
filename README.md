![Http Query Filter - Logo][logo]

# Http Query Filter

[![Build status](https://ci.appveyor.com/api/projects/status/id8it23ojpmcwlbb?svg=true)](https://ci.appveyor.com/project/junioro/http-query-filter)
[![Build Status](https://travis-ci.org/jroliveira/http-query-filter.svg?branch=master)](https://travis-ci.org/jroliveira/http-query-filter)
[![NuGet](https://img.shields.io/nuget/v/Http.Query.Filter.svg)](https://www.nuget.org/packages/Http.Query.Filter/)
[![NuGet](https://img.shields.io/nuget/dt/Http.Query.Filter.svg)](https://www.nuget.org/packages/Http.Query.Filter/)
[![CodeFactor](https://www.codefactor.io/repository/github/jroliveira/http-query-filter/badge)](https://www.codefactor.io/repository/github/jroliveira/http-query-filter)
[![License: MIT](http://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

Project-based filter system [StrongLoop Node.js API Platform][strongloop] developed by [IBM Company][ibm].

## Installing / Getting started

```bash
# Install package
$ dotnet add package Http.Query.Filter
```

## Developing

### Built With

 - [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/)
 - [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)

### Pre requisites

Download and install:

 - [.NET Core SDK](https://www.microsoft.com/net/download)

#### Installing the Cake

[Cake](https://github.com/cake-build/cake) (C# Make) is a cross-platform build automation system with a C# DSL for tasks such as compiling code, copying files and folders, running unit tests, compressing files and building NuGet packages.

```bash
$ dotnet tool install -g Cake.Tool --version 0.30.0
```

### Setting up Dev

```bash
# Clone this repository
$ git clone https://github.com/jroliveira/http-query-filter.git

# Go into the repository
$ cd http-query-filter
```

### Building

```bash
$ dotnet cake ./cakebuild/build.cake
```

or

```bash
# Restore dependencies
$ dotnet restore

# Build project
$ dotnet build
```

### Deploying / Publishing

``` bash
$ dotnet cake ./cakebuild/build.cake --target=Deploy
```

## Tests

``` bash
$ dotnet test
```

## Api Reference

[Documentation](docs/README.md)

## Licensing

The code is available under the [MIT license](LICENSE).

[strongloop]: https://strongloop.com/
[ibm]: http://www.ibm.com/
[logo]: docs/logo.png "Http Query Filter - Logo"