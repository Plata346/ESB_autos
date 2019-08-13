using RastreoCarro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Services;


namespace RastreoCarro
{
    /// <summary>
    /// Descripción breve de Rastreo
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Rastreo : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        /// <summary>
        /// Consulta la informacion del viaje por el usuario.
        /// SOlicita parametros como ID Nombre.
        /// </summary>
        /// <returns>
        /// String (separado por '-')
        /// </returns>
        [WebMethod]
        public String RastrearViajeUsuario(int idUsuario)
        {
            try
            {
                using (Ubr_201212487Entities db = new Ubr_201212487Entities())
                {
                    List<viaje> selec = null;
                    bool flag = true;

                    //consulta que obtiene el usuario y sus propiedades
                    try
                    {
                        selec = (from tabla_viaje in db.viaje
                                 where tabla_viaje.usuario_id.Equals(idUsuario)
                                 select tabla_viaje).ToList();

                    }
                    catch
                    {
                        flag = false;
                    }
                    
                    viaje vv = null;
                    foreach (viaje v in selec)
                    {
                        if (v.activo.Equals(true))
                        {
                            flag = false;
                            vv = v;
                            break;
                        }
                    }

                    if (!flag)
                    {
                        //existe un viaje activo
                        return "0"+
                            "-"+vv.usuario.nombre+
                            "-" + vv.piloto.nombre +
                            "-" + vv.piloto.marca_carro +
                            "-" + vv.piloto.linea_carro +
                            "-" + vv.piloto.placa_carro+
                            "-" + vv.origen +
                            "-" + vv.destino +
                            "-" + vv.fecha 
                            ;

                    }
                    else
                    {
                        return "1-No hay vijes pendientes-Sad But True!";
                    }
                }                
            }
            catch (Exception ex)
            {
                return "1-Que esta pasandaaaaaaa-ya no valide esto!";
            }
        }

        /// <summary>
        /// Consulta la informacion del viaje por el piloto.
        /// SOlicita parametros como ID Nombre.
        /// </summary>
        /// <returns>
        /// String (separado por '-')
        /// </returns>
        [WebMethod]
        public String RastrearViajePiloto(int idPiloto)
        {
            try
            {
                using (Ubr_201212487Entities db = new Ubr_201212487Entities())
                {
                    List<viaje> selec = null;
                    bool flag = true;

                    //consulta que obtiene el usuario y sus propiedades
                    try
                    {
                        selec = (from tabla_viaje in db.viaje
                                 where tabla_viaje.piloto_id.Equals(idPiloto)
                                 select tabla_viaje).ToList();

                    }
                    catch
                    {
                        flag = false;
                    }

                    viaje vv = null;
                    foreach (viaje v in selec)
                    {
                        if (v.activo.Equals(true))
                        {
                            flag = false;
                            vv = v;
                            break;
                        }
                    }

                    if (!flag)
                    {
                        //existe un viaje activo
                        return "0" +
                            "-" + vv.usuario.nombre +
                            "-" + vv.piloto.nombre +
                            "-" + vv.piloto.marca_carro +
                            "-" + vv.piloto.linea_carro +
                            "-" + vv.piloto.placa_carro +
                            "-" + vv.origen +
                            "-" + vv.destino +
                            "-" + vv.fecha
                            ;

                    }
                    else
                    {
                        return "1-No hay vijes pendientes-Sad But True!";
                    }
                }
            }
            catch (Exception ex)
            {
                return "1-Que esta pasandaaaaaaa-ya no valide esto!";
            }
        }


    }
}
