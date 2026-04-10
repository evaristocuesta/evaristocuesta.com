using AspNetStatic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var outputPath = args.Length >= 2 ? $"{args[1]}" : string.Empty;
var basePath = args.Length == 3 ? $"/{args[2]}" : string.Empty;

if (args.HasSsgArg())
{
    builder.Services.AddSingleton<IStaticResourcesInfoProvider>(
        new StaticResourcesInfoProvider(
            [
                new PageResource($"{basePath}/"),
                new PageResource($"{basePath}/privacy"),
                new PageResource($"{basePath}/cookies"),
                new CssResource($"{basePath}/css/site.css"),
                new CssResource($"{basePath}/evaristocuesta.com.styles.css"),
                new JsResource($"{basePath}/js/site.js"), 
                new BinResource($"{basePath}/images/favicon.ico"),
                new BinResource($"{basePath}/images/meta-image.jpg"),
                new BinResource($"{basePath}/images/profile.jpg"),
                new BinResource($"{basePath}/images/sidebar-background.jpg"),
                new PageResource($"{basePath}/robots.txt"),
                new PageResource($"{basePath}/sitemap.xml"),
            ]
        ));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UsePathBase(basePath);
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
            name: "privacy",
            pattern: "privacy",
            defaults: new { controller = "Home", action = "Privacy" });

app.MapControllerRoute(
            name: "cookies",
            pattern: "cookies",
            defaults: new { controller = "Home", action = "Cookies" });

if (args.HasSsgArg())
{
    if (!Path.Exists(outputPath))
    {
        Console.WriteLine($"Creating directory {outputPath}");
        Directory.CreateDirectory(outputPath);
    }

    Console.WriteLine($"Generating static content in {outputPath}");

    app.GenerateStaticContent(outputPath,
        alwaysDefaultFile: true,
        exitWhenDone: true,
        dontUpdateLinks: true);
}

app.Run();
