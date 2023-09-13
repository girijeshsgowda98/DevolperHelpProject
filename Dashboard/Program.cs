using Dashboard.DbService;
using Dashboard.Repositories;
using Dashboard.Repositories.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
/*void ConfigureServices(IServiceCollection services)
{
   services.AddScoped(typeof(IinsertQueryInterface), typeof(InsertQueryRepository));

   // Other service registrations and configurations...
}*/

builder.Services.AddScoped<InsertQueryRepository>();
builder.Services.AddScoped<RequestRepositoriesImp>();
builder.Services.AddScoped<ExceptionRepositories>();
builder.Services.AddScoped<FilterRepositories>();
builder.Services.AddScoped<NpgsqlDbService>();
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
