using GameStore.API.Data;
using GameStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Endpoints;

public static class GamesEndpoints
{
    const string GameEndPointName = "Games";
    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("games").WithParameterValidation();
        //POST /games
        group.MapPost("/", async (Game game, GameStoreContext dbContext) =>
        {
            dbContext.Games.Add(game);
            await dbContext.SaveChangesAsync();
            return Results.CreatedAtRoute(GameEndPointName  , new { id = game.Id }, game);
        });
        //GET /games
        group.MapGet("/", async (GameStoreContext dbContext) =>
        {
            var games = await dbContext.Games.AsNoTracking().ToListAsync();
            return Results.Ok(games);
        });
        //GET /games/{id}
        group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games.FindAsync(id);
            return game is not null ? Results.Ok(game) : Results.NotFound();
        })
        .WithName(GameEndPointName);
        //PUT /games/{id}
        group.MapPut("/{id}", async (int id, Game updatedGame, GameStoreContext dbContext) =>
        {
            var game = await dbContext.Games.FindAsync(id);
            if (game is null) return Results.NotFound();
            dbContext.Entry(game).CurrentValues.SetValues(updatedGame);

            await dbContext.SaveChangesAsync();
            return Results.NoContent();
        });
        //DELETE /games/{id}
        group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
        {
             await dbContext.Games.Where(g => g.Id == id).ExecuteDeleteAsync();
            
            return Results.NoContent();
        });
        return group;
    }
}