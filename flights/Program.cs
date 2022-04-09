using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using flights.Areas.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using flights.Services;
using flights.Context;
using flights.Entity;


using Stripe;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);




var connectionString = builder.Configuration.GetConnectionString("flightsContextConnection");
builder.Services.AddDbContext<flightsContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDbContext<FlightsystemdbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDefaultIdentity<flightsUser>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;

    options.SignIn.RequireConfirmedAccount = true;
}).
    AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<flightsContext>();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole",
         policy => policy.RequireRole("administrator"));
});
builder.Services.AddAuthentication()
   .AddGoogle(options =>
   {
       IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");

       options.ClientId = googleAuthNSection["ClientId"];
       options.ClientSecret = googleAuthNSection["ClientSecret"];
   });
   //.AddFacebook(
   // FBoption =>
   // {
   //     IConfigurationSection FacebookAuthNSection = builder.Configuration.GetSection("Authentication:Facebook");
   //     FBoption.AppId = FacebookAuthNSection["AppId"];
   //     FBoption.AppSecret = FacebookAuthNSection["AppSecret"];
   // });



//builder.Services.AddAuthorization(options => { options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); });
// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(o => o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<flightsUser>, UserClaimsPrincipalFactory<flightsUser, IdentityRole>>();
builder.Services.AddScoped<ITicketRepositary,TicketRepoService>();
builder.Services.AddScoped<IflightRepositary,flightRepoService>();
builder.Services.AddScoped<IairplaneRepositary,airplaneRepoService>();
builder.Services.AddScoped<IAirlineRepositary,AirlineRepoService>();
builder.Services.AddScoped<IUserRepositary, UserRepoService>();
builder.Services.AddScoped<IcountryRepositary, countryRepoService>();

var app = builder.Build();

StripeConfiguration.SetApiKey("sk_test_51Kkcs8EpWjtsLV5SSXJRy4KDGrf0ulUosk9wCrnIFkVwEXLF4aCApdoyqdudG4lUHZ9VCpPOYKlwvfqBhYuuFHVj00bQRMYPOr");


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();

    app.UseStatusCodePagesWithRedirects("/Error/{0}");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
//app.MapControllerRoute(
//          name: "flightsAdmin",
//    pattern: "{area}/{controller=Home}/{action=Index2}/{id?}");

// pattern: "{ area: exists = Admin}/{ controller = Home}/{ action = Index}/{ id ?}");
app.MapControllerRoute(
     //name: "default",
     //pattern: "{area=flights}/{controller=flight}/{action=show}/{id?}");
     name: "blog",
        pattern: "{Area}/{controller}/{action}/{id?}",
        defaults: new { Area = "flights", controller = "flight", action = "show" });
app.MapControllerRoute(
 name: "default",
     pattern: "{controller}/{action}/{id?}",
        defaults: new { controller = "Admin", action = "Create" });
app.MapRazorPages();

FlightsystemdbContext context = new FlightsystemdbContext();
visitor visitor = new visitor() { num = 1 };
context.visitors.Add(visitor);
context.SaveChanges();
app.Run();
