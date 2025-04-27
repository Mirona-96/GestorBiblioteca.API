using GestorBiblioteca.Infrastructure.Persistence;
using GestorBibliotecaApplication.Services.Implementations;
using GestorBibliotecaApplication.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//meus servicos
//builder.Services.AddSingleton<LivrosDbContext>();
//builder.Services.AddSingleton<LivrosDbContext>();
/*builder.Services.AddDbContext<LivrosDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("GestorBibliotecaCs")));*/

builder.Services.AddDbContext<LivrosDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("GestorBibliotecaCs"),
        sqlOptions => sqlOptions.MigrationsAssembly("GestorBiblioteca.API")
    )
);


builder.Services.AddScoped<IEmprestimoService, EmprestimoService>();
builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

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
