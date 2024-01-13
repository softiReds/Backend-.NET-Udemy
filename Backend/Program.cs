using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using Backend.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddSingleton<IPeopleService, People2Service>();
builder.Services.AddKeyedSingleton<IPeopleService, PeopleService>("peopleService"); //  AddKey...("key") -> Asigna una key a la dependencia, permite que se utilice la key para especificar la dependencia a la que se quiere acceder (la especificacion se hace desde el controlador)
builder.Services.AddKeyedSingleton<IPeopleService, People2Service>("people2Service");

builder.Services.AddKeyedSingleton<IRandomService, RandomService>("randomSingleton");
builder.Services.AddKeyedScoped<IRandomService, RandomService>("randomScoped");
builder.Services.AddKeyedTransient<IRandomService, RandomService>("randomTransient");
builder.Services.AddScoped<IPostsService, PostsService>();
builder.Services.AddKeyedScoped<ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto>, BeerService>("beerService");


builder.Services.AddHttpClient<IPostsService, PostsService>(e =>    //  AddHttpClient<Interface, Service> -> Configura un cliente HTTP para el servicio (manera correcta de hacerlo)
{
    //e.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/posts");
    e.BaseAddress = new Uri(builder.Configuration["BaseUrlPosts"]); //  Obtenemos la URL desde uno de los elementos del appsettings.json
});

builder.Services.AddDbContext<StoreContext>(e =>    //  AddDbContext<ContextClass>() -> Inyecta un contexto de BD
{
    e.UseSqlServer(builder.Configuration.GetConnectionString("StoreConnection"));
});

//  Validators
builder.Services.AddScoped<IValidator<BeerInsertDto>, BeerInsertValidator>();   //  <IValidator<DtoOrClass>, Validator>() -> Inyecta un validador
builder.Services.AddScoped<IValidator<BeerUpdateDto>, BeerUpdateValidator>();

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
