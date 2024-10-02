using SchedulifySystem.API;
using SchedulifySystem.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddWebAPIService(builder);
builder.Services.AddInfractstructure(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Schedulify Web API");
    c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
    c.RoutePrefix = "swagger";
});

app.UseHttpsRedirection();

app.UseCors("app-cors");
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.Run();