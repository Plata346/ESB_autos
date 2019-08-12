using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Services;

namespace WebApplication4
{
    /// <summary>
    /// Descripción breve de ESB_autos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class ESB_autos : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod]
        public String PedirViaje()
        {
            try
            {
                WS_Pilotos.WebService1Soap ServicoPilotos = new WS_Pilotos.WebService1SoapClient();
                if(ServicoPilotos.AgregarPiloto())
                {
                    return "Piloto agregado de forma exitosa";
                }else
                {
                    return "Son cosas que pasan :(";
                }

            }
            catch (Exception)
            {
                return "Fracaso total";
            }
        }
    }
}
