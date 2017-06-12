## API documentation

Jump to:
  [limit](#limit) |
  [skip](#skip) |
  [order](#order) |
  [where](#where) |
  [fields](#fields)

### Limit

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
    .BuildAsync();
```

Where *n* is the maximum number of results (records) to return.

### Skip

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
    .BuildAsync();
```

Where *n* is the number of records to skip.

### Order

An *order* filter specifies how to sort the results: ascending (ASC) or descending (DESC) based on the specified property.

**REST API**

`ASC`

```
?filter[order]=propertyName%20ASC
```

or `DESC`

```
?filter[order]=propertyName%20DESC
```

Order by two or more properties:

```
?filter[order][0]=propertyName%20ASC&filter[order][1]=propertyName%20DESC
```

Where *propertyName* is the name of the property (field) to sort by. 

### Where

A *where* filter specifies a set of logical conditions to match, similar to a WHERE clause in a SQL query.

**REST API**

`equal`

```
?filter[where][propertyName]=value
```

or `greater than (gt)`

```
?filter[where][propertyName][gt]=value
```

or `less than (lt)`

```
?filter[where][propertyName][lt]=value
```

Where:

 - *propertyName* is the name of a property (field) in the model being queried.
 - *value* is a literal value.

### Fields

A *fields* filter specifies properties (fields) to include from the results.

**REST API**

```
?filter[fields][propertyName]=true
```

Order by two or more properties:

```
?filter[fields][propertyName]=true&filter[fields][propertyName]=true
```

**Client API**

``` cs
var data = client
    .GetAll()
    .Select(propertyName)
    .BuildAsync();
```

Order by two or more properties:

``` cs
var data = client
    .GetAll()
    .Select(propertyName, propertyName)
    .BuildAsync();
```

Where *propertyName* is the name of the property (field) to include.

By default, queries return all model properties in results. However, if you specify at least one fields filter with a value of `true`, then by default the query will include **only** those you specifically include with filters.