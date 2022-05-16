using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TareaSemana6
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        private const string url = "http://192.168.0.106:1987/api/user";
        private readonly HttpClient client = new HttpClient();
        private Models.User user;
        public UserPage(Models.User user)
        {
            InitializeComponent();
            this.user = user;
            txtUsername.Text = user.username;
            txtPassword.Text = user.password;
        }

        private async void btnPut_Clicked(object sender, EventArgs e)
        {
            Models.User data = new Models.User
            {
                usuarioId = user.usuarioId,
                username = txtUsername.Text,
                password = txtPassword.Text,
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(data);
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PutAsync(String.Format(url + "/{0}", user.usuarioId), httpContent);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                await DisplayAlert("Exito", "Datos actualizado con éxito", "ok");

            }

        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            Models.User data = new Models.User
            {
                usuarioId = user.usuarioId,
                username = txtUsername.Text,
                password = txtPassword.Text,
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(data);
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.DeleteAsync(String.Format(url + "/{0}", user.usuarioId));

            if (response.StatusCode == HttpStatusCode.OK)
            {
                await DisplayAlert("Exito", "Datos eliminado con éxito", "ok");

            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                await DisplayAlert("Error", "El usuario no existe", "ok");
            }

        }
    }
}