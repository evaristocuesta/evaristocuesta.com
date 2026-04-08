using AspNetStatic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

if (args.HasSsgArg())
{
    builder.Services.AddSingleton<IStaticResourcesInfoProvider>(
        new StaticResourcesInfoProvider(
            [
                new PageResource("/"), 
                new CssResource("/css/site.css"),
                new CssResource("/evaristocuesta.com.styles.css"),
                new JsResource("/js/site.js"), 
                new BinResource("/images/favicon.ico"),
                new BinResource("/images/meta-image.jpg"),
                new BinResource("/images/profile.jpg"),
                new BinResource("/images/sidebar-background.jpg")
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
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

if (args.HasSsgArg())
{
    var outputPath = args.Length >= 2 ? $"{args[1]}" : string.Empty;

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
