using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace JokesApp.Models.Repositories
{
    public class JokesRepository    
    {

        public static async Task<bool> JokeExists(int id, JokesContext dbContext)
        {
            var joke = await GetJokeById(id,dbContext);
            if (joke == null) return false;
            else return true;
        }

        public static async Task<Joke?> GetJokeById(int id, JokesContext dbContext)
        {
            return await dbContext.Jokes.FirstOrDefaultAsync(x => x.ID == id);
        }

        public static async Task<List<Joke>> GetJokeList(JokesContext dbContext)
        {
            return await dbContext.Jokes.ToListAsync();
        }

        public static async Task<Joke> AddJoke(Joke joke, JokesContext dbContext)
        {
           await dbContext.Jokes.AddAsync(joke);
            await dbContext.SaveChangesAsync();
            return joke;
        }

        public static async Task<Joke?> RemoveJokeById(int id, JokesContext dbContext)
        {
            var joke = await GetJokeById(id, dbContext);
            if (joke != null)
            {
                dbContext.Jokes.Remove(joke);
                await dbContext.SaveChangesAsync();
                return joke;
            }
            else return null;
        }

        public static async Task<Joke> UpdateJoke(Joke joke, JokesContext dbContext)
        {
            dbContext.Jokes.Update(joke);
            await dbContext.SaveChangesAsync();
            return joke;
        }

    }
}
