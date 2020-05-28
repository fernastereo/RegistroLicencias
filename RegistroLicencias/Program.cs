using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.Data.Odbc;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RegistroLicencias
{
    class Program
    {
        private const string bucketName = "";
        private const string awsAccessKey = ""; 
        private const string awsSecretKey = ""; 
        
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USWest1;
        private static string ruta_documento = "";
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
            string keyName = "";
            string midas = "";

            Console.WriteLine("Subiendo archivo a web");
            int i = 1;
            foreach (var parametro in args)
            {
                switch (i)
                {
                    case 1:
                        ruta_documento = parametro;
                        Console.WriteLine(ruta_documento);
                        break;
                    case 2:
                        keyName = parametro;
                        Console.WriteLine(keyName);
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
                Console.WriteLine("Resolución cargada en página Web satisfactoriamente");
                

                if (midas == "1")
                {
                    EnviarMidas().Wait();
                }

            }
        }

        public static async Task<string> EnviarMidas()
        {
            //"C:\projects\pruebamidas.pdf" "1ca/resol/pruebamidas.pdf" "1" "cliente1" "12345678" "010500810028000" "6" "RECONOCIMIENTO DE LA EXISTENCIA DE UNA EDIFICACION" "RECONOCIMIENTO" "320" "1679900" "89200" "23000" "2020" "6609" "060 - 91349" "RESOLUCION EXPEDIDA"
            Console.WriteLine("Enviando Información a Midas");
            //Cargando informacion a objeto encargado de enviar la licencia
            var resolucionEnviar = new Resolucion();
            resolucionEnviar.Usuario = usuario;
            resolucionEnviar.Contrasena = contrasena;
            resolucionEnviar.Refcats = refcats;
            resolucionEnviar.Licencia = licencia;
            resolucionEnviar.Proyecto = proyecto;
            resolucionEnviar.Modalidad_Proyecto = modalidad_proyecto;
            resolucionEnviar.M2 = m2;
            resolucionEnviar.Presupuesto = presupuesto;
            resolucionEnviar.Impuesto = impuesto;
            resolucionEnviar.Estampilla = estampilla;
            resolucionEnviar.Vigencia = vigencia;
            resolucionEnviar.ID_Curaduria = id_curaduria;
            resolucionEnviar.Matricula_Inmobiliaria = matricula_inmobiliaria;
            resolucionEnviar.Estado = estado;
            //Convertir el archivo a base64            
            Byte[] bytes = File.ReadAllBytes(ruta_documento);
            String file = Convert.ToBase64String(bytes);
            resolucionEnviar.Documento = file;

            //Serializando el objeto a JSon para enviarlo
            string jsonString;
            jsonString = JsonSerializer.Serialize(resolucionEnviar);

            //Enviando los datos de la licencia al endpoint

            HttpClient client = new HttpClient();
            StringContent queryString = new StringContent(jsonString, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(new Uri("https://qmap.quspide.co/api/Curadurias/UploadData"), queryString);

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Respuesta Midas: ");
            Console.WriteLine(responseBody);
            return responseBody;
        }
    }
}
