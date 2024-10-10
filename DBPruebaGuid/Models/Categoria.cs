using System;
using System.Collections.Generic;

namespace DBPruebaGuid.Models
{
    public class Categoria
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nombre { get; set; }
        public ICollection<Producto> Productos { get; set; }
    }

}
