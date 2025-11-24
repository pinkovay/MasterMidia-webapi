using FluentValidation.AspNetCore;
using MasterMidia.Api.Middlewares;
using MasterMidia.App.Infrastructure;
using MasterMidia.App.Infrastructure.Storage;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Master Midia API", Version = "v1" });

    c.TagActionsBy(api =>
        (IList<string>)(api.ActionDescriptor.EndpointMetadata
           .OfType<TagsAttribute>()
           .FirstOrDefault()?.Tags 
        ?? [api.ActionDescriptor.RouteValues["controller"]!]));

    var xmlFile = "MasterMidia.App.xml"; 
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira o token JWT no formato 'Bearer {seu_token}'",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options => options.AddDefaultPolicy(
        policy => policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()));

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddFirebaseStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication(); 
app.UseAuthorization(); 

app.MapControllers();
// app.UseAuthorization();

app.Run();
