namespace Mauipoly;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private void btnPlay_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new ChoosePage());
    }

    private void btnExit_Clicked(object sender, EventArgs e)
    {
        
    }

    private void btnInfo_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new InfoPage());
    }
}