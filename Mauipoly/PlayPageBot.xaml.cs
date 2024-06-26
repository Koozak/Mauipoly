using MauiPageFullScreen;

namespace Mauipoly;

public partial class PlayPageBot : ContentPage
{
    Random rnd = new Random();
    Random rnd1 = new Random();
    Random rnd2 = new Random();
    bool yesp1 = false;
    bool yesp2 = false;
    bool yesp3 = false;
    bool yesp4 = false;
    int if_won_player = 0;
    bool yesb1 = false;
    bool yesb2 = false;
    bool yesb3 = false;
    bool yesb4 = false;
    int if_won_bot = 0;
    public int xdplayer = 0;
    public int xdbot = 0;
    public int global = 0;
    private BoardField[] fields = new BoardField[24];
    Player player = new Player("Player 1", false, "player1img.png");
    Bot bot = new Bot("Bot 1", false, "botimg.png");
    public PlayPageBot()
    {
        InitializeComponent();
        SetFields();
        Whose_Turn(player, bot);
    }
    private void Btn_Settings_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage());
    }
    private async void Whose_Turn(Player one, Bot two)
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
            int value2 = rnd1.Next(1, 5) + rnd2.Next(1, 5); 
            await Task.Delay(1000);
            Moving(value2);
            bot.isTurn = false;
            player.isTurn = true;
            await Task.Delay(1000);
            if (fields[xdbot].Occupied == false)
            {
                if (fields[xdbot].HowMuchForField != 1000000000)
                {
                    if ((bot.Money - fields[xdbot].HowMuchForField) <= 0)
                    {

                    }
                    else
                    {
                        bot.Money = bot.Money - fields[xdbot].HowMuchForField;
                        fields[xdbot].Occupied = true;
                        bot.BoardFieldList[global] = fields[xdbot];
                        await DisplayAlert("Bot", "Bot bought " + bot.BoardFieldList[global].Name.ToString(), "OK");
                        global++;
                    }
                }
            }
            ShowingStats(player);
        }
    }
    private void ShowingStats(Player player)
    {
        LabeLWithPlayerName.Text = player.Nickname + " Turn";
        PlayerImage.Source = player.Image;
        PlayerValues.Text = "Your Money is " + player.Money.ToString() + "     ";
        foreach (var item in player.BoardFieldList)
        {
            if (item == null)
            {

            }
            else
            {
                PlayerValues.Text += item.Name.ToString() + " This field costs " + item.HowMuchForField + System.Environment.NewLine;
            }

        }

    }
    private async void Btn_Throw_Clicked(object sender, EventArgs e)
    {
        if (player.isTurn == true)
        {
            int value = rnd1.Next(1, 5) + rnd2.Next(1, 5);
            ThrowBtn.Opacity = 0.5;
            ThrowBtn.IsEnabled = false;
            Moving(value);
            ShowingStats(player);
            foreach (var item in bot.BoardFieldList)
            {
                if (item != null && item.Name == fields[xdplayer].Name)
                {
                    await DisplayAlert("Enemy Field!", "You step on enemy field that will by cost " + fields[xdplayer].HowMuchForField + " You will have " + (player.Money - fields[xdplayer].HowMuchForField), "OK");
                    ShowingStats(player);
                    if (player.Money - fields[xdplayer].HowMuchForField < 0)
                    {
                        while (true == true)
                        {
                            foreach (var item1 in player.BoardFieldList)
                            {
                                if (item1 != null)
                                {
                                    bool answer = await DisplayAlert("Selling Fields", "Do you want to sell this field, it will give you " + item1.HowMuchForField + " You are still missing " + Math.Abs(player.Money - fields[xdplayer].HowMuchForField), "Yes", "NO");
                                    if (answer == true)
                                    {
                                        player.Money += item1.HowMuchForField;
                                        item1.Occupied = false;
                                        player.BoardFieldList = player.BoardFieldList.Where(val => val != item1).ToArray();
                                        if (player.Money - fields[xdplayer].HowMuchForField >= 0)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            bool answer1 = await DisplayAlert("End", "This is all you wanna sell", "Yes", "NO");
                            if (answer1 == true)
                            {
                                break;
                            }
                        }

                        if (player.Money - fields[xdplayer].HowMuchForField < 0)
                        {
                            await DisplayAlert("End Game", "Bot won", "OK");
                            ThrowBtn.Opacity = 0.5;
                            ThrowBtn.IsEnabled = false;
                            EndTurn.Opacity = 0.5;
                            EndTurn.IsEnabled = false;
                            BuyField.Opacity = 0.5;
                            BuyField.IsEnabled = false;
                        }
                        else
                        {
                            player.Money -= fields[xdplayer].HowMuchForField;
                            bot.Money += fields[xdplayer].HowMuchForField;
                            ShowingStats(player);
                        }
                    }
                    else
                    {
                        player.Money -= fields[xdplayer].HowMuchForField;
                        bot.Money += fields[xdplayer].HowMuchForField;
                        ShowingStats(player);
                    }
                    break;
                }
                else
                {

                }
            }
        }
    }

    private async void Btn_EndTurn_Clicked(object sender, EventArgs e)
    {
        if (player.isTurn == true)
        {

            player.isTurn = false;
            bot.isTurn = true;
            ShowingStats(player);
            int value = rnd1.Next(1, 5) + rnd2.Next(1, 5);
            Moving(value);
            player.isTurn = true;
            bot.isTurn = false;
            ThrowBtn.Opacity = 1;
            ThrowBtn.IsEnabled = true;
            if (xdbot >= 23)
            {
                xdbot = 0;
            }
            if (fields[xdbot].Occupied == false)
            {
                if (fields[xdbot].HowMuchForField != 1000000000)
                {
                    if ((bot.Money - fields[xdbot].HowMuchForField) < 0)
                    {

                    }
                    else
                    {
                        bot.Money = bot.Money - fields[xdbot].HowMuchForField;
                        fields[xdbot].Occupied = true;
                        bot.BoardFieldList[global] = fields[xdbot];
                        await DisplayAlert("Bot", "Bot bought " + bot.BoardFieldList[global].Name.ToString(), "OK");
                        global++;
                    }
                }
            }
            foreach (var item in player.BoardFieldList)
            {
                if (item != null && item.Name == fields[xdbot].Name)
                {
                    if (bot.Money - fields[xdbot].HowMuchForField < 0)
                    {
                        foreach (var item1 in bot.BoardFieldList)
                        {
                            if (item1 != null)
                            {
                                bot.Money += item1.HowMuchForField;
                                item1.Occupied = false;
                                bot.BoardFieldList = bot.BoardFieldList.Where(val => val != item1).ToArray();
                                if (bot.Money - fields[xdbot].HowMuchForField >= 0)
                                {
                                    break;
                                }
                            }
                        }
                        if (bot.Money - fields[xdbot].HowMuchForField < 0)
                        {
                            await DisplayAlert("End Game", "Player 1 won", "OK");
                            ThrowBtn.Opacity = 0.5;
                            ThrowBtn.IsEnabled = false;
                            EndTurn.Opacity = 0.5;
                            EndTurn.IsEnabled = false;
                            BuyField.Opacity = 0.5;
                            BuyField.IsEnabled = false;
                        }
                        else
                        {
                            bot.Money -= fields[xdbot].HowMuchForField;
                            player.Money += fields[xdbot].HowMuchForField;
                            await DisplayAlert("Bot step", "Bot step on your field " + item.Name + " You will get " + item.HowMuchForField, "Ok");

                        }
                    }
                    else
                    {
                        bot.Money -= fields[xdbot].HowMuchForField;
                        player.Money += fields[xdbot].HowMuchForField;
                        await DisplayAlert("Bot step", "Bot step on your field "+item.Name+ " You will get "+item.HowMuchForField, "Ok");

                    }
                    break;
                }
                else
                {

                }
            }
        foreach (var field in player.BoardFieldList)
            {
                if (field != null && field.Name == "betrayal1")
                {
                    if (yesp1 == false)
                    {
                        if_won_player++;
                        yesp1 = true;
                    }
                }
                if (field != null && field.Name == "betrayal2")
                {
                    if (yesp2 == false)
                    {
                        if_won_player++;
                        yesp2 = true;
                    }
                }
                if (field != null && field.Name == "betrayal3")
                {
                    if (yesp3 == false)
                    {
                        if_won_player++;
                        yesp3 = true;
                    }

                }
                if (field != null && field.Name == "betrayal4")
                {
                    if (yesp4 == false)
                    {
                        if_won_player++;
                        yesp4 = true;
                    }
                }
            }
            if (if_won_player == 4)
            {
                await DisplayAlert("End Game", "Player won", "OK");
                ThrowBtn.Opacity = 0.5;
                ThrowBtn.IsEnabled = false;
                EndTurn.Opacity = 0.5;
                EndTurn.IsEnabled = false;
                BuyField.Opacity = 0.5;
                BuyField.IsEnabled = false;
            }
            foreach (var field in bot.BoardFieldList)
            {
                if (field != null && field.Name == "betrayal1")
                {
                    if (yesb1 == false)
                    {
                        if_won_bot++;
                        yesb1 = true;
                    }
                }
                if (field != null && field.Name == "betrayal2")
                {
                    if (yesb2 == false)
                    {
                        if_won_bot++;
                        yesb2 = true;
                    }
                }
                if (field != null && field.Name == "betrayal3")
                {
                    if (yesb3 == false)
                    {
                        if_won_bot++;
                        yesb3 = true;
                    }

                }
                if (field != null && field.Name == "betrayal4")
                {
                    if (yesb4 == false)
                    {
                        if_won_bot++;
                        yesb4 = true;
                    }
                }
            }
            if (if_won_bot == 4)
            {
                await DisplayAlert("End Game", "Bot won", "OK");
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
        buyingFields(fields, player,xdplayer);
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
                    if ((player.Money - field[id].HowMuchForField) < 0)
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
            await DisplayAlert("Alert!", "It is occupied by " + player.Nickname, "OK");
        }
    }
        private void Moving(int r)
    {
        if (player.isTurn == true)
        {
            switch (fields[xdplayer].Name)
            {
                case "Start":
                    if (Start.Source != null && Start.Source.ToString() == "File: player1andbotimg.png")
                    {
                        Start.Source = bot.Image;
                    }
                    else
                    {
                        Start.Source = null;
                    }
                    break;

                case "prison1":
                    if (prison1.Source.ToString() == "File: player1andbotimg.png")
                    {
                        prison1.Source = bot.Image;
                    }
                    else
                    {
                        prison1.Source = null;
                    }
                    break;

                case "prison2":
                    if (prison2.Source.ToString() == "File: player1andbotimg.png")
                    {
                        prison2.Source = bot.Image;
                    }
                    else
                    {
                        prison2.Source = null;
                    }
                    break;
                case "betrayal1":
                    if (betrayal1.Source.ToString() == "File: player1andbotimg.png")
                    {
                        betrayal1.Source = bot.Image;
                    }
                    else
                    {
                        betrayal1.Source = null;
                    }
                    break;
                case "desire1":
                    if (desire1.Source.ToString() == "File: player1andbotimg.png")
                    {
                        desire1.Source = bot.Image;
                    }
                    else
                    {
                        desire1.Source = null;
                    }
                    break;
                case "desire2":
                    if (desire2.Source.ToString() == "File: player1andbotimg.png")
                    {
                        desire2.Source = bot.Image;
                    }
                    else
                    {
                        desire2.Source = null;
                    }
                    break;
                case "cambions":
                    if (cambions.Source.ToString() == "File: player1andbotimg.png")
                    {
                        cambions.Source = bot.Image;
                    }
                    else
                    {
                        cambions.Source = null;
                    }
                    break;
                case "gluttony1":
                    if (gluttony1.Source.ToString() == "File: player1andbotimg.png")
                    {
                        gluttony1.Source = bot.Image;
                    }
                    else
                    {
                        gluttony1.Source = null;
                    }
                    break;
                case "gluttony2":
                    if (gluttony2.Source.ToString() == "File: player1andbotimg.png")
                    {
                        gluttony2.Source = bot.Image;
                    }
                    else
                    {
                        gluttony2.Source = null;
                    }
                    break;
                case "betrayal2":
                    if (betrayal2.Source.ToString() == "File: player1andbotimg.png")
                    {
                        betrayal2.Source = bot.Image;
                    }
                    else
                    {
                        betrayal2.Source = null;
                    }
                    break;
                case "greed1":
                    if (greed1.Source.ToString() == "File: player1andbotimg.png")
                    {
                        greed1.Source = bot.Image;
                    }
                    else
                    {
                        greed1.Source = null;
                    }
                    break;
                case "greed2":
                    if (greed2.Source.ToString() == "File: player1andbotimg.png")
                    {
                        greed2.Source = bot.Image;
                    }
                    else
                    {
                        greed2.Source = null;
                    }
                    break;
                case "wayToHeven":
                    if (wayToHeven.Source.ToString() == "File: player1andbotimg.png")
                    {
                        wayToHeven.Source = bot.Image;
                    }
                    else
                    {
                        wayToHeven.Source = null;
                    }
                    break;
                case "anger1":
                    if (anger1.Source.ToString() == "File: player1andbotimg.png")
                    {
                        anger1.Source = bot.Image;
                    }
                    else
                    {
                        anger1.Source = null;
                    }
                    break;
                case "anger2":
                    if (anger2.Source.ToString() == "File: player1andbotimg.png")
                    {
                        anger2.Source = bot.Image;
                    }
                    else
                    {
                        anger2.Source = null;
                    }
                    break;
                case "betrayal3":
                    if (betrayal3.Source.ToString() == "File: player1andbotimg.png")
                    {
                        betrayal3.Source = bot.Image;
                    }
                    else
                    {
                        betrayal3.Source = null;
                    }
                    break;
                case "heresy1":
                    if (heresy1.Source.ToString() == "File: player1andbotimg.png")
                    {
                        heresy1.Source = bot.Image;
                    }
                    else
                    {
                        heresy1.Source = null;
                    }
                    break;
                case "heresy2":
                    if (heresy2.Source.ToString() == "File: player1andbotimg.png")
                    {
                        heresy2.Source = bot.Image;
                    }
                    else
                    {
                        heresy2.Source = null;
                    }
                    break;
                case "devil":
                    if (devil.Source.ToString() == "File: player1andbotimg.png")
                    {
                        devil.Source = bot.Image;
                    }
                    else
                    {
                        devil.Source = null;
                    }
                    break;
                case "violence1":
                    if (violence1.Source.ToString() == "File: player1andbotimg.png")
                    {
                        violence1.Source = bot.Image;
                    }
                    else
                    {
                        violence1.Source = null;
                    }
                    break;
                case "violence2":
                    if (violence2.Source.ToString() == "File: player1andbotimg.png")
                    {
                        violence2.Source = bot.Image;
                    }
                    else
                    {
                        violence2.Source = null;
                    }
                    break;
                case "betrayal4":
                    if (betrayal4.Source.ToString() == "File: player1andbotimg.png")
                    {
                        betrayal4.Source = bot.Image;
                    }
                    else
                    {
                        betrayal4.Source = null;
                    }
                    break;
                case "extortion1":
                    if (extortion1.Source.ToString() == "File: player1andbotimg.png")
                    {
                        extortion1.Source = bot.Image;
                    }
                    else
                    {
                        extortion1.Source = null;
                    }
                    break;
            }
            xdplayer = xdplayer + r;
            if (xdplayer >= 23)
            {
                int help1 = xdplayer - 23;
                xdplayer = 0 + help1;
                player.Money = player.Money + 100;
            }
            switch (fields[xdplayer].Name)
            {
                case "Start":
                    if (Start.Source == null)
                    {
                        Start.Source = player.Image;
                    }
                    else if (Start.Source != null)
                    {
                        Start.Source = "player1andbotimg.png";
                    }
                    break;

                case "prison1":
                    if (prison1.Source == null)
                    {
                        prison1.Source = player.Image;
                    }
                    else if (prison1.Source != null)
                    {
                        prison1.Source = "player1andbotimg.png";
                    }
                    break;

                case "prison2":
                    if (prison2.Source == null)
                    {
                        prison2.Source = player.Image;
                    }
                    else if (prison2.Source != null)
                    {
                        prison2.Source = "player1andbotimg.png";
                    }
                    break;
                case "betrayal1":
                    if (betrayal1.Source == null)
                    {
                        betrayal1.Source = player.Image;
                    }
                    else if (betrayal1.Source != null)
                    {
                        betrayal1.Source = "player1andbotimg.png";
                    }
                    break;
                case "desire1":
                    if (desire1.Source == null)
                    {
                        desire1.Source = player.Image;
                    }
                    else if (desire1.Source != null)
                    {
                        desire1.Source = "player1andbotimg.png";
                    }
                    break;
                case "desire2":
                    if (desire2.Source == null)
                    {
                        desire2.Source = player.Image;
                    }
                    else if (desire2.Source != null)
                    {
                        desire2.Source = "player1andbotimg.png";
                    }
                    break;
                case "cambions":
                    if (cambions.Source == null)
                    {
                        cambions.Source = player.Image;
                    }
                    else if (cambions.Source != null)
                    {
                        cambions.Source = "player1andbotimg.png";
                    }
                    break;
                case "gluttony1":
                    if (gluttony1.Source == null)
                    {
                        gluttony1.Source = player.Image;
                    }
                    else if (gluttony1.Source != null)
                    {
                        gluttony1.Source = "player1andbotimg.png";
                    }
                    break;
                case "gluttony2":
                    if (gluttony2.Source == null)
                    {
                        gluttony2.Source = player.Image;
                    }
                    else if (gluttony2.Source != null)
                    {
                        gluttony2.Source = "player1andbotimg.png";
                    }
                    break;
                case "betrayal2":
                    if (betrayal2.Source == null)
                    {
                        betrayal2.Source = player.Image;
                    }
                    else if (betrayal2.Source != null)
                    {
                        betrayal2.Source = "player1andbotimg.png";
                    }
                    break;
                case "greed1":
                    if (greed1.Source == null)
                    {
                        greed1.Source = player.Image;
                    }
                    else if (greed1.Source != null)
                    {
                        greed1.Source = "player1andbotimg.png";
                    }
                    break;
                case "greed2":
                    if (greed2.Source == null)
                    {
                        greed2.Source = player.Image;
                    }
                    else if (greed2.Source != null)
                    {
                        greed2.Source = "player1andbotimg.png";
                    }
                    break;
                case "wayToHeven":
                    if (wayToHeven.Source == null)
                    {
                        wayToHeven.Source = player.Image;
                    }
                    else if (wayToHeven.Source != null)
                    {
                        wayToHeven.Source = "player1andbotimg.png";
                    }
                    break;
                case "anger1":
                    if (anger1.Source == null)
                    {
                        anger1.Source = player.Image;
                    }
                    else if (anger1.Source != null)
                    {
                        anger1.Source = "player1andbotimg.png";
                    }
                    break;
                case "anger2":
                    if (anger2.Source == null)
                    {
                        anger2.Source = player.Image;
                    }
                    else if (anger2.Source != null)
                    {
                        anger2.Source = "player1andbotimg.png";
                    }
                    break;
                case "betrayal3":
                    if (betrayal3.Source == null)
                    {
                        betrayal3.Source = player.Image;
                    }
                    else if (betrayal3.Source != null)
                    {
                        betrayal3.Source = "player1andbotimg.png";
                    }
                    break;
                case "heresy1":
                    if (heresy1.Source == null)
                    {
                        heresy1.Source = player.Image;
                    }
                    else if (heresy1.Source != null)
                    {
                        heresy1.Source = "player1andbotimg.png";
                    }
                    break;
                case "heresy2":
                    if (heresy2.Source == null)
                    {
                        heresy2.Source = player.Image;
                    }
                    else if (heresy2.Source != null)
                    {
                        heresy2.Source = "player1andbotimg.png";
                    }
                    break;
                case "devil":
                    if (devil.Source == null)
                    {
                        devil.Source = player.Image;
                    }
                    else if (devil.Source != null)
                    {
                        devil.Source = "player1andbotimg.png";
                    }
                    break;
                case "violence1":
                    if (violence1.Source == null)
                    {
                        violence1.Source = player.Image;
                    }
                    else if (violence1.Source != null)
                    {
                        violence1.Source = "player1andbotimg.png";
                    }
                    break;
                case "violence2":
                    if (violence2.Source == null)
                    {
                        violence2.Source = player.Image;
                    }
                    else if (violence2.Source != null)
                    {
                        violence2.Source = "player1andbotimg.png";
                    }
                    break;
                case "betrayal4":
                    if (betrayal4.Source == null)
                    {
                        betrayal4.Source = player.Image;
                    }
                    else if (betrayal4.Source != null)
                    {
                        betrayal4.Source = "player1andbotimg.png";
                    }
                    break;
                case "extortion1":
                    if (extortion1.Source == null)
                    {
                        extortion1.Source = player.Image;
                    }
                    else if (extortion1.Source != null)
                    {
                        extortion1.Source = "player1andbotimg.png";
                    }
                    break;
            }
        }
        else
        {
            switch (fields[xdbot].Name)
            {
                case "Start":
                    if (Start.Source != null && Start.Source.ToString() == "File: player1andbotimg.png")
                    {
                        Start.Source = player.Image;
                    }
                    else
                    {
                        Start.Source = null;
                    }
                    break;

                case "prison1":
                    if (prison1.Source != null && prison1.Source.ToString() == "File: player1andbotimg.png")
                    {
                        prison1.Source = player.Image;
                    }
                    else
                    {
                        prison1.Source = null;
                    }
                    break;

                case "prison2":
                    if (prison2.Source != null && prison2.Source.ToString() == "File: player1andbotimg.png")
                    {
                        prison2.Source = player.Image;
                    }
                    else
                    {
                        prison2.Source = null;
                    }
                    break;
                case "betrayal1":
                    if (betrayal1.Source != null && betrayal1.Source.ToString() == "File: player1andbotimg.png")
                    {
                        betrayal1.Source = player.Image;
                    }
                    else
                    {
                        betrayal1.Source = null;
                    }
                    break;
                case "desire1":
                    if (desire1.Source != null && desire1.Source.ToString() == "File: player1andbotimg.png")
                    {
                        desire1.Source = player.Image;
                    }
                    else
                    {
                        desire1.Source = null;
                    }
                    break;
                case "desire2":
                    if (desire2.Source != null && desire2.Source.ToString() == "File: player1andbotimg.png")
                    {
                        desire2.Source = player.Image;
                    }
                    else
                    {
                        desire2.Source = null;
                    }
                    break;
                case "cambions":
                    if (cambions.Source != null && cambions.Source.ToString() == "File: player1andbotimg.png")
                    {
                        cambions.Source = player.Image;
                    }
                    else
                    {
                        cambions.Source = null;
                    }
                    break;
                case "gluttony1":
                    if (gluttony1.Source != null && gluttony1.Source.ToString() == "File: player1andbotimg.png")
                    {
                        gluttony1.Source = player.Image;
                    }
                    else
                    {
                        gluttony1.Source = null;
                    }
                    break;
                case "gluttony2":
                    if (gluttony2.Source != null && gluttony2.Source.ToString() == "File: player1andbotimg.png")
                    {
                        gluttony2.Source = player.Image;
                    }
                    else
                    {
                        gluttony2.Source = null;
                    }
                    break;
                case "betrayal2":
                    if (betrayal2.Source != null && betrayal2.Source.ToString() == "File: player1andbotimg.png")
                    {
                        betrayal2.Source = player.Image;
                    }
                    else
                    {
                        betrayal2.Source = null;
                    }
                    break;
                case "greed1":
                    if (greed1.Source != null && greed1.Source.ToString() == "File: player1andbotimg.png")
                    {
                        greed1.Source = player.Image;
                    }
                    else
                    {
                        greed1.Source = null;
                    }
                    break;
                case "greed2":
                    if (greed2.Source.ToString() == "File: player1andbotimg.png")
                    {
                        greed2.Source = player.Image;
                    }
                    else
                    {
                        greed2.Source = null;
                    }
                    break;
                case "wayToHeven":
                    if (wayToHeven.Source.ToString() == "File: player1andbotimg.png")
                    {
                        wayToHeven.Source = player.Image;
                    }
                    else
                    {
                        wayToHeven.Source = null;
                    }
                    break;
                case "anger1":
                    if (anger1.Source.ToString() == "File: player1andbotimg.png")
                    {
                        anger1.Source = player.Image;
                    }
                    else
                    {
                        anger1.Source = null;
                    }
                    break;
                case "anger2":
                    if (anger2.Source.ToString() == "File: player1andbotimg.png")
                    {
                        anger2.Source = player.Image;
                    }
                    else
                    {
                        anger2.Source = null;
                    }
                    break;
                case "betrayal3":
                    if (betrayal3.Source.ToString() == "File: player1andbotimg.png")
                    {
                        betrayal3.Source = player.Image;
                    }
                    else
                    {
                        betrayal3.Source = null;
                    }
                    break;
                case "heresy1":
                    if (heresy1.Source.ToString() == "File: player1andbotimg.png")
                    {
                        heresy1.Source = player.Image;
                    }
                    else
                    {
                        heresy1.Source = null;
                    }
                    break;
                case "heresy2":
                    if (heresy2.Source.ToString() == "File: player1andbotimg.png")
                    {
                        heresy2.Source = player.Image;
                    }
                    else
                    {
                        heresy2.Source = null;
                    }
                    break;
                case "devil":
                    if (devil.Source.ToString() == "File: player1andbotimg.png")
                    {
                        devil.Source = player.Image;
                    }
                    else
                    {
                        devil.Source = null;
                    }
                    break;
                case "violence1":
                    if (violence1.Source.ToString() == "File: player1andbotimg.png")
                    {
                        violence1.Source = player.Image;
                    }
                    else
                    {
                        violence1.Source = null;
                    }
                    break;
                case "violence2":
                    if (violence2.Source.ToString() == "File: player1andbotimg.png")
                    {
                        violence2.Source = player.Image;
                    }
                    else
                    {
                        violence2.Source = null;
                    }
                    break;
                case "betrayal4":
                    if (betrayal4.Source.ToString() == "File: player1andbotimg.png")
                    {
                        betrayal4.Source = player.Image;
                    }
                    else
                    {
                        betrayal4.Source = null;
                    }
                    break;
                case "extortion1":
                    if (extortion1.Source.ToString() == "File: player1andbotimg.png")
                    {
                        extortion1.Source = player.Image;
                    }
                    else
                    {
                        extortion1.Source = null;
                    }
                    break;
            }
            xdbot = xdbot + r;
            if (xdbot >= 23)
            {
                int help1 = xdbot - 23;
                xdbot = 0 + help1;
                bot.Money = bot.Money + 100;
            }
            switch (fields[xdbot].Name)
            {
                case "Start":
                    if (Start.Source == null)
                    {
                        Start.Source = bot.Image;
                    }
                    else if (Start.Source != null)
                    {
                        Start.Source = "player1andbotimg.png";
                    }
                    break;

                case "prison1":
                    if (prison1.Source == null)
                    {
                        prison1.Source = bot.Image;
                    }
                    else if (prison1.Source != null)
                    {
                        prison1.Source = "player1andbotimg.png";
                    }
                    break;

                case "prison2":
                    if (prison2.Source == null)
                    {
                        prison2.Source = bot.Image;
                    }
                    else if (prison2.Source != null)
                    {
                        prison2.Source = "player1andbotimg.png";
                    }
                    break;
                case "betrayal1":
                    if (betrayal1.Source == null)
                    {
                        betrayal1.Source = bot.Image;
                    }
                    else if (betrayal1.Source != null)
                    {
                        betrayal1.Source = "player1andbotimg.png";
                    }
                    break;
                case "desire1":
                    if (desire1.Source == null)
                    {
                        desire1.Source = bot.Image;
                    }
                    else if (desire1.Source != null)
                    {
                        desire1.Source = "player1andbotimg.png";
                    }
                    break;
                case "desire2":
                    if (desire2.Source == null)
                    {
                        desire2.Source = bot.Image;
                    }
                    else if (desire2.Source != null)
                    {
                        desire2.Source = "player1andbotimg.png";
                    }
                    break;
                case "cambions":
                    if (cambions.Source == null)
                    {
                        cambions.Source = bot.Image;
                    }
                    else if (cambions.Source != null)
                    {
                        cambions.Source = "player1andbotimg.png";
                    }
                    break;
                case "gluttony1":
                    if (gluttony1.Source == null)
                    {
                        gluttony1.Source = bot.Image;
                    }
                    else if (gluttony1.Source != null)
                    {
                        gluttony1.Source = "player1andbotimg.png";
                    }
                    break;
                case "gluttony2":
                    if (gluttony2.Source == null)
                    {
                        gluttony2.Source = bot.Image;
                    }
                    else if (gluttony2.Source != null)
                    {
                        gluttony2.Source = "player1andbotimg.png";
                    }
                    break;
                case "betrayal2":
                    if (betrayal2.Source == null)
                    {
                        betrayal2.Source = bot.Image;
                    }
                    else if (betrayal2.Source != null)
                    {
                        betrayal2.Source = "player1andbotimg.png";
                    }
                    break;
                case "greed1":
                    if (greed1.Source == null)
                    {
                        greed1.Source = bot.Image;
                    }
                    else if (greed1.Source != null)
                    {
                        greed1.Source = "player1andbotimg.png";
                    }
                    break;
                case "greed2":
                    if (greed2.Source == null)
                    {
                        greed2.Source = bot.Image;
                    }
                    else if (greed2.Source != null)
                    {
                        greed2.Source = "player1andbotimg.png";
                    }
                    break;
                case "wayToHeven":
                    if (wayToHeven.Source == null)
                    {
                        wayToHeven.Source = bot.Image;
                    }
                    else if (wayToHeven.Source != null)
                    {
                        wayToHeven.Source = "player1andbotimg.png";
                    }
                    break;
                case "anger1":
                    if (anger1.Source == null)
                    {
                        anger1.Source = bot.Image;
                    }
                    else if (anger1.Source != null)
                    {
                        anger1.Source = "player1andbotimg.png";
                    }
                    break;
                case "anger2":
                    if (anger2.Source == null)
                    {
                        anger2.Source = bot.Image;
                    }
                    else if (anger2.Source != null)
                    {
                        anger2.Source = "player1andbotimg.png";
                    }
                    break;
                case "betrayal3":
                    if (betrayal3.Source == null)
                    {
                        betrayal3.Source = bot.Image;
                    }
                    else if (betrayal3.Source != null)
                    {
                        betrayal3.Source = "player1andbotimg.png";
                    }
                    break;
                case "heresy1":
                    if (heresy1.Source == null)
                    {
                        heresy1.Source = bot.Image;
                    }
                    else if (heresy1.Source != null)
                    {
                        heresy1.Source = "player1andbotimg.png";
                    }
                    break;
                case "heresy2":
                    if (heresy2.Source == null)
                    {
                        heresy2.Source = bot.Image;
                    }
                    else if (heresy2.Source != null)
                    {
                        heresy2.Source = "player1andbotimg.png";
                    }
                    break;
                case "devil":
                    if (devil.Source == null)
                    {
                        devil.Source = bot.Image;
                    }
                    else if (devil.Source != null)
                    {
                        devil.Source = "player1andbotimg.png";
                    }
                    break;
                case "violence1":
                    if (violence1.Source == null)
                    {
                        violence1.Source = bot.Image;
                    }
                    else if (violence1.Source != null)
                    {
                        violence1.Source = "player1andbotimg.png";
                    }
                    break;
                case "violence2":
                    if (violence2.Source == null)
                    {
                        violence2.Source = bot.Image;
                    }
                    else if (violence2.Source != null)
                    {
                        violence2.Source = "player1andbotimg.png";
                    }
                    break;
                case "betrayal4":
                    if (betrayal4.Source == null)
                    {
                        betrayal4.Source = bot.Image;
                    }
                    else if (betrayal4.Source != null)
                    {
                        betrayal4.Source = "player1andbotimg.png";
                    }
                    break;
                case "extortion1":
                    if (extortion1.Source == null)
                    {
                        extortion1.Source = bot.Image;
                    }
                    else if (extortion1.Source != null)
                    {
                        extortion1.Source = "player1andbotimg.png";
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