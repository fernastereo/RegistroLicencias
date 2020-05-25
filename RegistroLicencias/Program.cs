using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.Data.Odbc;

namespace RegistroLicencias
{
    class Program
    {            
        //Ususario: user-web-curadurias
        //ID de Clave de Acceso: AKIAWFJEJP52LEJ6KX67
        //Password: F7mfLbT7p9QCNJQdgtiDM2GjwueI0k93/hFzSLmt

        private const string bucketName = "web-curadurias";
        private const string awsAccessKey = "AKIAWFJEJP52LEJ6KX67";
        private const string awsSecretKey = "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";
        //private const string keyName = "1ca/resol/_nuevo.pdf";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USWest1;
        private static string usuario = "";
        private static string contrasena = "";
        private static string refcats = "";
        private static string licencia = "";
        private static string proyecto = "";
        private static string modalidad_proyecto = "";
        private static string m2 = "";
        private static string presupuesto = "";
        private static string impuesto = "";
        private static string estampilla = "";
        private static string vigencia = "";
        private static string id_curaduria = "";
        private static string matricula_inmobiliaria = "";
        private static string estado = "";
        private static string documento = "";

        /// <summary>
        /// Modulo para el cargue de resoluciones a un bucket S3 para ser consultado por las diferentes
        /// páginas web de las curadurias urbanas.
        /// <para>
        /// El módulo toma la ruta de un archivo pdf en disco y lo guarda en la carpeta resol de la 
        /// correspondiente Curaduria
        /// </para>
        /// </summary>
        /// <param name="args">Array con los datos de la resolucion
        /// 0: ruta_documento = Ruta del documento a cargar
        /// 1: keyName = carpeta dentro del bucket donde se guardará el archivo (ej. 1ca/resol/_nuevo.pdf)
        /// 2: midas = 1 si debe enviar datos a midas / 0 si no debe enviarlos
        /// 3: usuario = usuario para conectarse a midas
        /// 4: contrasena = contraseña para conectarse a midas
        /// 5: refcats = referencia catastral del predio
        /// 6: licencia = numero de licencia
        /// 7: proyecto = tipo de licencia
        /// 8: modalidad_proyecto = modalidad de licencia
        /// 9: m2 = area
        /// 10: presupuesto = valor presupuesto
        /// 11: impuesto = valor impuesto delineacion
        /// 12: estampilla = valor impuesto estampilla
        /// 13: vigencia = año de expedicion de la licencia
        /// 14: id_curaduria = IDExpediente de la base de datos de SAC
        /// 15: matricula_inmobiliaria = matricula inmobiliaria del predio
        /// 16: estado = Estado del proyecto ('RESOLUCION EXPEDIDA')
        /// 17: documento = Nombre del documento pdf que se envió
        /// </param>
        static void Main(string[] args)
        {
            string ruta_documento = "";
            string keyName = "";
            string midas = "";

            int i = 1;
            foreach (var parametro in args)
            {
                switch (i)
                {
                    case 1:
                        ruta_documento = parametro;
                        break;
                    case 2:
                        keyName = parametro;
                        break;
                    case 3:
                        midas = parametro;
                        break;
                    case 4:
                        usuario = parametro;
                        break;
                    case 5:
                        contrasena = parametro;
                        break;
                    case 6:
                        refcats = parametro;
                        break;
                    case 7:
                        licencia = parametro;
                        break;
                    case 8:
                        proyecto = parametro;
                        break;
                    case 9:
                        modalidad_proyecto = parametro;
                        break;
                    case 10:
                        m2 = parametro;
                        break;
                    case 11:
                        presupuesto = parametro;
                        break;
                    case 12:
                        impuesto = parametro;
                        break;
                    case 13:
                        estampilla = parametro;
                        break;
                    case 14:
                        vigencia = parametro;
                        break;
                    case 15:
                        id_curaduria = parametro;
                        break;
                    case 16:
                        matricula_inmobiliaria = parametro;
                        break;
                    case 17:
                        estado = parametro;
                        break;
                    case 18:
                        documento = parametro;
                        break;
                    default:
                        break;
                }

                i++;
            }

            if (!String.IsNullOrEmpty(ruta_documento))
            {
                var client = new AmazonS3Client(awsAccessKey, awsSecretKey, bucketRegion);

                var uploadRequest = new TransferUtilityUploadRequest
                {
                    FilePath = ruta_documento,
                    BucketName = bucketName,
                    Key = keyName,
                    CannedACL = S3CannedACL.PublicRead
                };

                var fileTransferUtility = new TransferUtility(client);
                fileTransferUtility.Upload(uploadRequest);
                Console.WriteLine("Resolución cargada en página Web ssatisfactoriamente");

                if (midas == "1")
                {
                    enviarMidas();
                }

            }
        }

        static void enviarMidas()
        {
            Console.WriteLine("Envio Datos a Midas");
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
    }
}
