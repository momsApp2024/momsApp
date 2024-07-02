var builder = WebApplication.CreateBuilder(args);

// add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // configure JSON options if necessary
        options.JsonSerializerOptions.PropertyNamingPolicy = null; 
    });

// configure CORS to allow requests from the Angular app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
