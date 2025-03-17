using Business.DependencyResolvers;
using Business;
using Entities;
using OfficeOpenXml;
using Business.Concretes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailService, MailManager>();
builder.Services.AddBusinessServices(builder.Configuration);
builder.Services.AddHostedService<DomainNotificationWorker>();
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
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
    pattern: "{controller=Domain}/{action=Index}/{id?}");

app.Run();
