using Api.Configuracoes;
using Infra;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services
    .ConfigurarSwagger()
    .ConfigureController()
    .AddContext(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();