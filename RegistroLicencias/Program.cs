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
            string keyName = "";

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
                    case 17:
                        keyName = parametro;
                        break;
                    default:
                        break;
                }

                i++;
            }

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

    }
}
