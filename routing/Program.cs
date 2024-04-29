using routing.CustomConstraint;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("alphanumeric", typeof(Aiphanumerics));
});
var app = builder.Build();
app.UseStaticFiles();  //to run static file .. images,pdf ....
app.UseRouting();
app.Use(async (context, next) =>
{
    Endpoint endpoint = context.GetEndpoint();
    if (endpoint != null)
        await context.Response.WriteAsync(endpoint.DisplayName+"\n");
    await next(context);
});
app.UseEndpoints(endpoints =>
{
    endpoints.Map("/Home", async (context) =>
    {
        await context.Response.WriteAsync("You are in home page");
    });
    // assign route parameter option using ? mark {Id?}
    endpoints.MapGet("/Product/{Id?}", async (context) =>
     {
         var id = context.Request.RouteValues["Id"];
         if (id != null)
         { 
             id= id = Convert.ToInt32(id);
             await context.Response.WriteAsync("You are in Product page with id " + id);
         }else
         {
             await context.Response.WriteAsync("You are in home page__________");

         }
     });
    //default rout parameter value  authorname=uhiui
    //assign datatypes using :  
    endpoints.MapGet("/Product/Book/{authorname=uhiui}/{bookid:int:min(10):max(100)}", async (context) =>
    {
        var id = Convert.ToInt32(context.Request.RouteValues["bookid"]);
        //var author_name = Convert.ToString(context.Request.RouteValues["authorname"]);
        var author_name = context.Request.RouteValues["authorname"];


        await context.Response.WriteAsync($"This is the book authored by {author_name} and book id {id}");
    });
    endpoints.MapPost("/Qqq", async (context) =>
    {
        await context.Response.WriteAsync("You are in ProductProduct{{{{{{ page");
    });
    endpoints.MapGet("/Quaterly_report/{year:minlength(4):min(1999)}/{month:regex(^(mar|jun)$)}", async (context) =>
    {
        int year = Convert.ToInt32(context.Request.RouteValues["year"]);
        string month = Convert.ToString(context.Request.RouteValues["month"]);

        await context.Response.WriteAsync($"Quaterly Rreport for year - {year} month - {month}");
    });

    //Create a custom router contraint , create a new class CustomConstraint\Aiphanumerics.cs

    endpoints.MapGet("/user/{username:alphanumeric}", async (context) =>
    {
        string username = Convert.ToString(context.Request.RouteValues["username"]);
        await context.Response.WriteAsync($"Welcome {username}");
    });
});
//{year:minlength(4):min(1999)}
//app.MapGet("/", () => "Hello World!");
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("not found");
});
app.Run();
