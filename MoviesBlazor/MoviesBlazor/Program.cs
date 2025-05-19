using MoviesBlazor.Clients;
using MoviesBlazor.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var movieApiUrl = builder.Configuration["MovieApiUrl"] ?? throw new Exception("Movie Url is not set"); // get url to Api from the json file 

builder.Services.AddHttpClient<MoviesClient>(client => client.BaseAddress = new Uri(movieApiUrl)); // adds httpclient to di container 
builder.Services.AddHttpClient<GenresClient>(client => client.BaseAddress = new Uri(movieApiUrl)); // adds httpclient to di container 
//builder.Services.AddScoped(sp =>
//    new HttpClient { BaseAddress = new Uri(movieApiUrl) });

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
