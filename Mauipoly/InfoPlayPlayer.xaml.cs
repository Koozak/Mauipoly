namespace Mauipoly;

public partial class InfoPlayPlayer : ContentPage
{
	public InfoPlayPlayer()
	{
		InitializeComponent();
	}

    private void btnButton_Clicked(object sender, EventArgs e)
    {
		Navigation.PopAsync();
    }
}