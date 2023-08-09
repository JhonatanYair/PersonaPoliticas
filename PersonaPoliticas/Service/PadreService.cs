using PersonaPoliticas.Models;
using PersonaPoliticas.Datos;
using Microsoft.EntityFrameworkCore;

namespace PersonaPoliticas.Services
{
    public class PadreService : IPadreService
    {
        private DBPersonaContext context;

        public PadreService(DBPersonaContext dbContext)
        {
            context = dbContext;
        }

        // Recupera todos los padres, incluyendo sus propiedades de navegaci�n IdPersonaNavigation y Hijo
        public IEnumerable<Padre> Get()
        {
            return context.Padre.Include(p => p.IdPersonaNavigation).Include(p => p.Hijo);
        }

        // Guarda un nuevo padre en la base de datos
        public async Task Save(Padre padre)
        {
            context.Add(padre);
            await context.SaveChangesAsync();
        }

        // Actualiza un padre existente por su ID y actualiza sus propiedades, incluyendo propiedades de navegaci�n
        public async Task Update(int id, Padre padre)
        {
            // Encuentra el padre actual en la base de datos
            var padreActual = context.Padre.Include(p => p.IdPersonaNavigation).FirstOrDefault(p => p.Id == id);

            if (padreActual != null)
            {
                // Actualiza las propiedades del padre actual con los valores del padre proporcionado
                context.Entry(padreActual).CurrentValues.SetValues(padre);

                // Comprueba si la propiedad de navegaci�n IdPersonaNavigation est� presente en el padre proporcionado
                if (padre.IdPersonaNavigation != null)
                {
                    // Actualiza las propiedades de navegaci�n del padre actual con los valores del padre proporcionado
                    context.Entry(padreActual.IdPersonaNavigation).CurrentValues.SetValues(padre.IdPersonaNavigation);
                }

                await context.SaveChangesAsync();
            }
        }

        // Elimina un padre por su ID y tambi�n elimina los hijos relacionados debido a la pol�tica de cascada
        public async Task Delete(int id)
        {
            // Encuentra el padre actual en la base de datos
            var padreActual = context.Padre.Include(p => p.Hijo).FirstOrDefault(p => p.Id == id);

            if (padreActual != null)
            {
                // Elimina el padre actual, lo que tambi�n eliminar� los hijos relacionados debido a la pol�tica de cascada
                context.Padre.Remove(padreActual);
                await context.SaveChangesAsync();
            }
        }
    }

    public interface IPadreService
    {
        IEnumerable<Padre> Get();
        Task Save(Padre padre);
        Task Update(int id, Padre padre);
        Task Delete(int id);
    }
}
