using MauiPageFullScreen;

namespace Mauipoly;

public partial class PlayPageBot : ContentPage
{
	public PlayPageBot()
	{
		InitializeComponent();
	}

    private void Btn_Throw_Clicked(object sender, EventArgs e)
    {

    }

    private void Btn_EndTurn_Clicked(object sender, EventArgs e)
    {

    }

    private void Btn_BuyField_Clicked(object sender, EventArgs e)
    {

    }

    private void Btn_Fullscreen_Clicked(object sender, EventArgs e)
    {
        Controls.ToggleFullScreenStatus();
    }
}