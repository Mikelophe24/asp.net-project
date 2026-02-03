
using GameStore.api.Dtos;
using GameStore.api.Endpoints;
using GameStore.api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

var connString = "Data Source=GameStore.db";

builder.Services.AddSqlite<GamesStoreContext>(connString);

var app = builder.Build();
app.MigrateDb();
app.MapGameEndpoints();
app.Run();
