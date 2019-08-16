## API documentation

Jump to:
  [limit](#limit) |
  [skip](#skip) |
  [order](#order) |
  [where](#where) |
  [fields](#fields)

### Limit [&uarr;](#api-documentation)

A *limit* filter limits the number of records returned to the specified number (or less).

**REST API**

```
?filter[limit]=n
```

**Client API**

``` cs
var data = client
    .GetAll()
    .Limit(n)
    .Build();
```

Where *n* is the maximum number of results (records) to return.

### Skip [&uarr;](#api-documentation)

A *skip* filter omits the specified number of returned records. This is useful, for example, to paginate responses.

**REST API**

```
?filter[skip]=n
```

**Client API**

``` cs
var data = client
    .GetAll()
    .Skip(n)
    .Build();
```

Where *n* is the number of records to skip.

### Order [&uarr;](#api-documentation)

An *order* filter specifies how to sort the results: ascending (ASC) or descending (DESC) based on the specified property.

#### by `ASC`

**REST API**

```
?filter[order]=property ASC
```

#### by `DESC`

**REST API**

```
?filter[order]=property DESC
```

#### by `ASC` and `DESC`

Order by two or more properties:

**REST API**

```
?filter[order][0]=property ASC&filter[order][1]=property DESC
```

Where *property* is the name of the property (field) to sort by. 

### Where [&uarr;](#api-documentation)

A *where* filter specifies a set of logical conditions to match, similar to a WHERE clause in a SQL query.

#### Comparison Operators

##### Equivalence `=`

**REST API**

```
?filter[where][property]=value
```

**Client API**

``` cs
var data = client
    .GetAll()
    .Where("property".Equal("value"))
    .Build();
```

##### Greater than `gt`

**REST API**

```
?filter[where][property][gt]=value
```

**Client API**

``` cs
var data = client
    .GetAll()
    .Where("property".GreaterThan(value))
    .Build();
```

##### Less than `lt`

**REST API**

```
?filter[where][property][lt]=value
```

**Client API**

``` cs
var data = client
    .GetAll()
    .Where("property".LessThan(value))
    .Build();
```

Where:

 - *property* is the name of a property (field) in the model being queried.
 - *value* is a literal value. 

#### Logical Operators

##### And `and`

**REST API**

```
?filter[where][and][0][property]=value&filter[where][and][1][property][gt]=value
```

**Client API**

&#9888; in development!

``` cs
var data = client
    .GetAll()
    .Where("property".Equal("value")
        .And("property".GreaterThan(value)))
    .Build();
```

##### Or `or`

**REST API**

```
?filter[where][or][0][property]=value&filter[where][or][1][property][lt]=value
```

**Client API**

&#9888; in development!

``` cs
var data = client
    .GetAll()
    .Where("property".Equal("value")
        .Or("property".LessThan(value)))
    .Build();
```

##### And/Or

&#9888; in development!

**REST API**

```
?filter[where][or][0][property][gt]=value&filter[where][and][1][property]=value&filter[where][and][2][property][gt]=value
```

**Client API**

``` cs
var data = client
    .GetAll()
    .Where("property".GreaterThan(value)
        .Or("property".Equal("value")
            .And("property".GreaterThan(value))))
    .Build();
```

Where:

 - *property* is the name of a property (field) in the model being queried.
 - *value* is a literal value. 

### Fields [&uarr;](#api-documentation)

A *fields* filter specifies properties (fields) to include from the results.

**REST API**

```
?filter[fields][property]=true
```

Order by two or more properties:

```
?filter[fields][property]=true&filter[fields][property]=true
```

**Client API**

``` cs
var data = client
    .GetAll()
    .Select(property)
    .Build();
```

Order by two or more properties:

``` cs
var data = client
    .GetAll()
    .Select(property, property)
    .Build();
```

Where *property* is the name of the property (field) to include.

By default, queries return all model properties in results. However, if you specify at least one fields filter with a value of `true`, then by default the query will include **only** those you specifically include with filters.
