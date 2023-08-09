using PersonaPoliticas.Datos;
using PersonaPoliticas.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSqlServer<DBPersonaContext>(builder.Configuration.GetConnectionString("cnpersona"));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<IHijoService, HijoService>();
builder.Services.AddScoped<IPadreService, PadreService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
