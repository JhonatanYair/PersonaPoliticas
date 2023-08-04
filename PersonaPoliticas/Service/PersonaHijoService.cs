
using PersonaPoliticas.Models;
using PersonaPoliticas.Datos;
using Microsoft.EntityFrameworkCore;

namespace PersonaPoliticas.Services
{
    public class PersonaHijoService : IPersonaHijoService
    {
        private DBPersonaContext context;

        public PersonaHijoService(DBPersonaContext dbContext)
        {
            context = dbContext;
        }

        public IEnumerable<PersonaHijo> Get()
        {
            return context.PersonaHijo.Include(p => p.Padre).Include(p=>p.Genero);
        }

        public async Task<PersonaHijo> Save(PersonaHijo personahijo)
        {
            context.Add(personahijo);
            await context.SaveChangesAsync();
            return personahijo;
        }

        public async Task<PersonaHijo> Update(int id, PersonaHijo personahijo)
        {
            var personahijoActual = context.PersonaHijo.Find(id);

            if (personahijoActual != null)
            {
                personahijoActual = personahijo;
                await context.SaveChangesAsync();
            }

            return personahijoActual;

        }

        public async Task Delete(int id)
        {
            var personahijoActual = context.PersonaHijo.Find(id);

            if (personahijoActual != null)
            {
                context.Remove(personahijoActual);
                await context.SaveChangesAsync();
            }
        }
    }

    public interface IPersonaHijoService
    {
        IEnumerable<PersonaHijo> Get();
        Task<PersonaHijo> Save(PersonaHijo personahijo);
        Task<PersonaHijo> Update(int id, PersonaHijo personahijo);
        Task Delete(int id);
    }
}

