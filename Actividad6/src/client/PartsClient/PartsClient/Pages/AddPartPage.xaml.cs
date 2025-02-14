using Microsoft.Maui.Controls;
using PartsClient.Data;
using System;
using System.Threading.Tasks;

namespace PartsClient.Pages
{
    public partial class AddPartPage : ContentPage
    {
        Part currentPart;

        public AddPartPage(Part part = null)
        {
            InitializeComponent();
            if (part != null)
            {
                currentPart = part;
                partNameEntry.Text = part.PartName;
                supplierEntry.Text = part.SupplierString;
                partTypePicker.SelectedItem = part.PartType;
                BindingContext = new { IsEditMode = true };
            }
            else
            {
                BindingContext = new { IsEditMode = false };
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                var partName = partNameEntry.Text;
                var supplier = supplierEntry.Text;
                var partType = partTypePicker.SelectedItem?.ToString();

                if (currentPart != null)
                {
                    currentPart.PartName = partName;
                    currentPart.Suppliers = new List<string> { supplier };
                    currentPart.PartType = partType;

                    await PartsManager.Update(currentPart);
                }
                else
                {
                    var newPart = await PartsManager.Add(partName, supplier, partType);
                }

                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to save part: {ex.Message}", "OK");
            }
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            try
            {
                if (currentPart != null)
                {
                    await PartsManager.Delete(currentPart.PartID);
                    await Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to delete part: {ex.Message}", "OK");
            }
        }
    }
}