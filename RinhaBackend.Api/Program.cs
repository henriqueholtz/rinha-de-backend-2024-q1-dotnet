using Microsoft.EntityFrameworkCore;
using RinhaBackend.Api.Attributes;
using RinhaBackend.Api.Context;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<RinhaContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("RinhaConnection")));

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var result = new ValidationFailedResult(context.ModelState);

        // TODO: add `using System.Net.Mime;` to resolve MediaTypeNames  
        result.ContentTypes.Add(MediaTypeNames.Application.Json);

        return result;
    };
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

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
