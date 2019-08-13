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
        public String NuevoPiloto(String name, String tel, String marca, String linea, String placa)
        {
            try
            {
                WS_Piloto.WebService1SoapClient ServicoPilotos = new WS_Piloto.WebService1SoapClient();
                
                string phrase = ServicoPilotos.AgregarPiloto(name, tel, marca, linea, placa);
                string[] result = phrase.Split('-');

                //foreach (var word in result)
                //{
                //    System.Console.WriteLine($"<{word}>");
                //}
                if(result[0].Equals("0"))
                {
                    return result[1];
                }
                else
                {
                    return result[1];
                }                

            }
            catch (Exception)
            {
                return "Fracaso total";
            }
        }

    }
}
