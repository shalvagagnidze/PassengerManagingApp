using Azure.Identity;
using Azure.Extensions.AspNetCore.Configuration.Secrets; // Add this using directive
using Infrastructure;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

//static void ConfigureAzureKeyVault(WebApplicationBuilder builder)
//{
//    if (builder.Environment.IsProduction() || builder.Environment.IsStaging())
//    {
//        var keyVaultUrl = builder.Configuration["https://passengerapp.vault.azure.net/"];

//        // Use DefaultAzureCredential for RBAC
//        var credential = new DefaultAzureCredential(
//            new DefaultAzureCredentialOptions
//            {
//                // Optional: Specify tenant if needed
//                // TenantId = "your-tenant-id"
//            }
//        );

//        builder.Configuration.AddAzureKeyVault(
//                new Uri(keyVaultUrl),
//                credential,
//                new KeyVaultSecretManager()
//            );
//    }
//}
//ConfigureAzureKeyVault(builder);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

//app.UseHealthChecks("/health");

app.Run();
