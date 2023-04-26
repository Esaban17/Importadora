using ImportadoraModels;
using Newtonsoft.Json;
using System.Text;

namespace Importadora.Services
{
    public static class CompraService
    {

        private static int timeout = 30;
        private static string baseurl = "https://localhost:7135/api/";

        public static async System.Threading.Tasks.Task<IEnumerable<ImportadoraModels.Compra>> GetCompras()
        {


            // var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            // var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.GetAsync(baseurl + "Compra/GetCompras");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ImportadoraModels.Compra>>(await response.Content.ReadAsStringAsync());
            }

            else
            {
                throw new Exception(response.StatusCode.ToString());

            }


        }

        public static async System.Threading.Tasks.Task<ImportadoraModels.Compra> GetCompra(int id)
        {


            // var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            // var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.GetAsync(baseurl + $"Compra/GetCompra/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<ImportadoraModels.Compra>(await response.Content.ReadAsStringAsync());
            }

            else
            {
                throw new Exception(response.StatusCode.ToString());

            }

        }

        public static async System.Threading.Tasks.Task<ImportadoraModels.GeneralResult> Create(Compra compra)
        {


            // var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            // var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var jsonUser = JsonConvert.SerializeObject(compra);

            var stringContent = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, baseurl + "Compra/Create");

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

        public static async System.Threading.Tasks.Task<ImportadoraModels.GeneralResult> Update(Compra compra)
        {

            // var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            // var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var jsonUser = JsonConvert.SerializeObject(compra);

            var stringContent = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Put, baseurl + "Compra/Update");

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

        public static async System.Threading.Tasks.Task<ImportadoraModels.Compra> Delete(int id)
        {

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.DeleteAsync(baseurl + $"Compra/Delete/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<ImportadoraModels.Compra>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());

            }

        }

    }
}
