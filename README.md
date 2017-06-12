# Http Query Filter

[![Build status](https://ci.appveyor.com/api/projects/status/id8it23ojpmcwlbb?svg=true)](https://ci.appveyor.com/project/junioro/http-query-filter)
[![Build Status](https://travis-ci.org/jroliveira/http-query-filter.svg?branch=master)](https://travis-ci.org/jroliveira/http-query-filter)
[![NuGet](https://img.shields.io/nuget/v/Http.Query.Filter.svg)](https://www.nuget.org/packages/Http.Query.Filter/)
[![NuGet](https://img.shields.io/nuget/dt/Http.Query.Filter.svg)](https://www.nuget.org/packages/Http.Query.Filter/)
[![CodeFactor](https://www.codefactor.io/repository/github/jroliveira/http-query-filter/badge)](https://www.codefactor.io/repository/github/jroliveira/http-query-filter)
[![License: MIT](http://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

![Http Query Filter - Logo][logo]

Project-based filter system [StrongLoop Node.js API Platform][strongloop] developed by [IBM Company][ibm].
* [API documentation](docs/README.md)

### Pre requirements

* Visual Studio 2017

### Installing

``` bash
$ git clone https://github.com/jroliveira/http-query-filter.git
$ dotnet restore
```

### Running tests

``` bash
$ dotnet test -c Release test/unit/Http.Query.Filter.Test/Http.Query.Filter.Test.csproj
$ dotnet test -c Release test/unit/Http.Query.Filter.Client.Test/Http.Query.Filter.Client.Test.csproj
$ dotnet test -c Release test/integration/Http.Query.Filter.Integration.Test/Http.Query.Filter.Integration.Test.csproj
```

### Building

``` bash
$ dotnet build
```

### How to use it

**REST API**

``` PowerShell
PM> Install-Package Http.Query.Filter
```

**Client API**

``` PowerShell
PM> Install-Package Http.Query.Filter.Client
```

### Contributions

1. Fork it
2. git checkout -b <branch-name>
3. git add -A && git commit -m "feature description"
4. git push origin <branch-name>
5. Create a pull request

### License

The code is available under the [MIT license](LICENSE).

[strongloop]: https://strongloop.com/
[ibm]: http://www.ibm.com/
[logo]: https://raw.githubusercontent.com/jroliveira/http-query-filter/master/docs/logo.png "Http Query Filter - Logo"