using ImportadoraModels;
using Newtonsoft.Json;
using System.Text;

namespace Importadora.Services
{
    public static class VehiculoService
    {

        private static int timeout = 30;
        private static string baseurl = "https://localhost:7135/api/";

        public static async System.Threading.Tasks.Task<IEnumerable<ImportadoraModels.Vehiculo>> GetVehiculos()
        {

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.GetAsync(baseurl + "Vehiculo/GetVehiculos");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ImportadoraModels.Vehiculo>>(await response.Content.ReadAsStringAsync());
            }

            else
            {
                throw new Exception(response.StatusCode.ToString());

            }

        }

        public static async System.Threading.Tasks.Task<ImportadoraModels.Vehiculo> GetVehiculo(int id)
        {

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var response = await httpClient.GetAsync(baseurl + $"Vehiculo/GetVehiculo/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<ImportadoraModels.Vehiculo>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());

            }

        }

        public static async System.Threading.Tasks.Task<ImportadoraModels.GeneralResult> Create(Vehiculo vehiculo)
        {


            // var json_ = Newtonsoft.Json.JsonConvert.SerializeObject(object_to_serialize);
            // var content = new StringContent(json_, Encoding.UTF8, "application/json");
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };


            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            httpClient.Timeout = TimeSpan.FromSeconds(timeout);

            var jsonUser = JsonConvert.SerializeObject(vehiculo);

            var stringContent = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, baseurl + "Vehiculo/Create");

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

    }
}
