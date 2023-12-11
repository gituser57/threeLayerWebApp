using WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<HttpResponseExceptionFilter>();
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();



//app.Environment.EnvironmentName = "Production";

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
   // app.UseExceptionHandler("/Error");
}


// but /error can be called directly. better to pass delegate to UseExceptionHandler
//    app.UseExceptionHandler(app => app.Run(async context =>
//{
//    context.Response.StatusCode = 500;
//    await context.Response.WriteAsync("Error 500 occurred!");
//}));
app.Map("/error", app => app.Run(async context =>
{
    context.Response.StatusCode = 500;
    await context.Response.WriteAsync("Error 500 occurred!");
}));



app.Map("/results/{id}", SendResults);

IResult SendResults(int id)
{// https://metanit.com/sharp/aspnet6/10.1.php
    // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/responses?view=aspnetcore-8.0
    // https://learn.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-8.0
    if (id == 1)
        return Results.Text($"Text");
    if (id == 2)
        return Results.Json(new { Name = "Petro", Surname = "Petrenko" });
    if (id == 3)
        return Results.Ok($"Text");
    if (id == 4)
        return Results.Ok(new { Name = "Petro", Surname = "Petrenko" });
    if (id == 5)
        return Results.LocalRedirect("/results/1");
    if (id == 6)
        return Results.BadRequest("Bad request!");
    if (id == 7)
        Results.NotFound(new { message = "Resource Not Found" });

    //string path = "Files/forest.png";
    //string contentType = "image/png";
    //string downloadName = "winter_forest.png";
    //return Results.File(path, contentType, downloadName);

    return Results.StatusCode(500);
}



app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();



