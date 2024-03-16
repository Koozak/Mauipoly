namespace Mauipoly;

public partial class PlayPage : ContentPage
{
	public PlayPage()
	{
		InitializeComponent();
	}
    private void btnBackToPlayPage_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}