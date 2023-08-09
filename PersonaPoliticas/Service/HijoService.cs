using PersonaPoliticas.Models;
using PersonaPoliticas.Datos;
using Microsoft.EntityFrameworkCore;

namespace PersonaPoliticas.Services
{
    public class HijoService : IHijoService
    {
        private DBPersonaContext context;

        public HijoService(DBPersonaContext dbContext)
        {
            context = dbContext;
        }

        // Recupera todos los hijos, incluyendo sus relaciones con las entidades Padre y Persona
        public IEnumerable<Hijo> Get()
        {
            return context.Hijo.Include(h => h.IdPadreNavigation).Include(h => h.IdPersonaNavigation);
        }

        // Guarda un nuevo hijo en la base de datos
        public async Task Save(Hijo hijo)
        {
            context.Add(hijo);
            await context.SaveChangesAsync();
        }

        // Actualiza un hijo existente por su ID y actualiza sus propiedades, incluyendo propiedades de navegación
        public async Task Update(int id, Hijo hijo)
        {
            // Encuentra el hijo actual en la base de datos
            var hijoActual = context.Hijo.Include(h => h.IdPadreNavigation).Include(h => h.IdPersonaNavigation).FirstOrDefault(h => h.Id == id);

            if (hijoActual != null)
            {
                // Actualiza las propiedades del hijo actual con los valores del hijo proporcionado
                context.Entry(hijoActual).CurrentValues.SetValues(hijo);

                // Comprueba si las propiedades de navegación están presentes en el hijo proporcionado
                if (hijo.IdPadreNavigation != null)
                {
                    // Actualiza las propiedades de navegación del hijo actual con los valores del hijo proporcionado
                    context.Entry(hijoActual.IdPadreNavigation).CurrentValues.SetValues(hijo.IdPadreNavigation);
                }

                if (hijo.IdPersonaNavigation != null)
                {
                    // Actualiza las propiedades de navegación del hijo actual con los valores del hijo proporcionado
                    context.Entry(hijoActual.IdPersonaNavigation).CurrentValues.SetValues(hijo.IdPersonaNavigation);
                }

                await context.SaveChangesAsync();
            }
        }

        // Elimina un hijo por su ID
        public async Task Delete(int id)
        {
            // Encuentra el hijo actual en la base de datos
            var hijoActual = context.Hijo.Find(id);

            if (hijoActual != null)
            {
                context.Hijo.Remove(hijoActual);
                await context.SaveChangesAsync();
            }
        }
    }

    public interface IHijoService
    {
        IEnumerable<Hijo> Get();
        Task Save(Hijo hijo);
        Task Update(int id, Hijo hijo);
        Task Delete(int id);
    }
}
