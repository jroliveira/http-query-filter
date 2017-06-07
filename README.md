# Http Query Filter

[![Build status](https://ci.appveyor.com/api/projects/status/id8it23ojpmcwlbb?svg=true)](https://ci.appveyor.com/project/junioro/http-query-filter)
[![Build Status](https://travis-ci.org/jroliveira/http-query-filter.svg?branch=master)](https://travis-ci.org/jroliveira/http-query-filter)
[![NuGet](https://img.shields.io/nuget/v/Http.Query.Filter.svg)](https://www.nuget.org/packages/Http.Query.Filter/)
[![NuGet](https://img.shields.io/nuget/dt/Http.Query.Filter.svg)](https://www.nuget.org/packages/Http.Query.Filter/)
[![CodeFactor](https://www.codefactor.io/repository/github/jroliveira/http-query-filter/badge)](https://www.codefactor.io/repository/github/jroliveira/http-query-filter)
[![License: MIT](http://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

![Http Query Filter - Logo][logo]

Sistema de filtro baseado no projeto [StrongLoop Node.js API Platform][strongloop] desenvolvido pela [IBM Company][ibm].

## Features

### Limit

Retorna apenas a quantidade de dados passada no filtro ou menos dados.

Exemplo:

```
/accounts?filter[limit]=5
```

### Skip

Retorna apenas os dados ignorando os primeiros registros retornados na consulta. Geralmente é utilizado para paginação.

Exemplo:

```
/accounts?filter[skip]=1
```

### Order

Retorna os dados ordenados de forma crescente (asc) ou decrescente(desc) com base em um propriedade especifica.

Exemplos:

De forma CRESCENTE:

```
/accounts?filter[order]=name%20asc
```

De forma DECRESCENTE:

```
/accounts?filter[order]=name%20desc
```

De forma crescente E decrescente:

```
/accounts?filter[order][0]=id%20asc&filter[order][1]=name%20desc
```

### Where

Retorna os dados respeitando as condições solicitadas.

Exemplos:

Onde for IGUAL:

```
/accounts?filter[where][id]=100
```

Onde for MAIOR QUE:

```
/accounts?filter[where][id][gt]=100
```

Onde for MENOR QUE:

```
/accounts?filter[where][id][lt]=100
```

### Fields

Retorna os dados respeitando a configuração de visualização de cada campo.

obs.: A coluna por padrão será retornada na consulta.

Exemplos:

Onde for VERDADEIRO:

```
/accounts?filter[fields][id]=true
```

Onde for FALSO:

```
/accounts?filter[fields][id]=false
```

## Pre requirements

* Visual Studio 2017

## Installing

``` bash
$ git clone https://github.com/jroliveira/http-query-filter.git
$ dotnet restore
```

### Running tests

``` bash
$ dotnet test -c Release test/unit/Http.Query.Filter.Test/Http.Query.Filter.Test.csproj
```

## Building

``` bash
$ dotnet build
```

## How to use it

``` PowerShell
PM> Install-Package Http.Query.Filter
```

## Contributions

1. Fork it
2. git checkout -b <branch-name>
3. git add -A && git commit -m "feature description"
4. git push origin <branch-name>
5. Create a pull request

[strongloop]: https://strongloop.com/
[ibm]: http://www.ibm.com/
[logo]: https://raw.githubusercontent.com/jroliveira/http-query-filter/master/logo.png "Http Query Filter - Logo"