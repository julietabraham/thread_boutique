using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sample.Models;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<OnlineStoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineStoreContext")));
builder.Services.AddRazorPages();
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<OnlineStoreContext>();
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
var secretKey = builder.Configuration.GetSection("Stripe")["SecretKey"];
StripeConfiguration.SetApiKey(secretKey);
builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
