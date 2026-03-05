using Todo_Service.Options;
using Todo_Service.Strategies;
using Todo_Service.Factory;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); builder.Services.AddSwaggerGen();
builder.Configuration.GetSection("ConnectionStrings").Get<DefaultConnectionOption>();
builder.Services.AddSingleton<IDbConnectionStrategy, SqlServerDefaultStrategy>();
builder.Services.AddSingleton<DbConnectionFactory>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUI();
    app.UseSwagger();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
