using Data_Logic_Layer;
using Service_Logic_Layer;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IDataService, SqlData>();  
builder.Services.AddScoped<CartService>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();