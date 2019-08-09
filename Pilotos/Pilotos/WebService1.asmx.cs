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
        public bool AgregarPiloto()
        {
            try
            {
                using (Ubr_201212487Entities db = new Ubr_201212487Entities())
                {
                    Piloto pl = new Piloto();
                    pl.id = Convert.ToInt32(getUtimoPiloto());
                    pl.Nombre = "Piloto " + pl.id;
                    pl.Automovil = "Automovil " + pl.id;

                    db.Piloto.Add(pl);
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
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
                        db.Piloto.Add(new Piloto());

                        Piloto pl = db.Piloto.ToList().Last();

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
    }
}
