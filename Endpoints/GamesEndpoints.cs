
using GameStore.api.Dtos;

namespace GameStore.api.Endpoints
{
    public static class GamesEndpoints
    {
        const string GetGameEndpointName  = "GetGame";
        private static readonly List<GameDto> games = 
            [ new( 1, "streetfight" , "Fighting" ,  19.99M,
            new DateOnly(1992, 7, 15))
            , new( 2,  "loveU" , "Shooter" ,  59.99M, new DateOnly(2020, 3, 10))
            , new( 3, "fifa22" , "Sports" , 49.99M, new DateOnly(2021, 9, 27)),
        ];

        public static void MapGameEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/games");
            
             // GET  /games 
            group.MapGet("", () => games);

            //GET  /games/{id}
            group.MapGet("/{id}", (int id) => {


                var  game = games.Find(game => game.Id == id);
                return game is null ? Results.NotFound() : Results.Ok(game);
                })
            .WithName("GetGame");    

            // POST / games
            group.MapPost("/", (CreateGameDto newGame) =>
            {
            
            GameDto game = new(games.Count + 1, newGame.Name, newGame.Genre, newGame.Price, newGame.ReleaseDate);
            games.Add(game); 

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
            });
            // PUT / games/{id}
            group.MapPut("/{id}", (int id , UpdateGameDto updateGame) =>
            {
            var index= games.FindIndex(game => game.Id == id);

            if(index == -1)
                {
                    return Results.NotFound();
                }
            games[index] = new GameDto(id, updateGame.Name, updateGame.Genre, updateGame.Price, updateGame.ReleaseDate); 

            return Results.NoContent(); 
            });
            // DELETE / games/{id}
            group.MapDelete("/{id}", (int id) =>
            {
             var removed = games.RemoveAll(game => game.Id == id); 
            return removed == 0 ? Results.NotFound() : Results.NoContent();
            });   

        }
    }
}