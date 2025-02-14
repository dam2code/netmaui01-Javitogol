using Microsoft.Maui.Controls;
using PartsClient.Data;
using System;
using System.Threading.Tasks;

namespace PartsClient.Pages
{
    public partial class PartsPage : ContentPage
    {
        public PartsPage()
        {
            InitializeComponent();
            LoadParts();
        }

        private async void LoadParts()
        {
            try
            {
                var parts = await PartsManager.GetAll();
                BindingContext = new { Parts = parts };
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load parts: {ex.Message}", "OK");
            }
        }

        private async void OnAddPartClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddPartPage());
        }
    }
}