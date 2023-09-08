# MarketplaceAPI
ASP.NET Core REST API to query marketplace auctions

**Features**

- Pagination with page limit.
- Optional filtering (by status, by seller, by name), filtering by name is case insensitive, any position.
- Sorting (by key: CreatedDt, Price, by order: ASC, DESC).
- Optimize for searching by name.
- API versioning.

**Technologies**

- ASP.NET Core 6.0
- Entity Framework Core 6.0
- SQL Server

**Setup**

To run this project, install it locally using dotnet:
```
dotnet restore 
dotnet ef database update 
dotnet run
```

