using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace Mauipoly;

public partial class InfoPage : ContentPage
{
	public InfoPage()
	{
		InitializeComponent();
    }

    private void btnExit_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}