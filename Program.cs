using Todo_Service.Options;
using Todo_Service.Strategies;
using Todo_Service.Factory;
using Todo_Service.Repositories;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); builder.Services.AddSwaggerGen();
builder.Services.Configure<DefaultConnectionOption>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddSingleton<IDbConnectionStrategy, SqlServerDefaultStrategy>();
builder.Services.AddSingleton<DbConnectionFactory>();
builder.Services.AddSingleton<ITodoRepository , TodoRepository>();
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
