using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PersonaPoliticas.Models
{
    public partial class Genero
    {
        public Genero()
        {
            PersonaHijo = new HashSet<PersonaHijo>();
            PersonaPadre = new HashSet<PersonaPadre>();
        }

        public int Id { get; set; }
        public string Genero1 { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<PersonaHijo> PersonaHijo { get; set; }
        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<PersonaPadre> PersonaPadre { get; set; }
    }
}
