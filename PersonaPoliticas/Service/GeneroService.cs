

using PersonaPoliticas.Models;
using PersonaPoliticas.Datos;

namespace PersonaPoliticas.Services
{
    public class GeneroService : IGeneroService
    {
        private DBPersonaContext context;

        public GeneroService(DBPersonaContext dbContext)
        {
            context = dbContext;
        }

        public IEnumerable<Genero> Get()
        {
            return context.Genero;
        }

        public async Task<Genero> Save(Genero genero)
        {
            context.Add(genero);
            await context.SaveChangesAsync();
            return genero;
        }

        public async Task<Genero> Update(int id, Genero genero)
        {
            var generoActual = context.Genero.Find(id);

            if (generoActual != null)
            {
                generoActual = genero;
                await context.SaveChangesAsync();
            }

            return genero;

        }

        public async Task Delete(int id)
        {
            var generoActual = context.Genero.Find(id);

            if (generoActual != null)
            {
                context.Remove(generoActual);
                await context.SaveChangesAsync();
            }
        }
    }

    public interface IGeneroService
    {
        IEnumerable<Genero> Get();
        Task<Genero> Save(Genero genero);
        Task<Genero> Update(int id, Genero genero);
        Task Delete(int id);
    }
}

