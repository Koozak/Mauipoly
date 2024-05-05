namespace Mauipoly;

public partial class HomePage : ContentPage
{
    public HomePage()
	{
		InitializeComponent();
        Music.IsVisible = false;
        Music.Volume = 0.25;

    }
    private void btnPlay_Clicked(object sender, EventArgs e)
    {
        Music.Play();
        Navigation.PushAsync(new ChoosePage());
    }
    private void btnExit_Clicked(object sender, EventArgs e)
    {
        Music.Pause();
        Application.Current.Quit();

    }

    private void btnInfo_Clicked(object sender, EventArgs e)
    {
        Music.Play();
        Navigation.PushAsync(new InfoPage());
    }
}