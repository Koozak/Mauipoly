using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace Mauipoly;

public partial class InfoPage : ContentPage
{
	public InfoPage()
	{
		InitializeComponent();
    }

    private void btnBack_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private void btnHowToPlayWithPlayer_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new InfoPlayPlayer());
    }

    private void btnHowToPlayWithBot_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new InfoPlayBot());
    }
}