using Shape.Services.Interface;
using Shape.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddControllers();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:7153")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.Use((context, next) =>
{
    context.Response.Headers.Add("Referrer-Policy", "no-referrer");
    return next();
});

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});
app.UseAuthorization();

app.MapControllers();

app.Run();
