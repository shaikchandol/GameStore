using GameStore.API.Data;
using GameStore.API.Models;

namespace GameStore.API.Endpoints;

public static class GamesEndpoints
{
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games");
        group.MapPost("/games", async (Game game, GameStoreContext dbContext) =>
        {
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();
            return Results.Created($"/games/{game.Id}", game);
        });
        return group;
    }
}