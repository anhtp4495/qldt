using CNTT129_NetCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

app.UseSession();

//builder.Services.AddOptions<AppSettings>().Configure<IConfiguration>((options, setting) =>
//{
//    AppSettings.ConnectionString = setting.GetValue<string>("ConnectionString");
//});

ServiceProvider provider = builder.Services.BuildServiceProvider();
IConfiguration? configuration = provider.GetService<IConfiguration>();
AppSettings.ConnectionString = configuration.GetValue<string>("ConnectionString");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Login}/{id?}");

app.Run();
