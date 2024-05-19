namespace Mauipoly;

public partial class InfoPlayBot : ContentPage
{
	public InfoPlayBot()
	{
		InitializeComponent();
	}
    private void btnButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}