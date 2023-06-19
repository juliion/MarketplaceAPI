using Microsoft.EntityFrameworkCore;
using MarketplaceDAL;
using MarketplaceBLL.Mappings;
using MarketplaceBLL.Interfaces;
using MarketplaceAPI.Mappings;
using FluentValidation;
using MarketplaceAPI.Validators.Items;
using MarketplaceBLL.Services;
using MarketplaceBLL.Helpers;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MarketDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
});

builder.Services.AddAutoMapper(typeof(EntityToDTOProfile));
builder.Services.AddAutoMapper(typeof(DTOToViewModelProfile));

builder.Services.AddValidatorsFromAssemblyContaining<CreateItemValidator>();

builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped(typeof(ISortingHelper<>), typeof(SortingHelper<>));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
