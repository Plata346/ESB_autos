//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Usuarios.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class piloto
    {
        public piloto()
        {
            this.viaje = new HashSet<viaje>();
        }
    
        public int id { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string marca_carro { get; set; }
        public string linea_carro { get; set; }
        public string placa_carro { get; set; }
    
        public virtual ICollection<viaje> viaje { get; set; }
    }
}