using Mango.Web.Service;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// config for httpclient
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
// config for couponservice to use httpclient
builder.Services.AddHttpClient<ICouponService, CouponService>();   

// for CouponAPI base Url
StaticDetails.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];
// register for DI
builder.Services.AddScoped<IBaseService, BaseService>();    
builder.Services.AddScoped<ICouponService, CouponService>();


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
