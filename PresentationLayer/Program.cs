using AutoMapper;
using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Add CORS

 string cors = "AllowAll";

builder.Services.AddCors(options =>
{
    options.AddPolicy(cors,
               builder =>
               {
                   builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
               });
});
#endregion

#region Add DB Context to DI Container

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IGameCategoryInterface, GameCategoryRepository>();
builder.Services.AddTransient<IGameInterface, GameRepository>();
builder.Services.AddTransient<IGameService, GameService>();
builder.Services.AddTransient<IGameCategoryService,  GameCategoryService>();

#endregion

#region Parametr of Mapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MyMapper());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

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
