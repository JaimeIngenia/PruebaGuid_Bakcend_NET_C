namespace DBPruebaGuid.Models.ViewModels.Producto
{

        public class ProductoViewModel
        {
            public string Nombre { get; set; }
            public decimal Precio { get; set; }
            public Guid CategoriaId { get; set; }
        }
}
