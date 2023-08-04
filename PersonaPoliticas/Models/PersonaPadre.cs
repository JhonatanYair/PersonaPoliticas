using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PersonaPoliticas.Models
{
    public partial class PersonaPadre
    {
        public PersonaPadre()
        {
            PersonaHijo = new HashSet<PersonaHijo>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? Edad { get; set; }
        public int? GeneroId { get; set; }

        public virtual Genero Genero { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<PersonaHijo> PersonaHijo { get; set; }
    }
}
