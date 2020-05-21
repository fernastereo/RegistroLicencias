using System;
using System.Data.Odbc;

namespace RegistroLicencias
{
    class Program
    {
        static void Main(string[] args)
        {

            int i = 1;
            foreach (var parametro in args)
            {
                switch (i)
                {
                    case 1:
                        string usuario = parametro;
                        break;
                    case 2:
                        string contrasena = parametro;
                        break;
                    case 3:
                        string refcats = parametro;
                        break;
                    case 4:
                        string licencia = parametro;
                        break;
                    case 5:
                        string proyecto = parametro;
                        break;
                    case 6:
                        string modalidad_proyecto = parametro;
                        break;
                    case 7:
                        string m2 = parametro;
                        break;
                    case 8:
                        string presupuesto = parametro;
                        break;
                    case 9:
                        string impuesto = parametro;
                        break;
                    case 10:
                        string estampilla = parametro;
                        break;
                    case 11:
                        string vigencia = parametro;
                        break;
                    case 12:
                        string id_curaduria = parametro;
                        break;
                    case 13:
                        string matricula_inmobiliaria = parametro;
                        break;
                    case 14:
                        string estado = parametro;
                        break;
                    case 15:
                        string documento = parametro;
                        break;
                    case 16:
                        string ruta_documento = parametro;
                        break;
                    default:
                        break;
                }
                
                i++;
            }
            Console.WriteLine($"IDTabla: {idTabla}");
            Console.WriteLine($"IDExpediente: {idExpediente}");
            Console.WriteLine($"Abriendo BD: {rutaDB}");
            //"cliente1",
            //"12345678",
            //"010500810028000",
            //"6",
            //"RECONOCIMIENTO DE LA EXISTENCIA DE UNA EDIFICACION",
            //"RECONOCIMIENTO",
            //"0",
            //"0",
            //"0",
            //"0",
            //"2020",
            //"6609",
            //"060 - 91349",
            //"RESOLUCION EXPEDIDA",
            //"006609 - 004487.pdf"

        }
        private static string RutaDB()
        {
            string fic;
            fic = (System.AppDomain.CurrentDomain.BaseDirectory + "\\Config.dat");            
            string Texto;
            System.IO.StreamReader sr = new System.IO.StreamReader(fic);
            Texto = sr.ReadToEnd();
            sr.Close();
            return Texto;
        }
    }
}
