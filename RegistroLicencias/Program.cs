using System;
using System.Data.Odbc;

namespace RegistroLicencias
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string usuario = "";
            string contrasena = "";
            string refcats = "";
            string licencia = "";
            string proyecto = "";
            string modalidad_proyecto = "";
            string m2 = "";
            string presupuesto = "";
            string impuesto = "";
            string estampilla = "";
            string vigencia = "";
            string id_curaduria = "";
            string matricula_inmobiliaria = "";
            string estado = "";
            string documento = "";
            string ruta_documento = "";

            int i = 1;
            foreach (var parametro in args)
            {
                switch (i)
                {
                    case 1:
                        usuario = parametro;
                        break;
                    case 2:
                        contrasena = parametro;
                        break;
                    case 3:
                        refcats = parametro;
                        break;
                    case 4:
                        licencia = parametro;
                        break;
                    case 5:
                        proyecto = parametro;
                        break;
                    case 6:
                        modalidad_proyecto = parametro;
                        break;
                    case 7:
                        m2 = parametro;
                        break;
                    case 8:
                        presupuesto = parametro;
                        break;
                    case 9:
                        impuesto = parametro;
                        break;
                    case 10:
                        estampilla = parametro;
                        break;
                    case 11:
                        vigencia = parametro;
                        break;
                    case 12:
                        id_curaduria = parametro;
                        break;
                    case 13:
                        matricula_inmobiliaria = parametro;
                        break;
                    case 14:
                        estado = parametro;
                        break;
                    case 15:
                        documento = parametro;
                        break;
                    case 16:
                        ruta_documento = parametro;
                        break;
                    default:
                        break;
                }
                
                i++;
            }
            Console.WriteLine($"usuario: {usuario}");
            Console.WriteLine($"contrasena: {contrasena}");
            Console.WriteLine($"refcats {refcats}");
            Console.WriteLine($"licencia {licencia}");
            Console.WriteLine($"proyecto {proyecto}");
            Console.WriteLine($"modalidad_proyecto {modalidad_proyecto}");
            Console.WriteLine($"m2 {m2}");
            Console.WriteLine($"presupuesto {presupuesto}");
            Console.WriteLine($"impuesto {impuesto}");
            Console.WriteLine($"estampilla {estampilla}");
            Console.WriteLine($"vigencia {vigencia}");
            Console.WriteLine($"id_curaduria {id_curaduria}");
            Console.WriteLine($"matricula_inmobiliaria {matricula_inmobiliaria}");
            Console.WriteLine($"estado {estado}");
            Console.WriteLine($"documento {documento}");
            Console.WriteLine($"ruta_documento {ruta_documento}");
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
