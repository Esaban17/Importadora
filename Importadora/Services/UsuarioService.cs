using Newtonsoft.Json;

namespace Importadora.Services
{
    public static class UsuarioService
    {

        private static int timeout = 30;
        private static string baseurl = "https://localhost:7135/api/";

        public static async System.Threading.Tasks.Task<IEnumerable<ImportadoraModels.Usuario>> GetUsuarios()
        {


            // var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            // var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.GetAsync(baseurl + "Usuario/GetUsuarios");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ImportadoraModels.Usuario>>(await response.Content.ReadAsStringAsync());
            }

            else
            {
                throw new Exception(response.StatusCode.ToString());

            }


        }

    }
}
