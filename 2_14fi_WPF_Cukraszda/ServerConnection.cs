using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.Net.Http;

namespace _2_14fi_WPF_Cukraszda
{
    public class ServerConnection
    {
        private HttpClient client = new HttpClient();
        private string baseURL = "";
        public ServerConnection(string url)
        {
            baseURL = url;
        }
        public async Task<List<Cake>> GetCakes()
        {
            List<Cake> all = new List<Cake>();
            string url = baseURL + "/cake";

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseInString = await response.Content.ReadAsStringAsync();
                all = JsonConvert.DeserializeObject<List<Cake>>(responseInString);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return all;
        }
        public async Task<bool> PostCake(Cake oneCake)
        {
            string url = baseURL + "/cake";

            try
            {
                var jsonData = new
                {
                    newName = oneCake.name,
                    newPicture = oneCake.picture,
                    newDescription = oneCake.description,
                    newAllergenes = oneCake.allergenes,
                    newPrice = oneCake.price,
                    newStock = oneCake.stock
                };
                string jsonString = JsonConvert.SerializeObject(jsonData);
                StringContent sendThis = new StringContent(jsonString, Encoding.UTF8, "Application/JSON");
                HttpResponseMessage response = await client.PostAsync(url, sendThis);
                string responseString;
                if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    responseString = await response.Content.ReadAsStringAsync();
                    Message errorCake = JsonConvert.DeserializeObject<Message>(responseString);
                    MessageBox.Show(errorCake.message, "Hiba");
                    return false;
                }
                response.EnsureSuccessStatusCode();
                responseString = await response.Content.ReadAsStringAsync();
                Message successCake = JsonConvert.DeserializeObject<Message>(responseString);
                MessageBox.Show(successCake.message, "Siker :)");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            return true;
        }
        public async Task<bool> BuyCake(Cake oneCake)
        {
            string url = baseURL + "/buyCake";

            try
            {
                var jsonData = new
                {
                    id = oneCake.id,
                    count = oneCake.orderCount
                };
                string jsonString = JsonConvert.SerializeObject(jsonData);
                StringContent sendThis = new StringContent(jsonString, Encoding.UTF8, "Application/JSON");
                HttpResponseMessage response = await client.PostAsync(url, sendThis);
                response.EnsureSuccessStatusCode();
                string responseString = await response.Content.ReadAsStringAsync();
                Message successCake = JsonConvert.DeserializeObject<Message>(responseString);
                MessageBox.Show(successCake.message, "Siker :)");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            return true;
        }
        public async Task<bool> BuyCakes()
        {
            string url = baseURL + "/buyCakes";

            try
            {
                var jsonData = new
                {
                    cakes = Cart.cart.Select(cake => new { id = cake.id, count = cake.orderCount })
                };
                string jsonString = JsonConvert.SerializeObject(jsonData);
                StringContent sendThis = new StringContent(jsonString, Encoding.UTF8, "Application/JSON");
                HttpResponseMessage response = await client.PostAsync(url, sendThis);
                response.EnsureSuccessStatusCode();
                string responseString = await response.Content.ReadAsStringAsync();
                Message successCake = JsonConvert.DeserializeObject<Message>(responseString);
                MessageBox.Show(successCake.message, "Siker :)");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            return true;
        }
        public async Task<bool> RestockCake(Cake oneCake)
        {
            string url = baseURL + "/restockCake";

            try
            {
                var jsonData = new
                {
                    id = oneCake.id,
                    count = oneCake.orderCount
                };
                string jsonString = JsonConvert.SerializeObject(jsonData);
                StringContent sendThis = new StringContent(jsonString, Encoding.UTF8, "Application/JSON");
                HttpResponseMessage response = await client.PostAsync(url, sendThis);
                response.EnsureSuccessStatusCode();
                string responseString = await response.Content.ReadAsStringAsync();
                Message successCake = JsonConvert.DeserializeObject<Message>(responseString);
                MessageBox.Show(successCake.message, "Siker :)");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            return true;
        }
        public async Task<bool> RestockCakes()
        {
            string url = baseURL + "/restockCakes";

            try
            {
                var jsonData = new
                {
                    cakes = Cart.delivery.Select(cake => new { id = cake.id, count = cake.orderCount })
                };
                string jsonString = JsonConvert.SerializeObject(jsonData);
                StringContent sendThis = new StringContent(jsonString, Encoding.UTF8, "Application/JSON");
                HttpResponseMessage response = await client.PostAsync(url, sendThis);
                response.EnsureSuccessStatusCode();
                string responseString = await response.Content.ReadAsStringAsync();
                Message successCake = JsonConvert.DeserializeObject<Message>(responseString);
                MessageBox.Show(successCake.message, "Siker :)");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

            return true;
        }
    }
}
