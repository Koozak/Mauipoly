using MauiPageFullScreen;
using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Mauipoly;

public partial class PlayPagePlayer : ContentPage
{
    int global = 0;
    bool yesp11 = false;
    bool yesp12 = false;
    bool yesp13 = false;
    bool yesp14 = false;
    bool yesp21 = false;
    bool yesp22 = false;
    bool yesp23 = false;
    bool yesp24 = false;
    int if_won_player1 = 0;
    int if_won_player2 = 0;
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

    private void Btn_Fullscreen_Clicked(object sender, EventArgs e)
    {
        Controls.ToggleFullScreenStatus();
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
            foreach (var field in player1.BoardFieldList)
            {
                if (field!=null&&field.Name == "betrayal1")
                {
                    if (yesp11 == false)
                    {
                        if_won_player1++;
                        yesp11 = true;
                    }
                }
                if (field != null && field.Name == "betrayal2")
                {
                    if (yesp12 == false)
                    {
                        if_won_player1++;
                        yesp12 = true;
                    }
                }
                if (field != null && field.Name == "betrayal3")
                {
                    if (yesp13 == false)
                    {
                        if_won_player1++;
                        yesp13 = true;
                    }
                }
                if(field != null && field.Name == "betrayal4")
                {
                    if (yesp14 == false)
                    {
                        if_won_player1++;
                        yesp14 = true;
                    }
                }
            }
            if (if_won_player1 == 4)
            {
                DisplayAlert("End Game", "Player 1 won", "OK");
                ThrowBtn.Opacity = 0.5;
                ThrowBtn.IsEnabled = false;
                EndTurn.Opacity = 0.5;
                EndTurn.IsEnabled = false;
                BuyField.Opacity = 0.5;
                BuyField.IsEnabled = false;
            }
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
            foreach (var field in player2.BoardFieldList)
            {
                if (field != null && field.Name == "betrayal1")
                {
                    if (yesp21 == false)
                    {
                        if_won_player2++;
                        yesp21 = true;
                    }
                }
                if (field != null && field.Name == "betrayal2")
                {
                    if (yesp22 == false)
                    {
                        if_won_player2++;
                        yesp22 = true;
                    }
                }
                if (field != null && field.Name == "betrayal3")
                {
                    if (yesp23==false)
                    {
                        if_won_player2++;
                        yesp23 = true;
                    }
                    
                }
                if (field != null && field.Name == "betrayal4")
                {
                    if (yesp24 == false)
                    {
                        if_won_player2++;
                        yesp24 = true;
                    }
                }
            }
            if (if_won_player2 == 4)
            {
                DisplayAlert("End Game", "Player 2 won", "OK");
                ThrowBtn.Opacity = 0.5;
                ThrowBtn.IsEnabled = false;
                EndTurn.Opacity = 0.5;
                EndTurn.IsEnabled = false;
                BuyField.Opacity = 0.5;
                BuyField.IsEnabled = false;
            }

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
                case "Start":
                    if (Start.Source!=null && Start.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        Start.Source = player2.Image;
                    }
                    else
                    {
                        Start.Source = null;
                    }
                    break;

                case "prison1":
                    if (prison1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        prison1.Source = player2.Image;
                    }
                    else
                    {
                        prison1.Source = null;
                    }
                    break;

                case "prison2":
                    if (prison2.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        prison2.Source = player2.Image;
                    }
                    else
                    {
                        prison2.Source = null;
                    }
                    break;
                case "betrayal1":
                    if (betrayal1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        betrayal1.Source = player2.Image;
                    }
                    else
                    {
                        betrayal1.Source = null;
                    }
                    break;
                case "desire1":
                    if (desire1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        desire1.Source = player2.Image;
                    }
                    else
                    {
                        desire1.Source = null;
                    }
                    break;
                case "desire2":
                    if (desire2.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        desire2.Source = player2.Image;
                    }
                    else
                    {
                        desire2.Source = null;
                    }
                    break;
                case "cambions":
                    if (cambions.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        cambions.Source = player2.Image;
                    }
                    else
                    {
                        cambions.Source = null;
                    }
                    break;
                case "gluttony1":
                    if (gluttony1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        gluttony1.Source = player2.Image;
                    }
                    else
                    {
                        gluttony1.Source = null;
                    }
                    break;
                case "gluttony2":
                    if (gluttony2.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        gluttony2.Source = player2.Image;
                    }
                    else
                    {
                        gluttony2.Source = null;
                    }
                    break;
                case "betrayal2":
                    if (betrayal2.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        betrayal2.Source = player2.Image;
                    }
                    else
                    {
                        betrayal2.Source = null;
                    }
                    break;
                case "greed1":
                    if (greed1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        greed1.Source = player2.Image;
                    }
                    else
                    {
                        greed1.Source = null;
                    }
                    break;
                case "greed2":
                    if (greed2.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        greed2.Source = player2.Image;
                    }
                    else
                    {
                        greed2.Source = null;
                    }
                    break;
                case "wayToHeven":
                    if (wayToHeven.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        wayToHeven.Source = player2.Image;
                    }
                    else
                    {
                        wayToHeven.Source = null;
                    }
                    break;
                case "anger1":
                    if (anger1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        anger1.Source = player2.Image;
                    }
                    else
                    {
                        anger1.Source = null;
                    }
                    break;
                case "anger2":
                    if (anger2.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        anger2.Source = player2.Image;
                    }
                    else
                    {
                        anger2.Source = null;
                    }
                    break;
                case "betrayal3":
                    if (betrayal3.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        betrayal3.Source = player2.Image;
                    }
                    else
                    {
                        betrayal3.Source = null;
                    }
                    break;
                case "heresy1":
                    if (heresy1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        heresy1.Source = player2.Image;
                    }
                    else
                    {
                        heresy1.Source = null;
                    }
                    break;
                case "heresy2":
                    if (heresy2.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        heresy2.Source = player2.Image;
                    }
                    else
                    {
                        heresy2.Source = null;
                    }
                    break;
                case "devil":
                    if (devil.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        devil.Source = player2.Image;
                    }
                    else
                    {
                        devil.Source = null;
                    }
                    break;
                case "violence1":
                    if (violence1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        violence1.Source = player2.Image;
                    }
                    else
                    {
                        violence1.Source = null;
                    }
                    break;
                case "violence2":
                    if (violence2.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        violence2.Source = player2.Image;
                    }
                    else
                    {
                        violence2.Source = null;
                    }
                    break;
                case "betrayal4":
                    if (betrayal4.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        betrayal4.Source = player2.Image;
                    }
                    else
                    {
                        betrayal4.Source = null;
                    }
                    break;
                case "extortion1":
                    if (extortion1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        extortion1.Source = player2.Image;
                    }
                    else
                    {
                        extortion1.Source = null;
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
                case "Start":
                    if (Start.Source == null)
                    {
                        Start.Source = player1.Image;
                    }
                    else if (Start.Source != null)
                    {
                        Start.Source = "player1andplayer2img.png";
                    }
                    break;

                case "prison1":
                    if (prison1.Source == null)
                    {
                        prison1.Source = player1.Image;
                    }
                    else if (prison1.Source != null)
                    {
                        prison1.Source = "player1andplayer2img.png";
                    }
                    break;

                case "prison2":
                    if (prison2.Source == null)
                    {
                        prison2.Source = player1.Image;
                    }
                    else if (prison2.Source != null)
                    {
                        prison2.Source = "player1andplayer2img.png";
                    }
                    break;
                case "betrayal1":
                    if (betrayal1.Source == null)
                    {
                        betrayal1.Source = player1.Image;
                    }
                    else if (betrayal1.Source != null)
                    {
                        betrayal1.Source = "player1andplayer2img.png";
                    }
                    break;
                case "desire1":
                    if (desire1.Source == null)
                    {
                        desire1.Source = player1.Image;
                    }
                    else if (desire1.Source != null)
                    {
                        desire1.Source = "player1andplayer2img.png";
                    }
                    break;
                case "desire2":
                    if (desire2.Source == null)
                    {
                        desire2.Source = player1.Image;
                    }
                    else if (desire2.Source != null)
                    {
                        desire2.Source = "player1andplayer2img.png";
                    }
                    break;
                case "cambions":
                    if (cambions.Source == null)
                    {
                        cambions.Source = player1.Image;
                    }
                    else if (cambions.Source != null)
                    {
                        cambions.Source = "player1andplayer2img.png";
                    }
                    break;
                case "gluttony1":
                    if (gluttony1.Source == null)
                    {
                        gluttony1.Source = player1.Image;
                    }
                    else if (gluttony1.Source != null)
                    {
                        gluttony1.Source = "player1andplayer2img.png";
                    }
                    break;
                case "gluttony2":
                    if (gluttony2.Source == null)
                    {
                        gluttony2.Source = player1.Image;
                    }
                    else if (gluttony2.Source != null)
                    {
                        gluttony2.Source = "player1andplayer2img.png";
                    }
                    break;
                case "betrayal2":
                    if (betrayal2.Source == null)
                    {
                        betrayal2.Source = player1.Image;
                    }
                    else if (betrayal2.Source != null)
                    {
                        betrayal2.Source = "player1andplayer2img.png";
                    }
                    break;
                case "greed1":
                    if (greed1.Source == null)
                    {
                        greed1.Source = player1.Image;
                    }
                    else if (greed1.Source != null)
                    {
                        greed1.Source = "player1andplayer2img.png";
                    }
                    break;
                case "greed2":
                    if (greed2.Source == null)
                    {
                        greed2.Source = player1.Image;
                    }
                    else if (greed2.Source != null)
                    {
                        greed2.Source = "player1andplayer2img.png";
                    }
                    break;
                case "wayToHeven":
                    if (wayToHeven.Source == null)
                    {
                        wayToHeven.Source = player1.Image;
                    }
                    else if (wayToHeven.Source != null)
                    {
                        wayToHeven.Source = "player1andplayer2img.png";
                    }
                    break;
                case "anger1":
                    if (anger1.Source == null)
                    {
                        anger1.Source = player1.Image;
                    }
                    else if (anger1.Source != null)
                    {
                        anger1.Source = "player1andplayer2img.png";
                    }
                    break;
                case "anger2":
                    if (anger2.Source == null)
                    {
                        anger2.Source = player1.Image;
                    }
                    else if (anger2.Source != null)
                    {
                        anger2.Source = "player1andplayer2img.png";
                    }
                    break;
                case "betrayal3":
                    if (betrayal3.Source == null)
                    {
                        betrayal3.Source = player1.Image;
                    }
                    else if (betrayal3.Source != null)
                    {
                        betrayal3.Source = "player1andplayer2img.png";
                    }
                    break;
                case "heresy1":
                    if (heresy1.Source == null)
                    {
                        heresy1.Source = player1.Image;
                    }
                    else if (heresy1.Source != null)
                    {
                        heresy1.Source = "player1andplayer2img.png";
                    }
                    break;
                case "heresy2":
                    if (heresy2.Source == null)
                    {
                        heresy2.Source = player1.Image;
                    }
                    else if (heresy2.Source != null)
                    {
                        heresy2.Source = "player1andplayer2img.png";
                    }
                    break;
                case "devil":
                    if (devil.Source == null)
                    {
                        devil.Source = player1.Image;
                    }
                    else if (devil.Source != null)
                    {
                        devil.Source = "player1andplayer2img.png";
                    }
                    break;
                case "violence1":
                    if (violence1.Source == null)
                    {
                        violence1.Source = player1.Image;
                    }
                    else if (violence1.Source != null)
                    {
                        violence1.Source = "player1andplayer2img.png";
                    }
                    break;
                case "violence2":
                    if (violence2.Source == null)
                    {
                        violence2.Source = player1.Image;
                    }
                    else if (violence2.Source != null)
                    {
                        violence2.Source = "player1andplayer2img.png";
                    }
                    break;
                case "betrayal4":
                    if (betrayal4.Source == null)
                    {
                        betrayal4.Source = player1.Image;
                    }
                    else if (betrayal4.Source != null)
                    {
                        betrayal4.Source = "player1andplayer2img.png";
                    }
                    break;
                case "extortion1":
                    if (extortion1.Source == null)
                    {
                        extortion1.Source = player1.Image;
                    }
                    else if (extortion1.Source != null)
                    {
                        extortion1.Source = "player1andplayer2img.png";
                    }
                    break;
            }
        }
        else
        {
            switch (fields[xdplayer2].Name)
            {
                case "Start":
                    if (Start.Source!=null && Start.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        Start.Source = player1.Image;
                    }
                    else
                    {
                        Start.Source = null;
                    }
                    break;

                case "prison1":
                    if (prison1.Source != null && prison1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        prison1.Source = player1.Image;
                    }
                    else
                    {
                        prison1.Source = null;
                    }
                    break;

                case "prison2":
                    if (prison2.Source != null && prison2.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        prison2.Source = player1.Image;
                    }
                    else
                    {
                        prison2.Source = null;
                    }
                    break;
                case "betrayal1":
                    if (betrayal1.Source != null && betrayal1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        betrayal1.Source = player1.Image;
                    }
                    else
                    {
                        betrayal1.Source = null;
                    }
                    break;
                case "desire1":
                    if (desire1.Source != null && desire1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        desire1.Source = player1.Image;
                    }
                    else
                    {
                        desire1.Source = null;
                    }
                    break;
                case "desire2":
                    if (desire2.Source != null && desire2.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        desire2.Source = player1.Image;
                    }
                    else
                    {
                        desire2.Source = null;
                    }
                    break;
                case "cambions":
                    if (cambions.Source != null && cambions.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        cambions.Source = player1.Image;
                    }
                    else
                    {
                        cambions.Source = null;
                    }
                    break;
                case "gluttony1":
                    if (gluttony1.Source != null && gluttony1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        gluttony1.Source = player1.Image;
                    }
                    else
                    {
                        gluttony1.Source = null;
                    }
                    break;
                case "gluttony2":
                    if (gluttony2.Source != null && gluttony2.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        gluttony2.Source = player1.Image;
                    }
                    else
                    {
                        gluttony2.Source = null;
                    }
                    break;
                case "betrayal2":
                    if (betrayal2.Source != null && betrayal2.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        betrayal2.Source = player1.Image;
                    }
                    else
                    {
                        betrayal2.Source = null;
                    }
                    break;
                case "greed1":
                    if (greed1.Source != null && greed1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        greed1.Source = player1.Image;
                    }
                    else
                    {
                        greed1.Source = null;
                    }
                    break;
                case "greed2":
                    if (greed2.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        greed2.Source = player1.Image;
                    }
                    else
                    {
                        greed2.Source = null;
                    }
                    break;
                case "wayToHeven":
                    if (wayToHeven.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        wayToHeven.Source = player1.Image;
                    }
                    else
                    {
                        wayToHeven.Source = null;
                    }
                    break;
                case "anger1":
                    if (anger1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        anger1.Source = player1.Image;
                    }
                    else
                    {
                        anger1.Source = null;
                    }
                    break;
                case "anger2":
                    if (anger2.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        anger2.Source = player1.Image;
                    }
                    else
                    {
                        anger2.Source = null;
                    }
                    break;
                case "betrayal3":
                    if (betrayal3.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        betrayal3.Source = player1.Image;
                    }
                    else
                    {
                        betrayal3.Source = null;
                    }
                    break;
                case "heresy1":
                    if (heresy1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        heresy1.Source = player1.Image;
                    }
                    else
                    {
                        heresy1.Source = null;
                    }
                    break;
                case "heresy2":
                    if (heresy2.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        heresy2.Source = player1.Image;
                    }
                    else
                    {
                        heresy2.Source = null;
                    }
                    break;
                case "devil":
                    if (devil.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        devil.Source = player1.Image;
                    }
                    else
                    {
                        devil.Source = null;
                    }
                    break;
                case "violence1":
                    if (violence1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        violence1.Source = player1.Image;
                    }
                    else
                    {
                        violence1.Source = null;
                    }
                    break;
                case "violence2":
                    if (violence2.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        violence2.Source = player1.Image;
                    }
                    else
                    {
                        violence2.Source = null;
                    }
                    break;
                case "betrayal4":
                    if (betrayal4.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        betrayal4.Source = player1.Image;
                    }
                    else
                    {
                        betrayal4.Source = null;
                    }
                    break;
                case "extortion1":
                    if (extortion1.Source.ToString() == "File: player1andplayer2img.png")
                    {
                        extortion1.Source = player1.Image;
                    }
                    else
                    {
                        extortion1.Source = null;
                    }
                    break;
            }
            xdplayer2 = xdplayer2 + r;
            if (xdplayer2 >= 23)
            {
                int help1 = xdplayer2 - 23;
                xdplayer2 = 0 + help1;
                player2.Money = player2.Money + 200;
            }
            switch (fields[xdplayer2].Name)
            {
                case "Start":
                    if (Start.Source == null)
                    {
                        Start.Source = player2.Image;
                    }
                    else if (Start.Source != null)
                    {
                        Start.Source = "player1andplayer2img.png";
                    }
                    break;

                case "prison1":
                    if (prison1.Source == null)
                    {
                        prison1.Source = player2.Image;
                    }
                    else if (prison1.Source != null)
                    {
                        prison1.Source = "player1andplayer2img.png";
                    }
                    break;

                case "prison2":
                    if (prison2.Source == null)
                    {
                        prison2.Source = player2.Image;
                    }
                    else if (prison2.Source != null)
                    {
                        prison2.Source = "player1andplayer2img.png";
                    }
                    break;
                case "betrayal1":
                    if (betrayal1.Source == null)
                    {
                        betrayal1.Source = player2.Image;
                    }
                    else if (betrayal1.Source != null)
                    {
                        betrayal1.Source = "player1andplayer2img.png";
                    }
                    break;
                case "desire1":
                    if (desire1.Source == null)
                    {
                        desire1.Source = player2.Image;
                    }
                    else if (desire1.Source != null)
                    {
                        desire1.Source = "player1andplayer2img.png";
                    }
                    break;
                case "desire2":
                    if (desire2.Source == null)
                    {
                        desire2.Source = player2.Image;
                    }
                    else if (desire2.Source != null)
                    {
                        desire2.Source = "player1andplayer2img.png";
                    }
                    break;
                case "cambions":
                    if (cambions.Source == null)
                    {
                        cambions.Source = player2.Image;
                    }
                    else if (cambions.Source != null)
                    {
                        cambions.Source = "player1andplayer2img.png";
                    }
                    break;
                case "gluttony1":
                    if (gluttony1.Source == null)
                    {
                        gluttony1.Source = player2.Image;
                    }
                    else if (gluttony1.Source != null)
                    {
                        gluttony1.Source = "player1andplayer2img.png";
                    }
                    break;
                case "gluttony2":
                    if (gluttony2.Source == null)
                    {
                        gluttony2.Source = player2.Image;
                    }
                    else if (gluttony2.Source != null)
                    {
                        gluttony2.Source = "player1andplayer2img.png";
                    }
                    break;
                case "betrayal2":
                    if (betrayal2.Source == null)
                    {
                        betrayal2.Source = player2.Image;
                    }
                    else if (betrayal2.Source != null)
                    {
                        betrayal2.Source = "player1andplayer2img.png";
                    }
                    break;
                case "greed1":
                    if (greed1.Source == null)
                    {
                        greed1.Source = player2.Image;
                    }
                    else if (greed1.Source != null)
                    {
                        greed1.Source = "player1andplayer2img.png";
                    }
                    break;
                case "greed2":
                    if (greed2.Source == null)
                    {
                        greed2.Source = player2.Image;
                    }
                    else if (greed2.Source != null)
                    {
                        greed2.Source = "player1andplayer2img.png";
                    }
                    break;
                case "wayToHeven":
                    if (wayToHeven.Source == null)
                    {
                        wayToHeven.Source = player2.Image;
                    }
                    else if (wayToHeven.Source != null)
                    {
                        wayToHeven.Source = "player1andplayer2img.png";
                    }
                    break;
                case "anger1":
                    if (anger1.Source == null)
                    {
                        anger1.Source = player2.Image;
                    }
                    else if (anger1.Source != null)
                    {
                        anger1.Source = "player1andplayer2img.png";
                    }
                    break;
                case "anger2":
                    if (anger2.Source == null)
                    {
                        anger2.Source = player2.Image;
                    }
                    else if (anger2.Source != null)
                    {
                        anger2.Source = "player1andplayer2img.png";
                    }
                    break;
                case "betrayal3":
                    if (betrayal3.Source == null)
                    {
                        betrayal3.Source = player2.Image;
                    }
                    else if (betrayal3.Source != null)
                    {
                        betrayal3.Source = "player1andplayer2img.png";
                    }
                    break;
                case "heresy1":
                    if (heresy1.Source == null)
                    {
                        heresy1.Source = player2.Image;
                    }
                    else if (heresy1.Source != null)
                    {
                        heresy1.Source = "player1andplayer2img.png";
                    }
                    break;
                case "heresy2":
                    if (heresy2.Source == null)
                    {
                        heresy2.Source = player2.Image;
                    }
                    else if (heresy2.Source != null)
                    {
                        heresy2.Source = "player1andplayer2img.png";
                    }
                    break;
                case "devil":
                    if (devil.Source == null)
                    {
                        devil.Source = player2.Image;
                    }
                    else if (devil.Source != null)
                    {
                        devil.Source = "player1andplayer2img.png";
                    }
                    break;
                case "violence1":
                    if (violence1.Source == null)
                    {
                        violence1.Source = player2.Image;
                    }
                    else if (violence1.Source != null)
                    {
                        violence1.Source = "player1andplayer2img.png";
                    }
                    break;
                case "violence2":
                    if (violence2.Source == null)
                    {
                        violence2.Source = player2.Image;
                    }
                    else if (violence2.Source != null)
                    {
                        violence2.Source = "player1andplayer2img.png";
                    }
                    break;
                case "betrayal4":
                    if (betrayal4.Source == null)
                    {
                        betrayal4.Source = player2.Image;
                    }
                    else if (betrayal4.Source != null)
                    {
                        betrayal4.Source = "player1andplayer2img.png";
                    }
                    break;
                case "extortion1":
                    if (extortion1.Source == null)
                    {
                        extortion1.Source = player2.Image;
                    }
                    else if (extortion1.Source != null)
                    {
                        extortion1.Source = "player1andplayer2img.png";
                    }
                    break;
            }
        }
    }
    private void SetFields()
    {
        BoardField Start = new BoardField("Start", 1000000000, 6, 0);
        BoardField prison1 = new BoardField("prison1", 50, 5, 0);
        BoardField prison2 = new BoardField("prison2", 50, 4, 0);
        BoardField betrayal1 = new BoardField("betrayal1", 150, 3, 0);
        BoardField desire1 = new BoardField("desire1", 50, 2, 0);
        BoardField desire2 = new BoardField("desire2", 50, 1, 0);
        BoardField cambions = new BoardField("cambions", 1000000000, 0, 0);
        BoardField gluttony1 = new BoardField("gluttony1", 150, 0, 1);
        BoardField gluttony2 = new BoardField("gluttony2", 150, 0, 2);
        BoardField betrayal2 = new BoardField("betrayal2", 300, 0, 3);
        BoardField greed1 = new BoardField("greed1", 150, 0, 4);
        BoardField greed2 = new BoardField("greed2", 150, 0, 5);
        BoardField wayToHeven = new BoardField("wayToHeven", 1000000000, 0, 6);
        BoardField anger1 = new BoardField("anger1", 300, 1, 6);
        BoardField anger2 = new BoardField("anger2", 300, 2, 6);
        BoardField betrayal3 = new BoardField("betrayal3", 500, 3, 6);
        BoardField heresy1 = new BoardField("heresy1", 300, 4, 6);
        BoardField heresy2 = new BoardField("heresy2", 300, 5, 6);
        BoardField devil = new BoardField("devil", 1000000000, 6, 6);
        BoardField violence1 = new BoardField("violence1", 500, 6, 5);
        BoardField violence2 = new BoardField("violence2", 500, 6, 4);
        BoardField betrayal4 = new BoardField("betrayal4", 600, 6, 3);
        BoardField extortion1 = new BoardField("extortion1", 500, 6, 2);
        BoardField extortion2 = new BoardField("extortion2", 500, 6, 1);
        fields[0] = Start;
        fields[1] = prison1;
        fields[2] = prison2;
        fields[3] = betrayal1;
        fields[4] = desire1;
        fields[5] = desire2;
        fields[6] = cambions;
        fields[7] = gluttony1;
        fields[8] = gluttony2;
        fields[9] = betrayal2;
        fields[10] = greed1;
        fields[11] = greed2;
        fields[12] = wayToHeven;
        fields[13] = anger1;
        fields[14] = anger2;
        fields[15] = betrayal3;
        fields[16] = heresy1;
        fields[17] = heresy2;
        fields[18] = devil;
        fields[19] = violence1;
        fields[20] = violence2;
        fields[21] = betrayal4;
        fields[22] = extortion1;
        fields[23] = extortion2;
    }
}