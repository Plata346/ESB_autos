using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Services;
using Usuarios.Model;

namespace Usuarios
{
    /// <summary>
    /// Descripción breve de Actividades_Usuario
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class Actividades_Usuario : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        /*
            Metodo de registro de usuarios
            Permite agregar nuevos usuarios al sistema
        */
        /// <summary>
        /// Registrar nuevos usuarios.
        /// SOlicita parametros como Nombre, telefono, usuario, contraseña.
        /// </summary>
        /// <returns>
        /// Bool
        /// </returns>
        [WebMethod]
        public bool RegistrarUsuario(String user, String tel, String unique, String clave)
        {
            try
            {
                using (Ubr_201212487Entities db = new Ubr_201212487Entities())
                {
                    usuario usr = new usuario();
                    usr.id = Convert.ToInt32(getUtimoUsuario());
                    usr.nombre = user;
                    usr.telefono = tel;
                    usr.usr = unique;
                    usr.pasword =clave;
                    
                    db.usuario.Add(usr);
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private string getUtimoUsuario()
        {
            string ultimoCOdigo = "";

            try
            {
                using (TransactionScope trn = new TransactionScope(TransactionScopeOption.Suppress))
                {
                    using (Ubr_201212487Entities db = new Ubr_201212487Entities())
                    {
                        //db.usuario.Add(new usuario());

                        usuario pl = db.usuario.ToList().Last();

                        if (pl.Equals(null))
                        {
                            ultimoCOdigo = "1";
                        }
                        else
                        {
                            ultimoCOdigo = (pl.id + 1).ToString();
                        }
                    }
                }
            }
            catch
            {
                ultimoCOdigo = "1";
            }
            return ultimoCOdigo;
        }

        /// <summary>
        /// ingreso de usuarios.
        /// SOlicita parametros como usuario, contraseña.
        /// </summary>
        /// <returns>
        /// String (separado por '-')
        /// </returns>
        [WebMethod]
        public String logIn(String user, String clave)
        {
            try
            {
                using (Ubr_201212487Entities db = new Ubr_201212487Entities())
                {
                    //consulta que obtiene el usuario y sus propiedades
                    usuario selec = (from tabla_usuario in db.usuario where tabla_usuario.usr.Equals(user) select tabla_usuario).Single();

                    if (selec != null)
                    {
                        //valida si la contraseña y el usuario coinsiden
                        if (selec.usr == user && selec.pasword == clave)
                        {
                            return "0-Exito-"+selec.id;
                        }
                        else
                        {
                            return "1-La contraseña es invalida.-0";
                        }
                    }
                    else
                    {
                        return "1-El usuario no existe.-0";
                    }
                }                
            }
            catch (Exception ex)
            {
                return "1-El usuario no existe.-0";
            }
        }

        /// <summary>
        /// solicitur de viaje nuevo.
        /// SOlicita parametro id Usuario, Origne y disteno.
        /// </summary>
        /// <returns>
        /// String (separado por '-')
        /// </returns>
        [WebMethod]
        public String PedirViaje(int idUsuario, String Origen, String Destino)
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

                    piloto vv = null;
                    foreach (viaje v in selec)
                    {
                        if(v.activo.Equals(true))
                        {
                            flag = false;
                            vv = v.piloto;
                            break;
                        }
                    }
                    

                    if (!flag)
                    {
                        //existe un viaje activo
                        return "1-Usted tiene un viaje activo.-"+vv.nombre;
                        
                    }
                    else
                    {
                        //asignar nuevo viaje
                        viaje viajesote = new viaje();
                        viajesote.id = Convert.ToInt32(getUtimoViaje());
                        viajesote.fecha = DateTime.Now; ;
                        viajesote.origen = Origen;
                        viajesote.destino = Destino;
                        viajesote.usuario_id = idUsuario;
                        viajesote.activo = true;

                        //listar todos los pilotos asignados
                        List<viaje> viajes = db.viaje.ToList();

                        //listar todos los pilotos 
                        List<piloto> pilotos = db.piloto.ToList();

                        //bandera
                        bool thishone = true;
                        //asiganr piloto
                        foreach(piloto pl in pilotos)
                        {
                            foreach (viaje vj in viajes)
                            {
                                thishone = true;
                                if (vj.piloto_id == pl.id && vj.activo.Equals(true) )
                                {
                                    //piloto existe y tiene viaje activo
                                    thishone = false;
                                    break;
                                }                                                               
                            }
                            if (thishone)
                            {
                                viajesote.piloto_id = pl.id;
                                break;
                            }
                        }

                        if (!thishone) return "1-No hay pilotos disponibles-Sad But True!";

                        db.viaje.Add(viajesote);
                        db.SaveChanges();
                        return "0-Nuevo viaje asigndo.-"+viajesote.piloto.nombre;
                    }
                }
            }
            catch (Exception ex)
            {
                return "1-El viaje no se pudo registrar-0";
            }       
        }


        private string getUtimoViaje()
        {
            string ultimoCOdigo = "";

            try
            {
                using (TransactionScope trn = new TransactionScope(TransactionScopeOption.Suppress))
                {
                    using (Ubr_201212487Entities db = new Ubr_201212487Entities())
                    {
                        //db.usuario.Add(new usuario());

                        viaje pl = db.viaje.ToList().Last();

                        if (pl.Equals(null))
                        {
                            ultimoCOdigo = "1";
                        }
                        else
                        {
                            ultimoCOdigo = (pl.id + 1).ToString();
                        }
                    }
                }
            }
            catch
            {
                ultimoCOdigo = "1";
            }
            return ultimoCOdigo;
        }


        /// <summary>
        /// Finalizar viaje pendiente.
        /// SOlicita parametro id Usuario
        /// </summary>
        /// <returns>
        /// String (separado por '-')
        /// </returns>
        [WebMethod]
        public String FinalizarViaje(int idUsuario)
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

                    piloto vv = null;
                    foreach (viaje v in selec)
                    {
                        if (v.activo.Equals(true))
                        {
                            flag = false;
                            vv = v.piloto;
                            v.activo = false;

                            //db.Entry(viaje).State = System.Data.EntityState.Modified;                            
                            db.SaveChanges();
                            break;
                        }
                    }


                    if (!flag)
                    {
                        //existe un viaje activo
                        return "0-Usted finalizo el viaje con " + vv.nombre;

                    }
                    else
                    {
                        return "1-No hay vijes apra finalizar-Sad But True!";                      
                    }
                }
            }
            catch (Exception ex)
            {
                return "1-El viaje no se pudo finalizar-0";
            }
        }



    }
}
