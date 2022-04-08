using System.Reflection;
using Microsoft.OpenApi.Models;
using Wolfpack.Api.Filters;
using Wolfpack.Business;
using Wolfpack.Data.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.DescribeAllParametersInCamelCase();
    options.SchemaFilter<EnumSchemaFilter>();

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Wolfpack API",
        Description = "Wolfpack pack manager API",
        Contact = new OpenApiContact
        {
            Name = "Wolfpack IT",
            Email = "join@wolfpackit.nl",
            Url = new Uri("https://jointhewolfpack.nl"),
        },
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services
    .AddBusiness(builder.Configuration)
    .AddDatabase(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// HTTPS redirection is off in this project but should be turned on in production.
// app.UseHttpsRedirection();

app.MapControllers();

app
    .MigrateDatabase();

app.Run();