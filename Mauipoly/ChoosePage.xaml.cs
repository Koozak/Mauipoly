namespace Mauipoly;

public partial class ChoosePage : ContentPage
{
	public ChoosePage()
	{
		InitializeComponent();
	}
    private void btnPlayWithPlayer_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PlayPage());
    }
    private void btnPlayWithBot_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PlayPage());
    }
}