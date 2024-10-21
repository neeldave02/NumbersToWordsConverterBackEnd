using System;
using NumbersToWords;
using System.Numerics;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Change this to the React Apps Origin - it should be port 3000 by default
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowReactApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// API call for converting a number
app.MapGet("/convert/{number}", (string number) =>
{
    var converter = new NumberToWordsConverter();

    try
    {
        // Try parse as small number - for decimals
        if (decimal.TryParse(number, out decimal decimalNumber))
        {
            string result = converter.ConvertToWords(decimalNumber);
            return Results.Text(result, "text/plain");
        }
        // Do parsing for big integers
        else if (BigInteger.TryParse(number, out BigInteger bigIntegerNumber))
        {
            string result = converter.ConvertToWords(bigIntegerNumber);
            return Results.Text(result, "text/plain");
        }
        else
        {
            return Results.BadRequest("The input number is invalid.");
        }
    }
    catch (OverflowException)
    {
        return Results.BadRequest("The input number is too large to handle due to overflow.");
    }


})
.WithName("ConvertNumberToWords")
.WithOpenApi();

app.Run();
