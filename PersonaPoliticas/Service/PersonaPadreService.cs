
using PersonaPoliticas.Models;
using PersonaPoliticas.Datos;
using Microsoft.EntityFrameworkCore;

namespace PersonaPoliticas.Services
{
    public class PersonaPadreService : IPersonaPadreService
    {
        private DBPersonaContext context;

        public PersonaPadreService(DBPersonaContext dbContext)
        {
            context = dbContext;
        }

        public IEnumerable<PersonaPadre> Get()
        {
            return context.PersonaPadre.Include(p=>p.Genero);
        }

        public async Task<PersonaPadre> Save(PersonaPadre personaPadre)
        {
            //context.Add(personapadre);
            //await context.SaveChangesAsync();
            //return personapadre;


            Genero genero = await context.Genero.FindAsync(personaPadre.GeneroId);

            if (genero == null)
            {
                // Si el Genero no existe, puedes lanzar una excepción o manejar el escenario según tus necesidades.
                throw new Exception("El Genero especificado no existe.");
            }

            // Asignar el Genero al PersonaPadre
            personaPadre.Genero = genero;

            // Agregar el PersonaPadre al contexto
            context.PersonaPadre.Add(personaPadre);
            await context.SaveChangesAsync();

            return personaPadre;

        }

        public async Task<PersonaPadre> Update(int id, PersonaPadre personapadre)
        {
            var personapadreActual = context.PersonaPadre.Find(id);

            if (personapadreActual != null)
            {
                personapadreActual = personapadre;
                await context.SaveChangesAsync();
            }
            return personapadreActual;
        }

        public async Task Delete(int id)
        {
            var personapadreActual = context.PersonaPadre.Find(id);

            if (personapadreActual != null)
            {
                context.Remove(personapadreActual);
                await context.SaveChangesAsync();
            }
        }
    }

    public interface IPersonaPadreService
    {
        IEnumerable<PersonaPadre> Get();
        Task<PersonaPadre> Save(PersonaPadre personapadre);
        Task<PersonaPadre> Update(int id, PersonaPadre personapadre);
        Task Delete(int id);
    }
}

