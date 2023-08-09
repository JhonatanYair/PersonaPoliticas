using PersonaPoliticas.Models;
using PersonaPoliticas.Datos;
using Microsoft.EntityFrameworkCore;

namespace PersonaPoliticas.Services
{
    public class PersonaService : IPersonaService
    {
        private DBPersonaContext context;

        public PersonaService(DBPersonaContext dbContext)
        {
            context = dbContext;
        }

        // Recupera todas las personas, incluyendo su relación con la entidad Genero y las colecciones Hijo y Padre
        public IEnumerable<Persona> Get()
        {
            return context.Persona.Include(p => p.IdGeneroNavigation).Include(p => p.Hijo).Include(p => p.Padre);
        }

        // Guarda una nueva persona en la base de datos
        public async Task Save(Persona persona)
        {
            context.Add(persona);
            await context.SaveChangesAsync();
        }

        // Actualiza una persona existente por su ID y actualiza sus propiedades, incluyendo propiedades de navegación
        public async Task Update(int id, Persona persona)
        {
            // Encuentra la persona actual en la base de datos
            var personaActual = context.Persona.Include(p => p.IdGeneroNavigation).FirstOrDefault(p => p.Id == id);

            if (personaActual != null)
            {
                // Actualiza las propiedades de la persona actual con los valores de la persona proporcionada
                context.Entry(personaActual).CurrentValues.SetValues(persona);

                // Comprueba si la propiedad de navegación IdGeneroNavigation está presente en la persona proporcionada
                if (persona.IdGeneroNavigation != null)
                {
                    // Actualiza las propiedades de navegación de la persona actual con los valores de la persona proporcionada
                    context.Entry(personaActual.IdGeneroNavigation).CurrentValues.SetValues(persona.IdGeneroNavigation);
                }

                await context.SaveChangesAsync();
            }
        }

        // Elimina una persona por su ID y también elimina las relaciones Hijo y Padre debido a la política de cascada
        public async Task Delete(int id)
        {
            // Encuentra la persona actual en la base de datos
            var personaActual = context.Persona.Include(p => p.Hijo).Include(p => p.Padre).FirstOrDefault(p => p.Id == id);

            if (personaActual != null)
            {
                // Elimina la persona actual, lo que también eliminará las relaciones Hijo y Padre debido a la política de cascada
                context.Persona.Remove(personaActual);
                await context.SaveChangesAsync();
            }
        }


        public async Task Save2(Persona persona)
        {
            // Inicia una nueva transacción
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Agrega la persona al contexto
                    context.Add(persona);
                    // Guarda los cambios en la base de datos
                    await context.SaveChangesAsync();

                    // Confirma la transacción si todo es exitoso
                    transaction.Commit();
                }
                catch (Exception)
                {
                    // Revierte la transacción en caso de error
                    transaction.Rollback();
                }
            }
        }

        public async Task Update2(int id, Persona persona)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Encuentra la persona actual en la base de datos
                    var personaActual = context.Persona.Include(p => p.IdGeneroNavigation).FirstOrDefault(p => p.Id == id);

                    if (personaActual != null)
                    {
                        // Actualiza las propiedades de la persona actual con los valores proporcionados
                        context.Entry(personaActual).CurrentValues.SetValues(persona);

                        // Actualiza las propiedades de navegación si están presentes en la persona proporcionada
                        if (persona.IdGeneroNavigation != null)
                        {
                            context.Entry(personaActual.IdGeneroNavigation).CurrentValues.SetValues(persona.IdGeneroNavigation);
                        }

                        // Guarda los cambios en la base de datos
                        await context.SaveChangesAsync();
                        // Confirma la transacción si todo es exitoso
                        transaction.Commit();
                    }
                }
                catch (Exception)
                {
                    // Revierte la transacción en caso de error
                    transaction.Rollback();
                }
            }
        }

        public async Task Delete2(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    // Encuentra la persona actual en la base de datos, incluyendo relaciones
                    var personaActual = context.Persona.Include(p => p.Hijo).Include(p => p.Padre).FirstOrDefault(p => p.Id == id);

                    if (personaActual != null)
                    {
                        // Elimina la persona, lo que también eliminará las relaciones debido a la política de cascada
                        context.Persona.Remove(personaActual);
                        // Guarda los cambios en la base de datos
                        await context.SaveChangesAsync();
                        // Confirma la transacción si todo es exitoso
                        transaction.Commit();
                    }
                }
                catch (Exception)
                {
                    // Revierte la transacción en caso de error
                    transaction.Rollback();
                }
            }
        }


        // Batch Updates Politica
        // Realizar actualizaciones por lotes (batch updates)
        // puede ser más eficiente que actualizar registros uno por uno, ya que minimiza las llamadas a la base de datos.

        public async Task IncrementarEdad()
        {
            // Lista de IDs de personas a actualizar
            var idsPersonas = new List<int> { 1, 2, 3 };

            // Incremento de la edad
            int incrementoEdad = 1;

            // Carga las personas a actualizar
            var personas = context.Persona.Where(p => idsPersonas.Contains(p.Id)).ToList();

            // Aplica el incremento a la edad
            foreach (var persona in personas)
            {
                persona.Edad += incrementoEdad;
            }

            // Actualiza las personas en la base de datos
            await context.SaveChangesAsync(); // Usa await para guardar los cambios de manera asincrónica
        }

    }

    public interface IPersonaService
    {
        IEnumerable<Persona> Get();
        Task Save(Persona persona);
        Task Update(int id, Persona persona);
        Task Delete(int id);
        Task Save2(Persona persona);
        Task Update2(int id, Persona persona);
        Task Delete2(int id);
        Task IncrementarEdad();

    }
}
