using Wolfpack.Data.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddDatabase(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// HTTPS redirection is off in this project but should be turned on in production.
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app
    .MigrateDatabase();

app.Run();
