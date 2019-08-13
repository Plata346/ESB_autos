using Pilotos.Model;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Services;
using TransactionScope = System.Transactions.TransactionScope;

namespace Pilotos
{
    /// <summary>
    /// Descripción breve de WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }

        [WebMethod]
        public String AgregarPiloto(String name, String tel, String marca, String linea, String placa)
        {
            try
            {
                using (Ubr_201212487Entities db = new Ubr_201212487Entities())
                {
                    piloto pl = new piloto();
                    pl.id = Convert.ToInt32(getUtimoPiloto());
                    pl.nombre = name;
                    pl.telefono =tel;
                    pl.marca_carro = marca;
                    pl.linea_carro = linea;
                    pl.placa_carro = placa;

                    db.piloto.Add(pl);
                    db.SaveChanges();

                    return "0-Piloto agregado con Exito.-" + pl.id;
                }                
            }
            catch (Exception ex)
            {
                return "1-Error al agregar nuevo piloto.-ERROR DE LA VIDA!";
            }
        }

        private string getUtimoPiloto()
        {
            string ultimoCOdigo = "";

            try
            {
                using (TransactionScope trn = new TransactionScope(TransactionScopeOption.Suppress))
                {
                    using (Ubr_201212487Entities db = new Ubr_201212487Entities())
                    {
                        db.piloto.Add(new piloto());

                        piloto pl = db.piloto.ToList().Last();

                        if (pl.Equals(null))
                        {
                            ultimoCOdigo = "1";
                        }
                        else
                        {
                            ultimoCOdigo = (pl.id + 1).ToString();
                        }

                        //int intCurrentId = Convert.ToInt32(db.Database.SqlQuery<decimal>("SELECT IDENT_CURRENT ('Col_ATM')", new object[0]).FirstOrDefault());

                        //ultimoCOdigo = (intCurrentId + 1).ToString();
                    }
                }
            }
            catch
            {
                ultimoCOdigo = "1";
            }
            return ultimoCOdigo;
        }

        [WebMethod]
        public String FinalizarViaje(int idPiloto)
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

                    usuario uu = null;
                    foreach (viaje v in selec)
                    {
                        if (v.activo.Equals(true))
                        {
                            flag = false;
                            uu = v.usuario;
                            v.activo = false;

                            //db.Entry(viaje).State = System.Data.EntityState.Modified;                            
                            db.SaveChanges();
                            break;
                        }
                    }


                    if (!flag)
                    {
                        //existe un viaje activo
                        return "0-Usted finalizo el viaje con " + uu.nombre;

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
