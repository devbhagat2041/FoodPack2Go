using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Implement;
using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Register Connection string

builder.Services.AddDbContext<MyAppDbContext>(options => options.UseSqlServer
(builder.Configuration.GetConnectionString("foodpack")));

//Dependency Injections 

builder.Services.AddSession();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();

builder.Services.AddScoped<ISub, Sub>();
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<ISliderRepository, SliderRepository>();
builder.Services.AddScoped<ICategoylistRepository, CategoylistRepository>();
builder.Services.AddScoped<IBannerRepository, BannerRepository>();
builder.Services.AddScoped<IMasterHomePageRepo, MasterHomePageRepo>();
builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<IContactRepo, ContactRepo>();
builder.Services.AddScoped<IOrderManage, OrderManage>();
builder.Services.AddScoped<IFooterRepo, FooterRepo>();
builder.Services.AddScoped<IAboutUsRepo, AboutUsRepo>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();









var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();







