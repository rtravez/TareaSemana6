using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Net.Http;

using Xamarin.Forms;

using Newtonsoft.Json;

using System.ComponentModel;
using System.Linq;
using System.Net;

using System.Text;
using System.Threading.Tasks;


namespace TareaSemana6
{
    public partial class MainPage : ContentPage
    {
        private const string url = "http://192.168.0.106:1987/api/users";
        private readonly HttpClient client = new HttpClient();
        private ObservableCollection<TareaSemana6.Models.User> _post;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void BtnGet_Clicked(object sender, EventArgs e)
        {
            this.ObtenerDatos();
        }

        private async void BtnPost_Clicked(object sender, EventArgs e)
        {
            Models.User data = new Models.User
            {
                username = txtUsername.Text,
                password = txtPassword.Text,
            };

            Uri RequestUri = new Uri(url);
            var json = JsonConvert.SerializeObject(data);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(RequestUri, contentJson);

            string jsonString = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Created)
            {
                await DisplayAlert("Exito", "Datos ingresado con éxito", "ok");
                ObtenerDatos();
            }

        }

        private async void ObtenerDatos()
        {
            var content = await client.GetStringAsync(url);
            List<TareaSemana6.Models.User> posts = JsonConvert.DeserializeObject<List<TareaSemana6.Models.User>>(content);
            _post = new ObservableCollection<TareaSemana6.Models.User>(posts);
            MyListView.ItemsSource = _post;

        }

        private async void MyListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {           
            await Navigation.PushAsync(new UserPage((Models.User)e.SelectedItem));

        }
    }
}
