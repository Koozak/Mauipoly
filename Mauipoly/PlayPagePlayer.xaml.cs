using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Mauipoly;

public partial class PlayPagePlayer : ContentPage
{
    Random rnd = new Random();
    Random rnd1 = new Random();
    Random rnd2 = new Random();
    public int xdplayer1 = 0;
    public int xdplayer2 = 0;
    private BoardField[] fields = new BoardField[24];
    Player player1 = new Player("Fast_Tomek", false, "player1img.png");
    Player player2 = new Player("Fast_Romek", false, "player2img.png");

    public PlayPagePlayer()
	{
		InitializeComponent();
        Whose_Turn(player1, player2);
        SetFields();

    }


    private void Whose_Turn(Player one,Player two)
    {
        int value = rnd.Next(1, 3);
        if (value == 1)
        {
            one.isTurn = true;
            ShowingStats(one);
            field1.Source = player1.Image;
        }
        else
        {
            two.isTurn = true;
            ShowingStats(two);
            field1.Source= player2.Image;
        }
    }

    private async void Btn_Throw_Clicked(object sender, EventArgs e)
    {
        if(player1.isTurn == true)
        {
            int value = rnd1.Next(1, 5)+rnd2.Next(1,5);
            ThrowBtn.Opacity = 0.5;
            ThrowBtn.IsEnabled = false;
            Moving(value);
            ThrowBtn.Opacity = 1;
            ThrowBtn.IsEnabled = true;
            player1.isTurn = false;
            player2.isTurn = true;
            test(fields,player1,xdplayer1);
            ShowingStats(player1);

        }
        else
        {
            int value = rnd1.Next(1, 5) + rnd2.Next(1, 5);
            ThrowBtn.Opacity = 0.5;
            ThrowBtn.IsEnabled = false;
            Moving(value);
            ThrowBtn.Opacity = 1;
            ThrowBtn.IsEnabled = true;
            player2.isTurn = false;
            player1.isTurn = true;
            test(fields, player2,xdplayer2);
            ShowingStats(player2);
            
        }
    }
    private async void test(BoardField[] field, Player player, int id)
    {
        if (id >= 23)
        {
            id = 0;
        }
        if (field[id].Occupied == false)
        {
            if (field[id].HowMuchForField != 1000000000)
            {
                bool answer = await DisplayAlert("Question?", "Would you like to buy this field. The cost of this file is " + field[id].HowMuchForField.ToString(), "Yes", "No");
                if (answer == true)
                {
                    if ((player.Money - field[id].HowMuchForField) < 0)
                    {
                        await DisplayAlert("Alert!", "You dont have enough", "OK");
                    }
                    else
                    {
                        player.Money = player.Money - field[id].HowMuchForField;
                        ShowingStats(player);
                        int help = 0;
                        if (player.BoardFieldList[help] == field[id])
                        {
                            player.BoardFieldList[help] = field[id];
                            
                        }
                        else
                        {
                            help++;
                            player.BoardFieldList[help] = field[id];
                        }
                    }
                }
            }
            else
            {

            }

        }
        else
        {
            await DisplayAlert("Alert!", "It is occupied", "OK");
        }
    }
    private void ShowingStats(Player player)
    {
        LabeLWithPlayerName.Text = player.Nickname + " Turn";
        PlayerImage.Source = player.Image;
        PlayerValues.Text = "Your Money is " + player.Money.ToString();

    }
    private void Moving(int r)
    {
        if (player1.isTurn == true)
        {

            xdplayer1 = xdplayer1 + r;
            if (xdplayer1 >= 23)
            {
                int help1 = 23 - xdplayer1;
                xdplayer1 = 0 + help1;
                player1.Money = player1.Money + 200;
                xdplayer1 = xdplayer1 + r;
                switch (fields[xdplayer1].Name)
                {
                    case "field1":
                        field1.Source = player1.Image;
                        break;

                    case "field2":
                        field2.Source = player1.Image;
                        break;

                    case "field3":
                        field3.Source = player1.Image;
                        break;
                    case "field4":
                        field4.Source = player1.Image;
                        break;
                    case "field5":
                        field5.Source = player1.Image;
                        break;
                    case "field6":
                        field6.Source = player1.Image;
                        break;
                    case "field7":
                        field7.Source = player1.Image;
                        break;
                    case "field8":
                        field8.Source = player1.Image;
                        break;
                    case "field9":
                        field9.Source = player1.Image;
                        break;
                    case "field10":
                        field10.Source = player1.Image;
                        break;
                    case "field11":
                        field11.Source = player1.Image;
                        break;
                    case "field12":
                        field12.Source = player1.Image;
                        break;
                    case "field13":
                        field13.Source = player1.Image;
                        break;
                    case "field14":
                        field14.Source = player1.Image;
                        break;
                    case "field15":
                        field15.Source = player1.Image;
                        break;
                    case "field16":
                        field16.Source = player1.Image;
                        break;
                    case "field17":
                        field17.Source = player1.Image;
                        break;
                    case "field18":
                        field18.Source = player1.Image;
                        break;
                    case "field19":
                        field19.Source = player1.Image;
                        break;
                    case "field20":
                        field20.Source = player1.Image;
                        break;
                    case "field21":
                        field21.Source = player1.Image;
                        break;
                    case "field22":
                        field22.Source = player1.Image;
                        break;
                    case "field23":
                        field23.Source = player1.Image;
                        break;
                }
            }
            else
            {

                switch (fields[xdplayer1].Name)
                {
                    case "field1":
                        field1.Source = player1.Image;
                        break;

                    case "field2":
                        field2.Source = player1.Image;
                        break;

                    case "field3":
                        field3.Source = player1.Image;
                        break;
                    case "field4":
                        field4.Source = player1.Image;
                        break;
                    case "field5":
                        field5.Source = player1.Image;
                        break;
                    case "field6":
                        field6.Source = player1.Image;
                        break;
                    case "field7":
                        field7.Source = player1.Image;
                        break;
                    case "field8":
                        field8.Source = player1.Image;
                        break;
                    case "field9":
                        field9.Source = player1.Image;
                        break;
                    case "field10":
                        field10.Source = player1.Image;
                        break;
                    case "field11":
                        field11.Source = player1.Image;
                        break;
                    case "field12":
                        field12.Source = player1.Image;
                        break;
                    case "field13":
                        field13.Source = player1.Image;
                        break;
                    case "field14":
                        field14.Source = player1.Image;
                        break;
                    case "field15":
                        field15.Source = player1.Image;
                        break;
                    case "field16":
                        field16.Source = player1.Image;
                        break;
                    case "field17":
                        field17.Source = player1.Image;
                        break;
                    case "field18":
                        field18.Source = player1.Image;
                        break;
                    case "field19":
                        field19.Source = player1.Image;
                        break;
                    case "field20":
                        field20.Source = player1.Image;
                        break;
                    case "field21":
                        field21.Source = player1.Image;
                        break;
                    case "field22":
                        field22.Source = player1.Image;
                        break;
                    case "field23":
                        field23.Source = player1.Image;
                        break;
                }
            }
        }
        else
        {
            xdplayer2 = xdplayer2 + r;
            if (xdplayer2 >= 23)
            {
                int help1 = 23 - xdplayer2;
                xdplayer2 = 0 + help1;
                player2.Money = player2.Money + 200;
                xdplayer2 = xdplayer2 + r;
                switch (fields[xdplayer2].Name)
                {
                    case "field1":
                        field1.Source = player2.Image;
                        break;

                    case "field2":
                        field2.Source = player2.Image;
                        break;

                    case "field3":
                        field3.Source = player2.Image;
                        break;
                    case "field4":
                        field4.Source = player2.Image;
                        break;
                    case "field5":
                        field5.Source = player2.Image;
                        break;
                    case "field6":
                        field6.Source = player2.Image;
                        break;
                    case "field7":
                        field7.Source = player2.Image;
                        break;
                    case "field8":
                        field8.Source = player2.Image;
                        break;
                    case "field9":
                        field9.Source = player2.Image;
                        break;
                    case "field10":
                        field10.Source = player2.Image;
                        break;
                    case "field11":
                        field11.Source = player2.Image;
                        break;
                    case "field12":
                        field12.Source = player2.Image;
                        break;
                    case "field13":
                        field13.Source = player2.Image;
                        break;
                    case "field14":
                        field14.Source = player2.Image;
                        break;
                    case "field15":
                        field15.Source = player2.Image;
                        break;
                    case "field16":
                        field16.Source = player2.Image;
                        break;
                    case "field17":
                        field17.Source = player2.Image;
                        break;
                    case "field18":
                        field18.Source = player2.Image;
                        break;
                    case "field19":
                        field19.Source = player2.Image;
                        break;
                    case "field20":
                        field20.Source = player2.Image;
                        break;
                    case "field21":
                        field21.Source = player2.Image;
                        break;
                    case "field22":
                        field22.Source = player2.Image;
                        break;
                    case "field23":
                        field23.Source = player2.Image;
                        break;
                }
            }
            else
            {

                switch (fields[xdplayer2].Name)
                {
                    case "field1":
                        field1.Source = player2.Image;
                        break;

                    case "field2":
                        field2.Source = player2.Image;
                        break;

                    case "field3":
                        field3.Source = player2.Image;
                        break;
                    case "field4":
                        field4.Source = player2.Image;
                        break;
                    case "field5":
                        field5.Source = player2.Image;
                        break;
                    case "field6":
                        field6.Source = player2.Image;
                        break;
                    case "field7":
                        field7.Source = player2.Image;
                        break;
                    case "field8":
                        field8.Source = player2.Image;
                        break;
                    case "field9":
                        field9.Source = player2.Image;
                        break;
                    case "field10":
                        field10.Source = player2.Image;
                        break;
                    case "field11":
                        field11.Source = player2.Image;
                        break;
                    case "field12":
                        field12.Source = player2.Image;
                        break;
                    case "field13":
                        field13.Source = player2.Image;
                        break;
                    case "field14":
                        field14.Source = player2.Image;
                        break;
                    case "field15":
                        field15.Source = player2.Image;
                        break;
                    case "field16":
                        field16.Source = player2.Image;
                        break;
                    case "field17":
                        field17.Source = player2.Image;
                        break;
                    case "field18":
                        field18.Source = player2.Image;
                        break;
                    case "field19":
                        field19.Source = player2.Image;
                        break;
                    case "field20":
                        field20.Source = player2.Image;
                        break;
                    case "field21":
                        field21.Source = player2.Image;
                        break;
                    case "field22":
                        field22.Source = player2.Image;
                        break;
                    case "field23":
                        field23.Source = player2.Image;
                        break;
                }
            }
        }
    }
    private void SetFields()
    {
        BoardField field1 = new BoardField("field1", 1000000000, 6, 0);
        BoardField field2 = new BoardField("field2", 50, 5, 0);
        BoardField field3 = new BoardField("field3", 50, 4, 0);
        BoardField field4 = new BoardField("field4", 150, 3, 0);
        BoardField field5 = new BoardField("field5", 50, 2, 0);
        BoardField field6 = new BoardField("field6", 50, 1, 0);
        BoardField field7 = new BoardField("field7", 1000000000, 0, 0);
        BoardField field8 = new BoardField("field8", 150, 0, 1);
        BoardField field9 = new BoardField("field9", 150, 0, 2);
        BoardField field10 = new BoardField("field10", 300, 0, 3);
        BoardField field11 = new BoardField("field11", 150, 0, 4);
        BoardField field12 = new BoardField("field12", 150, 0, 5);
        BoardField field13 = new BoardField("field13", 1000000000, 0, 6);
        BoardField field14 = new BoardField("field14", 300, 1, 6);
        BoardField field15 = new BoardField("field15", 300, 2, 6);
        BoardField field16 = new BoardField("field16", 500, 3, 6);
        BoardField field17 = new BoardField("field17", 300, 4, 6);
        BoardField field18 = new BoardField("field18", 300, 5, 6);
        BoardField field19 = new BoardField("field19", 1000000000, 6, 6);
        BoardField field20 = new BoardField("field20", 500, 6, 5);
        BoardField field21 = new BoardField("field21", 500, 6, 4);
        BoardField field22 = new BoardField("field22", 600, 6, 3);
        BoardField field23 = new BoardField("field23", 500, 6, 2);
        BoardField field24 = new BoardField("field24", 500, 6, 1);
        fields[0] = field1;
        fields[1] = field2;
        fields[2] = field3;
        fields[3] = field4;
        fields[4] = field5;
        fields[5] = field6;
        fields[6] = field7;
        fields[7] = field8;
        fields[8] = field9;
        fields[9] = field10;
        fields[10] = field11;
        fields[11] = field12;
        fields[12] = field13;
        fields[13] = field14;
        fields[14] = field15;
        fields[15] = field16;
        fields[16] = field17;
        fields[17] = field18;
        fields[18] = field19;
        fields[19] = field20;
        fields[20] = field21;
        fields[21] = field22;
        fields[22] = field23;
        fields[23] = field24;
    }
}