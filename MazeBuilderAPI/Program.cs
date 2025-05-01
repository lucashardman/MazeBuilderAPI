using System.Text.Json.Serialization;
using MazeBuilderAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.UseUrls("http://+:7013");
builder.Services.AddScoped<MazeService>();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()   
            .AllowAnyMethod()  
            .AllowAnyHeader();   
    });
});

var app = builder.Build();
app.UsePathBase("/MazeBuilder");
app.UseCors("AllowAll");


app.UseSwagger();
app.UseSwaggerUI();
app.UseSwaggerUI(c =>
{
    // Fazendo isso só para padronizar e o swagger ficar em /docs como nas outras APIs que faço com FastAPI
    c.RoutePrefix = "docs"; 
    c.SwaggerEndpoint("/MazeBuilder/swagger/v1/swagger.json", "MazeBuilder API V1");
});


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();