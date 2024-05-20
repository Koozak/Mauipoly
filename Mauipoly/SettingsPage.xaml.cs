using MauiPageFullScreen;

namespace Mauipoly
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }
        private void btnFullscreen_Clicked(object sender, EventArgs e)
        {
            Controls.ToggleFullScreenStatus();
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnHowToPlay_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new InfoPage());
        }

        private async void btnExit_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Game Exit", "Are you sure you wanna exit game ", "Yes", "No");
            if (answer == true)
            {
                Application.Current.Quit();
            }
        }
    }
}
