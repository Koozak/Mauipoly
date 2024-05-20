using CommunityToolkit.Maui.Views;
using MauiPageFullScreen;

namespace Mauipoly;

public partial class HomePage : ContentPage
{
    public HomePage()
	{
		InitializeComponent();
        Music.IsVisible = false;
        Music.Volume = 0.2;
        
    }
    private void btnPlay_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ChoosePage());
        Music.Play();
    }
    private async void btnExit_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Game Exit", "Are you sure you wanna exit game ", "Yes", "No");
        if (answer == true)
        {
            Application.Current.Quit();
        }
    }
    private void btnSettings_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage());
    }
}