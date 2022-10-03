//using FluentValidation;
//using FluentValidation.AspNetCore;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using TNS_store2.Models;
//using TNS_store2.Models.Validators;
//using TNS_store2.Services;

//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddSession();

//// Add services to the container.
////var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
////builder.Services.AddDbContext<ApplicationDbContext>(options =>
////    options.UseSqlServer(connectionString));

////builder.Services.AddDatabaseDeveloperPageExceptionFilter();

////builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
////    .AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages();


//var SqlServer = builder.Configuration.GetConnectionString("SqlServer");
//builder.Services.AddDbContext<TnsDbContext>(options =>
//    options.UseSqlServer(builder.Configuration["ConnectionStrings:SqlServer"]));

//builder.Services.AddTransient<ICartService, CartService>();
//builder.Services.AddFluentValidation();
//builder.Services.AddTransient<IValidator, ProductValidator>();
//builder.Services.AddTransient<IValidator, OrderValidator>();

//builder.Services.AddTransient<IMailSender, MailSender>();

//builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
////OR
//builder.Services.AddControllers(options => options.EnableEndpointRouting = false);

//builder.Services.AddWebOptimizer(options =>
//{
//    options.CompileScssFiles();
//});

//var app = builder.Build();
//app.UseStaticFiles();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseMigrationsEndPoint();
//}
//else
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}
////app.UseMvc();
//app.UseSession();
//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();
//app.UseWebOptimizer();

//app.UseAuthentication();
//app.UseAuthorization();


//app.UseMvc(endpoints =>
//{
//    endpoints.MapRoute(
//        name: "search",
//        template: "Search/{controller=Home}/{action=Search}/{search}/{priceMin?}/{priceMax?}");


//});

////app.MapControllerRoute(
////            name: "areas",
////            pattern: "{area:exists}/{controller=Categories}/{action=Index}/{id?}"
////          );

////app.MapControllerRoute(
////    name: "default",
////    pattern: "{controller=Home}/{action=Index}/{id?}");





//app.Run();



using TNS_store2.Models;
using TNS_store2.Models.Validators;
using TNS_store2.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TnsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:SqlServer"]);
});

builder.Services.AddFluentValidation();
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
}
);
builder.Services.AddTransient<ICartService, CartService>();
builder.Services.AddTransient<IMailSender, MailSender>();
builder.Services.AddTransient<IHistoryService, HistoryService>();

builder.Services.AddWebOptimizer(options =>
{
    options.CompileScssFiles();
});

var app = builder.Build();

// Configure the HTTP request pipeline.

var supportedCulture = new[]
{
   // new CultureInfo("en-US"),
    new CultureInfo("en"),
//   new CultureInfo("ru-RU"),
    new CultureInfo("ru")
};

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("ru"),
    SupportedCultures = supportedCulture,
    SupportedUICultures = supportedCulture
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseWebOptimizer();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    //    endpoints.MapControllerRoute(
    //  name: "makeOrder",
    //  pattern: "MakeOrder/{controller=Orders}/{action=Create}/{id:int}"
    //);

    //app.UseEndpoints(endpoints =>
    //{
    //    endpoints.MapControllerRoute(
    //  name: "makeOrder",
    //  pattern: "MakeOrder/{controller=Orders}/{action=Create}/{id:type}" 
    //------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------
    // type =:required.long.guid.:maxlength(8).:length(2,5).:min(5)/:max(10),:range(40,50),:regex(^4%6$)
    //------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------
    //);

    //endpoints.MapControllerRoute(
    //  name: "makeOrder",
    //  pattern: "MakeOrder/{controller=Orders}/{action=Create}/{*catchall}"
    ////defaults: new { controller="Orders" , action = "Create" }
    //);


    //endpoints.MapControllerRoute(
    //   name: "makeOrder",
    //   pattern: "MakeOrder/{controller=Orders}/{action=Create}/{title}"
    // //defaults: new { controller="Orders" , action = "Create" }
    // );

    //endpoints.MapControllerRoute(
    //   name: "makeOrder",
    //   pattern: "MakeOrder/{controller=Orders}/{action=Create}/{title?}"
    // //defaults: new { controller="Orders" , action = "Create" }
    // );

    //endpoints.MapControllerRoute(
    //       name: "makeOrder",
    //       pattern: "MakeOrder/{controller=Orders}/{action=Create}"
    //     //defaults: new { controller="Orders" , action = "Create" }
    //     );

    //endpoints.MapControllerRoute(
    //       name: "makeOrder",
    //       pattern: "MakeOrder",
    //       defaults: new { controller="Orders" , action = "Create" }
    //     );

    endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Products}/{action=Index}/{id?}"
                 );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});



app.Run();