using GestorBiblioteca.Infrastructure.Persistence;
using GestorBibliotecaApplication.Commands.CreateEmprestimo;
using GestorBibliotecaApplication.Services;
using GestorBibliotecaApplication.Services.Implementations;
using GestorBibliotecaApplication.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NPOI.SS.Formula.Functions;


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

//builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<T>());

//builder.Services.AddDbContext<LivrosDbContext>(o => o.UseInMemoryDatabase("GestorBibliotecaCs"));



builder.Services.AddScoped<IEmprestimoService, EmprestimoService>();
builder.Services.AddScoped<EmprestimoService>();
/*builder.Services.AddScoped<ILivroService, LivroService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();*/
builder.Services
    .AddAplication();

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
