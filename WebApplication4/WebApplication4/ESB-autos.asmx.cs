using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Services;
using System.Xml.Linq;

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

        /*
            Instancia global para el WS de pilotos
        */

        WS_Piloto.WebService1SoapClient ServicoPilotos = new WS_Piloto.WebService1SoapClient();

        /// <summary>
        /// Registrar nuevos piloto.
        /// SOlicita parametros como Nombre, telefono, mara, linea y placa del vehicuo.
        /// </summary>
        /// <returns>
        /// String -xml-
        /// </returns>
        [WebMethod]
        public String NuevoPiloto(String name, String tel, String marca, String linea, String placa)
        {
            try
            {                           
                string phrase = ServicoPilotos.AgregarPiloto(name, tel, marca, linea, placa);
                string[] result = phrase.Split('-');

                //foreach (var word in result)
                //{
                //    System.Console.WriteLine($"<{word}>");
                //}

                XDocument XMLRespuesta = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement("Piloto",
                            new XElement("Nombre", new XCData(name)),
                            new XElement("Telefono", new XCData(tel)),
                            new XElement("Marca_vehiculo", new XCData(marca)),
                            new XElement("Linea_vehiculo", new XCData(linea)),
                            new XElement("Placa_vehiculo", new XCData(placa)),
                            new XElement("Codigo", new XCData(result[0])),
                            new XElement("Mensaje", new XCData(result[1])))
                    );

                StringWriter wr = new StringWriter();
                XMLRespuesta.Save(wr);

                return wr.ToString();                             

            }
            catch (Exception ex)
            {
                return "Fracaso total";
            }
        }

        /// <summary>
        /// Finalizar viaje pendiente.
        /// SOlicita parametro id Piloto
        /// </summary>
        /// <returns>
        /// String -xml-
        /// </returns>
        [WebMethod]
        public String TerminarViajePiloto(int idPiloto)
        {
            try
            {
                string phrase = ServicoPilotos.FinalizarViaje(idPiloto);
                string[] result = phrase.Split('-');               

                XDocument XMLRespuesta = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement("Viaje_Piloto",                            
                            new XElement("Codigo", new XCData(result[0])),
                            new XElement("Mensaje", new XCData(result[1])))
                    );

                StringWriter wr = new StringWriter();
                XMLRespuesta.Save(wr);

                return wr.ToString();

            }
            catch (Exception ex)
            {
                return "Fracaso total";
            }
        }



        /*
            Instancia global para el WS de clientes
        */

        WS_Usuario.Actividades_UsuarioSoapClient ServicoUsuarios = new WS_Usuario.Actividades_UsuarioSoapClient();


        /// <summary>
        /// Registrar nuevos usuarios.
        /// SOlicita parametros como Nombre, telefono, usuario, contraseña.
        /// </summary>
        /// <returns>
        /// String -xml-
        /// </returns>
        [WebMethod]
        public string NuevoUsuario(String user, String tel, String unique, String clave)
        {
            try
            {                
                bool phrase = ServicoUsuarios.RegistrarUsuario(user, tel, unique, clave);
                
                if (phrase)
                {
                    XDocument XMLRespuesta = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement("Usuario",
                            new XElement("Nombre", new XCData(user)),
                            new XElement("Telefono", new XCData(tel)),
                            new XElement("Usuario", new XCData(unique)),
                            new XElement("Codigo", new XCData("0")),
                            new XElement("Mensaje", new XCData("Usuario agregado exitosamente.")))                        
                    );

                    StringWriter wr = new StringWriter();
                    XMLRespuesta.Save(wr);

                    return wr.ToString(); 
                }
                else
                {
                    XDocument XMLRespuesta = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement("Usuario",
                            new XElement("Nombre", new XCData(user)),
                            new XElement("Telefono", new XCData(tel)),
                            new XElement("Usuario", new XCData(unique)),
                            new XElement("Codigo", new XCData("1")),
                            new XElement("Mensaje", new XCData("ERROR al registrar el usuario.")))
                    );

                    StringWriter wr = new StringWriter();
                    XMLRespuesta.Save(wr);

                    return wr.ToString();
                }

            }
            catch (Exception)
            {
                XDocument XMLRespuesta = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement("Usuario",                            
                            new XElement("Codigo", new XCData("1")),
                            new XElement("Mensaje", new XCData("ERROR DE LA VIDA!!!")))
                    );

                StringWriter wr = new StringWriter();
                XMLRespuesta.Save(wr);

                return wr.ToString();
            }
        }

        /// <summary>
        /// ingreso de usuarios.
        /// SOlicita parametros como usuario, contraseña.
        /// </summary>
        /// <returns>
        /// String -xml-
        /// </returns>
        [WebMethod]
        public string IniciarSesion(String user, String clave)
        {
            try
            {
                string phrase= ServicoUsuarios.logIn(user, clave);
                string[] result = phrase.Split('-');

                XDocument XMLRespuesta = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement("LogIn",
                            new XElement("Usuario", new XCData(user)),
                            new XElement("id", new XCData(result[2])),
                            new XElement("Codigo", new XCData(result[0])),
                            new XElement("Mensaje", new XCData(result[1])))
                    );

                StringWriter wr = new StringWriter();
                XMLRespuesta.Save(wr);

                return wr.ToString();

            }
            catch (Exception)
            {
                XDocument XMLRespuesta = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement("LogIn",
                            new XElement("Codigo", new XCData("1")),
                            new XElement("Mensaje", new XCData("ERROR DE LA VIDA!!!")))
                    );

                StringWriter wr = new StringWriter();
                XMLRespuesta.Save(wr);

                return wr.ToString();
            }
        }

        /// <summary>
        /// solicitur de viaje nuevo.
        /// SOlicita parametro id Usuario, Origne y disteno.
        /// </summary>
        /// <returns>
        /// String -xml-
        /// </returns>
        [WebMethod]
        public String SolicitarViaje(int idUsuario, String Origen, String Destino)
        {
            try
            {
                string phrase = ServicoUsuarios.PedirViaje(idUsuario, Origen, Destino);
                string[] result = phrase.Split('-');

                XDocument XMLRespuesta = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement("Viaje",
                            new XElement("Origen", new XCData(Origen)),
                            new XElement("Destino", new XCData(Destino)),
                            new XElement("Piloto", new XCData(result[2])),
                            new XElement("Codigo", new XCData(result[0])),
                            new XElement("Mensaje", new XCData(result[1])))
                    );

                StringWriter wr = new StringWriter();
                XMLRespuesta.Save(wr);

                return wr.ToString();

            }
            catch (Exception ex)
            {
                return "Fracaso total";
            }
        }

        /// <summary>
        /// Finalizar viaje pendiente.
        /// SOlicita parametro id Usuario
        /// </summary>
        /// <returns>
        /// String -xml-
        /// </returns>
        [WebMethod]
        public String TerminarViajeUsuario(int idUsuario)
        {
            try
            {
                string phrase = ServicoUsuarios.FinalizarViaje(idUsuario);
                string[] result = phrase.Split('-');

                XDocument XMLRespuesta = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement("Viaje_Usuario",
                            new XElement("Codigo", new XCData(result[0])),
                            new XElement("Mensaje", new XCData(result[1])))
                    );

                StringWriter wr = new StringWriter();
                XMLRespuesta.Save(wr);

                return wr.ToString();

            }
            catch (Exception ex)
            {
                return "Fracaso total";
            }
        }

        /*
           Instancia global para el WS de clientes
       */

        WS_Rastreo.RastreoSoapClient ServicoRastreo = new WS_Rastreo.RastreoSoapClient();


        /// <summary>
        /// Consulta la informacion del viaje por el usuario.
        /// SOlicita parametros como ID Nombre.
        /// </summary>
        /// <returns>
        /// String -xml-
        /// </returns>
        [WebMethod]
        public string ConsultarViajeUsuario(int idUsuario)
        {
            try
            {
                string phrase = ServicoRastreo.RastrearViajeUsuario(idUsuario);
                string[] result = phrase.Split('-');

                if(result.Count() > 3)
                {
                    XDocument XMLRespuesta = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement("Rastreo_Viaje",
                            new XElement("Nombre", new XCData(result[1])),
                            new XElement("Piloto", new XCData(result[2])),
                            new XElement("Marca_vehiculo", new XCData(result[3])),
                            new XElement("Linea_vehiculo", new XCData(result[4])),
                            new XElement("Placa_vehiculo", new XCData(result[5])),
                            new XElement("Origen", new XCData(result[6])),
                            new XElement("Destino", new XCData(result[7])),                            
                            new XElement("Fecha", new XCData(result[8])),
                            new XElement("Codigo", new XCData(result[0])))
                    );

                    StringWriter wr = new StringWriter();
                    XMLRespuesta.Save(wr);

                    return wr.ToString();
                }
                else
                {
                    XDocument XMLRespuesta = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement("Rastreo_Viaje",
                            new XElement("Codigo", new XCData(result[0])),
                            new XElement("Mensaje", new XCData(result[1])))
                    );

                    StringWriter wr = new StringWriter();
                    XMLRespuesta.Save(wr);

                    return wr.ToString();
                }                

            }
            catch (Exception)
            {
                XDocument XMLRespuesta = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement("Rastreo_Viaje",
                            new XElement("Codigo", new XCData("1")),
                            new XElement("Mensaje", new XCData("ERROR DE LA VIDA!!!")))
                    );

                StringWriter wr = new StringWriter();
                XMLRespuesta.Save(wr);

                return wr.ToString();
            }
        }


        /// <summary>
        /// Consulta la informacion del viaje por el piloto.
        /// SOlicita parametros como ID Nombre.
        /// </summary>
        /// <returns>
        /// String -xml-
        /// </returns>
        [WebMethod]
        public string ConsultarViajePiloto(int idPiloto)
        {
            try
            {
                string phrase = ServicoRastreo.RastrearViajePiloto(idPiloto);
                string[] result = phrase.Split('-');

                if (result.Count() > 3)
                {
                    XDocument XMLRespuesta = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement("Rastreo_Viaje",                            
                            new XElement("Nombre", new XCData(result[2])),
                            new XElement("Cliente", new XCData(result[1])),
                            new XElement("Marca_vehiculo", new XCData(result[3])),
                            new XElement("Linea_vehiculo", new XCData(result[4])),
                            new XElement("Placa_vehiculo", new XCData(result[5])),
                            new XElement("Origen", new XCData(result[6])),
                            new XElement("Destino", new XCData(result[7])),
                            new XElement("Fecha", new XCData(result[8])),
                            new XElement("Codigo", new XCData(result[0])))
                    );

                    StringWriter wr = new StringWriter();
                    XMLRespuesta.Save(wr);

                    return wr.ToString();
                }
                else
                {
                    XDocument XMLRespuesta = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement("Rastreo_Viaje",
                            new XElement("Codigo", new XCData(result[0])),
                            new XElement("Mensaje", new XCData(result[1])))
                    );

                    StringWriter wr = new StringWriter();
                    XMLRespuesta.Save(wr);

                    return wr.ToString();
                }

            }
            catch (Exception)
            {
                XDocument XMLRespuesta = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement("Rastreo_Viaje",
                            new XElement("Codigo", new XCData("1")),
                            new XElement("Mensaje", new XCData("ERROR DE LA VIDA!!!")))
                    );

                StringWriter wr = new StringWriter();
                XMLRespuesta.Save(wr);

                return wr.ToString();
            }
        }
    }
}
