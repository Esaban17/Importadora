using ImportadoraModels;
using Newtonsoft.Json;
using System.Text;

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

        public static async System.Threading.Tasks.Task<ImportadoraModels.Usuario> GetUsuario(int id)
        {


            // var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            // var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.GetAsync(baseurl + $"Usuario/GetUsuario/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<ImportadoraModels.Usuario>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());

            }

        }

        public static async System.Threading.Tasks.Task<ImportadoraModels.GeneralResult> Create(Usuario usuario)
        {


            // var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            // var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var jsonUser = JsonConvert.SerializeObject(usuario);

            var stringContent = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, baseurl + "Usuario/Create");

            request.Content = stringContent;

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<ImportadoraModels.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());

            }

        }

        public static async System.Threading.Tasks.Task<ImportadoraModels.GeneralResult> Update(Usuario usuario)
        {

            // var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            // var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var jsonUser = JsonConvert.SerializeObject(usuario);

            var stringContent = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Put, baseurl + "Usuario/Update");

            request.Content = stringContent;

            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<ImportadoraModels.GeneralResult>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());

            }

        }

        public static async System.Threading.Tasks.Task<ImportadoraModels.Usuario> Delete(int id)
        {

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.DeleteAsync(baseurl + $"Usuario/Delete/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<ImportadoraModels.Usuario>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());

            }

        }
    }
}
