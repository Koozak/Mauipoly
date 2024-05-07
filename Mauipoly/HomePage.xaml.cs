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
    private void btnExit_Clicked(object sender, EventArgs e)
    {
        Application.Current.Quit();
    }
    private void btnSettings_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage());
    }
}