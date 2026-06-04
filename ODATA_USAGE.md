# OData Setup Complete

OData has been successfully added to your todolist-dotnet project!

## What Was Added

1. **NuGet Package**: Microsoft.AspNetCore.OData v9.1.0
2. **OData Configuration** in Program.cs with support for:
   - $select (choose specific fields)
   - $filter (filter results)
   - $orderby (sort results)
   - $expand (include related data)
   - $count (get total count)
   - $top/$skip (pagination)
3. **OData Controller**: TodosODataController.cs with full CRUD operations

## Endpoints

### OData Endpoint
- Base URL: `/odata/Todos`

### REST API Endpoint (existing)
- Base URL: `/api/todo`

## Example OData Queries

### Get all todos
```
GET /odata/Todos
```

### Get a specific todo by ID
```
GET /odata/Todos(5)
```

### Filter todos (only completed)
```
GET /odata/Todos?$filter=IsCompleted eq true
```

### Filter todos (by title contains)
```
GET /odata/Todos?$filter=contains(Title, 'meeting')
```

### Sort todos by creation date (descending)
```
GET /odata/Todos?$orderby=CreatedDate desc
```

### Select specific fields only
```
GET /odata/Todos?$select=Id,Title,IsCompleted
```

### Pagination (skip first 10, take next 5)
```
GET /odata/Todos?$skip=10&$top=5
```

### Get count of todos
```
GET /odata/Todos?$count=true
```

### Complex query (filter + sort + select)
```
GET /odata/Todos?$filter=IsCompleted eq false&$orderby=CreatedDate desc&$select=Id,Title,CreatedDate
```

### Filter by date range
```
GET /odata/Todos?$filter=CreatedDate ge 2024-01-01T00:00:00Z and CreatedDate le 2024-12-31T23:59:59Z
```

## CRUD Operations

### Create a new todo (POST)
```
POST /odata/Todos
Content-Type: application/json

{
  "Title": "New Todo",
  "Description": "Description here",
  "IsCompleted": false
}
```

### Update a todo (PUT - full update)
```
PUT /odata/Todos(5)
Content-Type: application/json

{
  "Id": 5,
  "Title": "Updated Todo",
  "Description": "Updated description",
  "IsCompleted": true,
  "CreatedDate": "2024-01-01T00:00:00Z",
  "CompletedDate": "2024-01-15T00:00:00Z"
}
```

### Partial update (PATCH)
```
PATCH /odata/Todos(5)
Content-Type: application/json

{
  "IsCompleted": true
}
```

### Delete a todo
```
DELETE /odata/Todos(5)
```

## OData Query Options Reference

| Option | Description | Example |
|--------|-------------|---------|
| $filter | Filter results | `$filter=IsCompleted eq true` |
| $orderby | Sort results | `$orderby=CreatedDate desc` |
| $top | Limit results | `$top=10` |
| $skip | Skip results | `$skip=5` |
| $select | Choose fields | `$select=Id,Title` |
| $expand | Include related data | `$expand=RelatedEntity` |
| $count | Include count | `$count=true` |

## Filter Operators

| Operator | Description | Example |
|----------|-------------|---------|
| eq | Equal | `Title eq 'Buy milk'` |
| ne | Not equal | `IsCompleted ne true` |
| gt | Greater than | `Id gt 5` |
| ge | Greater or equal | `CreatedDate ge 2024-01-01T00:00:00Z` |
| lt | Less than | `Id lt 10` |
| le | Less or equal | `CompletedDate le 2024-12-31T23:59:59Z` |
| and | Logical and | `IsCompleted eq true and Id gt 5` |
| or | Logical or | `IsCompleted eq true or Title eq 'Important'` |
| not | Logical not | `not IsCompleted` |
| contains | String contains | `contains(Title, 'meeting')` |
| startswith | String starts with | `startswith(Title, 'Buy')` |
| endswith | String ends with | `endswith(Title, 'today')` |

## Testing

You can test the OData endpoints using:
1. Browser: Navigate to `https://localhost:7xxx/odata/Todos`
2. Postman/Insomnia: Create requests with OData query parameters
3. curl:
```bash
curl "https://localhost:7xxx/odata/Todos?\$filter=IsCompleted eq false"
```

Note: In bash/curl, escape the `$` with `\$`

## OData Metadata

To view the OData service metadata document:
```
GET /odata/$metadata
```

This shows the data model structure in EDMX format.

## Benefits

- **Client-driven queries**: Clients can filter, sort, and paginate without custom endpoints
- **Standardized protocol**: OData is an ISO/IEC approved standard
- **Reduces API surface**: One endpoint supports many query patterns
- **Strongly typed**: Full IntelliSense support in clients that support OData

## Both APIs Available

Your project now has both:
- **REST API**: `/api/todo` (existing traditional REST endpoints)
- **OData API**: `/odata/Todos` (new OData-enabled endpoints)

Both can coexist and serve different use cases!
