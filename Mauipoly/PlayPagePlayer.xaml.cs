using Label = Microsoft.Maui.Controls.Label;

namespace Mauipoly;

public partial class PlayPagePlayer : ContentPage
{
    Random rnd = new Random();
    Random rnd1 = new Random();
    Random rnd2 = new Random();
    Player player1 = new Player("Fast_Tomek", false, "player1img.png");
    Player player2 = new Player("Fast_Romek", false, "player2img.png");
    public PlayPagePlayer()
	{
		InitializeComponent();
        Whose_Turn(player1, player2);
        board.Add(new Image() {Source = player1.Image }, 0, 6);
        board.Add(new Image() { Source = player2.Image }, 0, 6);

    }
    private void Whose_Turn(Player one,Player two)
    {
        int value = rnd.Next(1, 3);
        if (value == 1)
        {
            one.isTurn = true;
            LabeLWithPlayerName.Text = one.Nickname + " Turn";
            PlayerImage.Source = one.Image;
            PlayerValues.Text = "Your Money is "+one.Money.ToString();
        }
        else
        {
            two.isTurn = true;
            LabeLWithPlayerName.Text = two.Nickname + " Turn";
            PlayerImage.Source = two.Image;
            PlayerValues.Text = "Your Money is " + two.Money.ToString();
        }
    }

    private async void Btn_Throw_Clicked(object sender, EventArgs e)
    {
        if(player1.isTurn == true)
        {

            int value = rnd1.Next(1, 7)+rnd2.Next(1,7);
            ThrowBtn.Opacity = 0.5;
            ThrowBtn.IsEnabled = false;
            await MovingTomek();
            ThrowBtn.Opacity = 1;
            ThrowBtn.IsEnabled = true;
            player1.isTurn = false;
            player2.isTurn = true;            
            LabeLWithPlayerName.Text = player2.Nickname + " Turn";
            PlayerImage.Source = player2.Image;
            PlayerValues.Text = "Your Money is " + player2.Money.ToString();
        }
        else
        {
            int value = rnd1.Next(1, 7) + rnd2.Next(1, 7);
            ThrowBtn.Opacity = 0.5;
            ThrowBtn.IsEnabled = false;
            await MovingRomek();
            ThrowBtn.Opacity = 1;
            ThrowBtn.IsEnabled = true;
            player2.isTurn = false;
            player1.isTurn = true;
            LabeLWithPlayerName.Text = player1.Nickname + " Turn";
            PlayerImage.Source = player1.Image;
            PlayerValues.Text = "Your Money is " + player1.Money.ToString();
        }
    }
    private async Task Moving()
    {
        int a = 6;
        int w = 0;
        int d = 0;
        int s = 6;
        for(int i = 0; i < 6; i++)
        {
            if(a!=0)
            {
                            
                //board.Remove(Image() { Source = player1.Image }, 0, a);
                a--;
                await Task.Delay(500);
                board.Add(new Image() { Source = player1.Image }, 0, a);
            }

           
        }
    }
}