# Source inspection evidence

## Observed vulnerable implementation

Before the correction, `CatalogService.Search()` built SQL using C# string interpolation:

```csharp
command.CommandText = $"SELECT name FROM products WHERE name ILIKE '%{query}%' ORDER BY name";
```

The caller-provided `query` value became part of PostgreSQL command syntax.

## Existing coverage

The original test asserted only a normal case-insensitive search:

```csharp
[Fact] public void CatalogSearchIgnoresCase() =>
    Assert.Equal(new[] { "Coffee" }, new CatalogService(db).Search("COF"));
```

It did not exercise quotes or SQL-shaped text.

## Evidence-supported root cause

When the search contains SQL-significant characters, string interpolation changes the database command text. The harmless input `' OR 1=1 --` changed the predicate and returned every seeded product. The failing test output is preserved in [`01-regression-before.txt`](../05-validate/01-regression-before.txt).

## Scope boundaries

- In scope: parameterize catalog search and add focused regression coverage.
- Out of scope: unrelated checkout, coupon, pricing, logging, schema, and price-refresh defects.
- No production environment was inspected.
