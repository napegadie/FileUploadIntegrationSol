using Azure;
using Azure.Identity;
using Azure.Messaging.EventGrid;
using Azure.Messaging.ServiceBus;
using FileUploadIntegrationPrj.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using System.Net.Http.Headers;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        builder =>
        {
            builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("https://localhost:7283", "https://csacontextblob.blob.core.windows.net");
        });
});

builder.Services.AddAzureClients(builderazure =>
{
    builderazure.AddBlobServiceClient(builder.Configuration["ConnectionStrings:AzureBlobStorage"]);

    //using AMQP as transport
    builder.Services.AddSingleton((s) => {
        return new ServiceBusClient(builder.Configuration.GetConnectionString("ServiceBus"),
            new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpTcp,
                RetryOptions = new ServiceBusRetryOptions()
                {
                    Mode = ServiceBusRetryMode.Exponential,
                    Delay = TimeSpan.FromMilliseconds(50),
                    MaxDelay = TimeSpan.FromSeconds(5),
                    MaxRetries = 3
                }

            });
    });

    builder.Services.AddSingleton((s) => {
        return new EventGridPublisherClient(new Uri(builder.Configuration.GetConnectionString("EGEndpoint")),
               new AzureKeyCredential(builder.Configuration.GetConnectionString("EGKey")));
    });


    // Use DefaultAzureCredential by default
    builderazure.UseCredential(new DefaultAzureCredential());

});

builder.Services.AddDbContext<BookSearchDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
