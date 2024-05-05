using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Mauipoly;

public partial class PlayPagePlayer : ContentPage
{
    int global = 0;
    Random rnd = new Random();
    Random rnd1 = new Random();
    Random rnd2 = new Random();
    public int xdplayer1 = 0;
    public int xdplayer2 = 0;
    private BoardField[] fields = new BoardField[24];
    Player player1 = new Player("Player1", false, "player1img.png");
    Player player2 = new Player("Player2", false, "player2img.png");

    public PlayPagePlayer()
    {
        InitializeComponent();
        Whose_Turn(player1, player2);
        SetFields();

    }


    private void Whose_Turn(Player one, Player two)
    {
        int value = rnd.Next(1, 3);
        if (value == 1)
        {
            one.isTurn = true;
            ShowingStats(one);
        }
        else
        {
            two.isTurn = true;
            ShowingStats(two);
        }
    }

    private void Btn_Throw_Clicked(object sender, EventArgs e)
    {
        if (player1.isTurn == true)
        {
            int value = rnd1.Next(1, 5) + rnd2.Next(1, 5);
            ThrowBtn.Opacity = 0.5;
            ThrowBtn.IsEnabled = false;
            Moving(value);
            ShowingStats(player1);
            foreach (var item in player2.BoardFieldList)
            {
                if (item != null && item.Name == fields[xdplayer1].Name)
                {
                    DisplayAlert("Alert!", "You step on enemy field that will by cost " + fields[xdplayer1].HowMuchForField, "OK");
                    if (player1.Money - fields[xdplayer1].HowMuchForField <= 0)
                    {
                        DisplayAlert("End Game", "Player 2 won", "OK");
                        ThrowBtn.Opacity = 0.5;
                        ThrowBtn.IsEnabled = false;
                        EndTurn.Opacity = 0.5;
                        EndTurn.IsEnabled = false;
                        BuyField.Opacity = 0.5;
                        BuyField.IsEnabled = false;
                    }
                    else
                    {
                        player1.Money -= fields[xdplayer1].HowMuchForField;
                        player2.Money += fields[xdplayer1].HowMuchForField;
                    }

                    break;
                }
                else
                {

                }
            }
        }
        else
        {
            int value = rnd1.Next(1, 5) + rnd2.Next(1, 5);
            ThrowBtn.Opacity = 0.5;
            ThrowBtn.IsEnabled = false;
            Moving(value);
            ShowingStats(player2);
            foreach (var item in player1.BoardFieldList)
            {
                if (item != null && item.Name == fields[xdplayer2].Name)
                {
                    DisplayAlert("Alert!", "You step on enemy field that will by cost " + fields[xdplayer2].HowMuchForField, "OK");
                    if(player2.Money-fields[xdplayer2].HowMuchForField <= 0) 
                    {
                        DisplayAlert("End Game", "Player 1 won", "OK");
                        ThrowBtn.Opacity = 0.5;
                        ThrowBtn.IsEnabled = false;
                        EndTurn.Opacity = 0.5;
                        EndTurn.IsEnabled = false;
                        BuyField.Opacity = 0.5;
                        BuyField.IsEnabled = false;
                    }
                    else
                    {
                        player2.Money -= fields[xdplayer2].HowMuchForField;
                        player1.Money += fields[xdplayer2].HowMuchForField;
                    }
                    break;
                }
                else
                {

                }
            }
        }
    }
    private void Btn_EndTurn_Clicked(object sender, EventArgs e)
    {
        if (player1.isTurn == true)
        {
            ThrowBtn.Opacity = 1;
            ThrowBtn.IsEnabled = true;
            player1.isTurn = false;
            player2.isTurn = true;
            ShowingStats(player2);
            BuyField.Opacity = 1;
            BuyField.IsEnabled = true;
        }
        else
        {
            ThrowBtn.Opacity = 1;
            ThrowBtn.IsEnabled = true;
            player2.isTurn = false;
            player1.isTurn = true;
            ShowingStats(player1);
            BuyField.Opacity = 1;
            BuyField.IsEnabled = true;
        }
    }
    private void Btn_BuyField_Clicked(object sender, EventArgs e)
    {
        if (player1.isTurn == true)
        {
                buyingFields(fields, player1, xdplayer1);

        } 
        else 
        {
                buyingFields(fields, player2, xdplayer2);

        }
    }
    private async void buyingFields(BoardField[] field, Player player, int id)
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
                    if ((player.Money - field[id].HowMuchForField) <= 0)
                    {
                        await DisplayAlert("Alert!", "You dont have enough", "OK");
                    }
                    else
                    {
                        player.Money = player.Money - field[id].HowMuchForField;
                        field[id].Occupied = true;
                        player.BoardFieldList[global] = field[id];
                        global++;
                        ShowingStats(player);
                    }
                }
            }
            else
            {

            }

        }
        else
        {
            await DisplayAlert("Alert!", "It is occupied by "+player.Nickname, "OK");
        }
    }
    private void ShowingStats(Player player)
    {
        LabeLWithPlayerName.Text = player.Nickname + " Turn";
        PlayerImage.Source = player.Image;
        PlayerValues.Text = "Your Money is " + player.Money.ToString()+"     ";
        foreach (var item in player.BoardFieldList)
        {
            if (item == null)
            {

            }
            else
            {
                PlayerValues.Text += item.Name.ToString()+",";
            }
            
        }

    }
    private void Moving(int r)
    {
        if (player1.isTurn == true)
        {
            switch (fields[xdplayer1].Name)
            {
                case "field1":
                    if (field1.Source!=null && field1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field1.Source = player2.Image;
                    }
                    else
                    {
                        field1.Source = null;
                    }
                    break;

                case "field2":
                    if (field2.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field2.Source = player2.Image;
                    }
                    else
                    {
                        field2.Source = null;
                    }
                    break;

                case "field3":
                    if (field3.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field3.Source = player2.Image;
                    }
                    else
                    {
                        field3.Source = null;
                    }
                    break;
                case "field4":
                    if (field4.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field4.Source = player2.Image;
                    }
                    else
                    {
                        field4.Source = null;
                    }
                    break;
                case "field5":
                    if (field5.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field5.Source = player2.Image;
                    }
                    else
                    {
                        field5.Source = null;
                    }
                    break;
                case "field6":
                    if (field6.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field6.Source = player2.Image;
                    }
                    else
                    {
                        field6.Source = null;
                    }
                    break;
                case "field7":
                    if (field7.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field7.Source = player2.Image;
                    }
                    else
                    {
                        field7.Source = null;
                    }
                    break;
                case "field8":
                    if (field8.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field8.Source = player2.Image;
                    }
                    else
                    {
                        field8.Source = null;
                    }
                    break;
                case "field9":
                    if (field9.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field9.Source = player2.Image;
                    }
                    else
                    {
                        field9.Source = null;
                    }
                    break;
                case "field10":
                    if (field10.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field10.Source = player2.Image;
                    }
                    else
                    {
                        field10.Source = null;
                    }
                    break;
                case "field11":
                    if (field11.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field11.Source = player2.Image;
                    }
                    else
                    {
                        field11.Source = null;
                    }
                    break;
                case "field12":
                    if (field12.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field12.Source = player2.Image;
                    }
                    else
                    {
                        field12.Source = null;
                    }
                    break;
                case "field13":
                    if (field13.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field13.Source = player2.Image;
                    }
                    else
                    {
                        field13.Source = null;
                    }
                    break;
                case "field14":
                    if (field14.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field14.Source = player2.Image;
                    }
                    else
                    {
                        field14.Source = null;
                    }
                    break;
                case "field15":
                    if (field15.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field15.Source = player2.Image;
                    }
                    else
                    {
                        field15.Source = null;
                    }
                    break;
                case "field16":
                    if (field16.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field16.Source = player2.Image;
                    }
                    else
                    {
                        field16.Source = null;
                    }
                    break;
                case "field17":
                    if (field17.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field17.Source = player2.Image;
                    }
                    else
                    {
                        field17.Source = null;
                    }
                    break;
                case "field18":
                    if (field18.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field18.Source = player2.Image;
                    }
                    else
                    {
                        field18.Source = null;
                    }
                    break;
                case "field19":
                    if (field19.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field19.Source = player2.Image;
                    }
                    else
                    {
                        field19.Source = null;
                    }
                    break;
                case "field20":
                    if (field20.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field20.Source = player2.Image;
                    }
                    else
                    {
                        field20.Source = null;
                    }
                    break;
                case "field21":
                    if (field21.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field21.Source = player2.Image;
                    }
                    else
                    {
                        field21.Source = null;
                    }
                    break;
                case "field22":
                    if (field22.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field22.Source = player2.Image;
                    }
                    else
                    {
                        field22.Source = null;
                    }
                    break;
                case "field23":
                    if (field23.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field23.Source = player2.Image;
                    }
                    else
                    {
                        field23.Source = null;
                    }
                    break;
            }
            xdplayer1 = xdplayer1 + r;
            if (xdplayer1 >= 23)
            {
                int help1 = xdplayer1 - 23;
                xdplayer1 = 0 + help1;
                player1.Money = player1.Money + 200;
            }
            switch (fields[xdplayer1].Name)
            {
                case "field1":
                    if (field1.Source == null)
                    {
                        field1.Source = player1.Image;
                    }
                    else if (field1.Source != null)
                    {
                        field1.Source = "player1andplayer2img.png";
                    }
                    break;

                case "field2":
                    if (field2.Source == null)
                    {
                        field2.Source = player1.Image;
                    }
                    else if (field2.Source != null)
                    {
                        field2.Source = "player1andplayer2img.png";
                    }
                    break;

                case "field3":
                    if (field3.Source == null)
                    {
                        field3.Source = player1.Image;
                    }
                    else if (field3.Source != null)
                    {
                        field3.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field4":
                    if (field4.Source == null)
                    {
                        field4.Source = player1.Image;
                    }
                    else if (field4.Source != null)
                    {
                        field4.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field5":
                    if (field5.Source == null)
                    {
                        field5.Source = player1.Image;
                    }
                    else if (field5.Source != null)
                    {
                        field5.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field6":
                    if (field6.Source == null)
                    {
                        field6.Source = player1.Image;
                    }
                    else if (field6.Source != null)
                    {
                        field6.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field7":
                    if (field7.Source == null)
                    {
                        field7.Source = player1.Image;
                    }
                    else if (field7.Source != null)
                    {
                        field7.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field8":
                    if (field8.Source == null)
                    {
                        field8.Source = player1.Image;
                    }
                    else if (field8.Source != null)
                    {
                        field8.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field9":
                    if (field9.Source == null)
                    {
                        field9.Source = player1.Image;
                    }
                    else if (field9.Source != null)
                    {
                        field9.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field10":
                    if (field10.Source == null)
                    {
                        field10.Source = player1.Image;
                    }
                    else if (field10.Source != null)
                    {
                        field10.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field11":
                    if (field11.Source == null)
                    {
                        field11.Source = player1.Image;
                    }
                    else if (field11.Source != null)
                    {
                        field11.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field12":
                    if (field12.Source == null)
                    {
                        field12.Source = player1.Image;
                    }
                    else if (field12.Source != null)
                    {
                        field12.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field13":
                    if (field13.Source == null)
                    {
                        field13.Source = player1.Image;
                    }
                    else if (field13.Source != null)
                    {
                        field13.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field14":
                    if (field14.Source == null)
                    {
                        field14.Source = player1.Image;
                    }
                    else if (field14.Source != null)
                    {
                        field14.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field15":
                    if (field15.Source == null)
                    {
                        field15.Source = player1.Image;
                    }
                    else if (field15.Source != null)
                    {
                        field15.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field16":
                    if (field16.Source == null)
                    {
                        field16.Source = player1.Image;
                    }
                    else if (field16.Source != null)
                    {
                        field16.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field17":
                    if (field17.Source == null)
                    {
                        field17.Source = player1.Image;
                    }
                    else if (field17.Source != null)
                    {
                        field17.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field18":
                    if (field18.Source == null)
                    {
                        field18.Source = player1.Image;
                    }
                    else if (field18.Source != null)
                    {
                        field18.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field19":
                    if (field19.Source == null)
                    {
                        field19.Source = player1.Image;
                    }
                    else if (field19.Source != null)
                    {
                        field19.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field20":
                    if (field20.Source == null)
                    {
                        field20.Source = player1.Image;
                    }
                    else if (field20.Source != null)
                    {
                        field20.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field21":
                    if (field21.Source == null)
                    {
                        field21.Source = player1.Image;
                    }
                    else if (field21.Source != null)
                    {
                        field21.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field22":
                    if (field22.Source == null)
                    {
                        field22.Source = player1.Image;
                    }
                    else if (field22.Source != null)
                    {
                        field22.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field23":
                    if (field23.Source == null)
                    {
                        field23.Source = player1.Image;
                    }
                    else if (field23.Source != null)
                    {
                        field23.Source = "player1andplayer2img.png";
                    }
                    break;
            }
        }
        else
        {
            switch (fields[xdplayer2].Name)
            {
                case "field1":
                    if (field1.Source!=null && field1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field1.Source = player1.Image;
                    }
                    else
                    {
                        field1.Source = null;
                    }
                    break;

                case "field2":
                    if (field2.Source != null && field2.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field2.Source = player1.Image;
                    }
                    else
                    {
                        field2.Source = null;
                    }
                    break;

                case "field3":
                    if (field3.Source != null && field3.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field3.Source = player1.Image;
                    }
                    else
                    {
                        field3.Source = null;
                    }
                    break;
                case "field4":
                    if (field4.Source != null && field4.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field4.Source = player1.Image;
                    }
                    else
                    {
                        field4.Source = null;
                    }
                    break;
                case "field5":
                    if (field5.Source != null && field5.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field5.Source = player1.Image;
                    }
                    else
                    {
                        field5.Source = null;
                    }
                    break;
                case "field6":
                    if (field6.Source != null && field6.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field6.Source = player1.Image;
                    }
                    else
                    {
                        field6.Source = null;
                    }
                    break;
                case "field7":
                    if (field7.Source != null && field7.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field7.Source = player1.Image;
                    }
                    else
                    {
                        field7.Source = null;
                    }
                    break;
                case "field8":
                    if (field8.Source != null && field8.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field8.Source = player1.Image;
                    }
                    else
                    {
                        field8.Source = null;
                    }
                    break;
                case "field9":
                    if (field9.Source != null && field9.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field9.Source = player1.Image;
                    }
                    else
                    {
                        field9.Source = null;
                    }
                    break;
                case "field10":
                    if (field10.Source != null && field10.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field10.Source = player1.Image;
                    }
                    else
                    {
                        field10.Source = null;
                    }
                    break;
                case "field11":
                    if (field11.Source != null && field11.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field11.Source = player1.Image;
                    }
                    else
                    {
                        field11.Source = null;
                    }
                    break;
                case "field12":
                    if (field12.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field12.Source = player1.Image;
                    }
                    else
                    {
                        field12.Source = null;
                    }
                    break;
                case "field13":
                    if (field13.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field13.Source = player1.Image;
                    }
                    else
                    {
                        field13.Source = null;
                    }
                    break;
                case "field14":
                    if (field14.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field14.Source = player1.Image;
                    }
                    else
                    {
                        field14.Source = null;
                    }
                    break;
                case "field15":
                    if (field15.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field15.Source = player1.Image;
                    }
                    else
                    {
                        field15.Source = null;
                    }
                    break;
                case "field16":
                    if (field16.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field16.Source = player1.Image;
                    }
                    else
                    {
                        field16.Source = null;
                    }
                    break;
                case "field17":
                    if (field17.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field17.Source = player1.Image;
                    }
                    else
                    {
                        field17.Source = null;
                    }
                    break;
                case "field18":
                    if (field18.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field18.Source = player1.Image;
                    }
                    else
                    {
                        field18.Source = null;
                    }
                    break;
                case "field19":
                    if (field19.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field19.Source = player1.Image;
                    }
                    else
                    {
                        field19.Source = null;
                    }
                    break;
                case "field20":
                    if (field20.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field20.Source = player1.Image;
                    }
                    else
                    {
                        field20.Source = null;
                    }
                    break;
                case "field21":
                    if (field21.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field21.Source = player1.Image;
                    }
                    else
                    {
                        field21.Source = null;
                    }
                    break;
                case "field22":
                    if (field22.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field22.Source = player1.Image;
                    }
                    else
                    {
                        field22.Source = null;
                    }
                    break;
                case "field23":
                    if (field23.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        field23.Source = player1.Image;
                    }
                    else
                    {
                        field23.Source = null;
                    }
                    break;
            }
            xdplayer2 = xdplayer2 + r;
            if (xdplayer2 >= 23)
            {
                int help1 = 23 - xdplayer2;
                xdplayer2 = 0 + help1;
                player2.Money = player2.Money + 200;
                xdplayer2 = xdplayer2 + r;
            }
            switch (fields[xdplayer2].Name)
            {
                case "field1":
                    if (field1.Source == null)
                    {
                        field1.Source = player2.Image;
                    }
                    else if (field1.Source != null)
                    {
                        field1.Source = "player1andplayer2img.png";
                    }
                    break;

                case "field2":
                    if (field2.Source == null)
                    {
                        field2.Source = player2.Image;
                    }
                    else if (field2.Source != null)
                    {
                        field2.Source = "player1andplayer2img.png";
                    }
                    break;

                case "field3":
                    if (field3.Source == null)
                    {
                        field3.Source = player2.Image;
                    }
                    else if (field3.Source != null)
                    {
                        field3.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field4":
                    if (field4.Source == null)
                    {
                        field4.Source = player2.Image;
                    }
                    else if (field4.Source != null)
                    {
                        field4.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field5":
                    if (field5.Source == null)
                    {
                        field5.Source = player2.Image;
                    }
                    else if (field5.Source != null)
                    {
                        field5.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field6":
                    if (field6.Source == null)
                    {
                        field6.Source = player2.Image;
                    }
                    else if (field6.Source != null)
                    {
                        field6.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field7":
                    if (field7.Source == null)
                    {
                        field7.Source = player2.Image;
                    }
                    else if (field7.Source != null)
                    {
                        field7.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field8":
                    if (field8.Source == null)
                    {
                        field8.Source = player2.Image;
                    }
                    else if (field8.Source != null)
                    {
                        field8.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field9":
                    if (field9.Source == null)
                    {
                        field9.Source = player2.Image;
                    }
                    else if (field9.Source != null)
                    {
                        field9.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field10":
                    if (field10.Source == null)
                    {
                        field10.Source = player2.Image;
                    }
                    else if (field10.Source != null)
                    {
                        field10.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field11":
                    if (field11.Source == null)
                    {
                        field11.Source = player2.Image;
                    }
                    else if (field11.Source != null)
                    {
                        field11.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field12":
                    if (field12.Source == null)
                    {
                        field12.Source = player2.Image;
                    }
                    else if (field12.Source != null)
                    {
                        field12.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field13":
                    if (field13.Source == null)
                    {
                        field13.Source = player2.Image;
                    }
                    else if (field13.Source != null)
                    {
                        field13.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field14":
                    if (field14.Source == null)
                    {
                        field14.Source = player2.Image;
                    }
                    else if (field14.Source != null)
                    {
                        field14.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field15":
                    if (field15.Source == null)
                    {
                        field15.Source = player2.Image;
                    }
                    else if (field15.Source != null)
                    {
                        field15.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field16":
                    if (field16.Source == null)
                    {
                        field16.Source = player2.Image;
                    }
                    else if (field16.Source != null)
                    {
                        field16.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field17":
                    if (field17.Source == null)
                    {
                        field17.Source = player2.Image;
                    }
                    else if (field17.Source != null)
                    {
                        field17.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field18":
                    if (field18.Source == null)
                    {
                        field18.Source = player2.Image;
                    }
                    else if (field18.Source != null)
                    {
                        field18.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field19":
                    if (field19.Source == null)
                    {
                        field19.Source = player2.Image;
                    }
                    else if (field19.Source != null)
                    {
                        field19.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field20":
                    if (field20.Source == null)
                    {
                        field20.Source = player2.Image;
                    }
                    else if (field20.Source != null)
                    {
                        field20.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field21":
                    if (field21.Source == null)
                    {
                        field21.Source = player2.Image;
                    }
                    else if (field21.Source != null)
                    {
                        field21.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field22":
                    if (field22.Source == null)
                    {
                        field22.Source = player2.Image;
                    }
                    else if (field22.Source != null)
                    {
                        field22.Source = "player1andplayer2img.png";
                    }
                    break;
                case "field23":
                    if (field23.Source == null)
                    {
                        field23.Source = player2.Image;
                    }
                    else if (field23.Source != null)
                    {
                        field23.Source = "player1andplayer2img.png";
                    }
                    break;
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