# Implementation record

## Production code

`CatalogService.Search()` now uses fixed SQL command text:

```csharp
command.CommandText = "SELECT name FROM products WHERE name ILIKE @query ORDER BY name";
command.Parameters.AddWithValue("query", $"%{query}%");
```

The wildcard characters used for substring matching are part of the parameter value. User content is no longer inserted into the SQL command text.

## Regression coverage

The existing catalog test was expanded rather than adding a new test, preserving the requested 20-test total. It covers:

- Existing case-insensitive search behavior.
- Harmless SQL-shaped input.
- A quote character that previously broke command construction.

## Files changed

- [`CatalogService.cs`](../../cartsvc-dotnet/src/CartSvc/CatalogService.cs)
- [`ServiceTests.cs`](../../cartsvc-dotnet/tests/CartSvc.Tests/ServiceTests.cs)
