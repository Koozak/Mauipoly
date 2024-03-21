namespace Mauipoly;

public partial class PlayPage : ContentPage
{
	public PlayPage()
	{
		InitializeComponent();
		Player player1 = new Player(1, "Test", true);
		LabeLWithPlayerName.Text = player1.Nickname+" Turn";
    }
}