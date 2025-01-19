using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using PetMateCoreHosted.Server.Data;
using PetMateCoreHosted.Server.Models;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Stripe;
using Microsoft.AspNetCore.SignalR;
using PetMateCoreHosted.Server.Hubs;
using Amazon.S3;

var builder = WebApplication.CreateBuilder(args);
StripeConfiguration.ApiKey = "sk_test_51OxaqmCQwNVZ7f3s5sLp2rHXU7629P2xZuxgUZPTi5pwFathIK23LsLLIcybQJFSgIsVyrkoWelfgMVpmHv9zCO3000YTiKswi";

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// adds all neccasary AWS services for application
var awsOptions = builder.Configuration.GetAWSOptions();
builder.Services.AddDefaultAWSOptions(awsOptions);
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();


builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy",
        builder =>
        {
            builder.WithOrigins("http://localhost:5250", "http://127.0.0.1:5500/datepicker.html")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
          new[] { "application/octet-stream" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseResponseCompression();
app.UseRouting();
app.MapHub<ChatHub>("/chathub");
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
