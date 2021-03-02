using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Game.Enums;
using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    ///     The Main Game Page
    /// </summary>
    public partial class BattlePage : ContentPage
    {

        // Wait time before proceeding
        const int WaitTime = 1500;
        // HTML Formatting for message output box
        readonly HtmlWebViewSource _htmlSource = new HtmlWebViewSource();

        // Hold the Map Objects, for easy access to update them
        readonly Dictionary<string, object> _mapLocationObject = new Dictionary<string, object>();

        // Empty Constructor for UTs
        readonly bool _unitTestSetting;

        protected BattlePage(bool unitTest) =>
            _unitTestSetting = unitTest;

        /// <summary>
        ///     Constructor
        /// </summary>
        public BattlePage()
        {
            InitializeComponent();

            // Set initial State to Starting
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Starting;

            // Set up the UI to Defaults
            BindingContext = BattleEngineViewModel.Instance;

            // Create and Draw the Map
            InitializeMapGrid();

            // Start the Battle Engine
            BattleEngineViewModel.Instance.Engine.StartBattle(false);

            // Set the background image to match the round location
            SetBackgroundImage();

            // Ask the Game engine to select who goes first
            BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(null);

            // Add Players to Display
            //DrawGameAttackerDefenderBoard();

            // Set the Battle Mode
            ShowBattleMode();
        }

        /// <summary>
        ///     Dray the Player Boxes
        /// </summary>
        public void DrawPlayerBoxes()
        {
            // Clear the character list box
            var Characters = CharacterListBox.Children.ToList();
            foreach (var data in Characters) CharacterListBox.Children.Remove(data);

            // Draw the Characters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList
                                                      .Where(m => m.PlayerType == PlayerTypeEnum.Character).ToList())
                CharacterListBox.Children.Add(PlayerInfoDisplayBox(data));

            var monsters = MonsterListBox.Children.ToList();
            foreach (var data in monsters) MonsterListBox.Children.Remove(data);

            // Draw the Monsters
            foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList
                                                      .Where(m => m.PlayerType == PlayerTypeEnum.Monster).ToList())
                MonsterListBox.Children.Add(PlayerInfoDisplayBox(data));

            // Add one blank PlayerInfoDisplayBox to hold space in case the character list is empty
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Count() == 0)
                CharacterListBox.Children.Add(PlayerInfoDisplayBox(null));

            // Add one blank PlayerInfoDisplayBox to hold space in case the monster list is empty
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Count() == 0)
                MonsterListBox.Children.Add(PlayerInfoDisplayBox(null));
        }

        /// <summary>
        ///     Put the Player into a Display Box
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout PlayerInfoDisplayBox(PlayerInfoModel data)
        {
            data ??= new PlayerInfoModel {ImageURI = ""};

            // Hookup the image
            var playerImage = new Image
            {
                Source = data.ImageURI,
                Style = Application.Current.Resources.TryGetValue("PlayerBattleMediumStyle", out var imageStyle)
                            ? (Style)imageStyle
                            : null
            };

            // Put the Image Button and Text inside a layout
            var playerStack = new StackLayout
            {
                //Style = (Style)Application.Current.Resources["PlayerBattleDisplayBox"],
                Style = Application.Current.Resources.TryGetValue("PlayerBattleDisplayBox", out var stackStyle)
                            ? (Style)stackStyle
                            : null,
                Children = {playerImage}
            };

            return playerStack;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ShowBattleMode();
        }

        /// <summary>
        ///     Hide the different button states
        ///     Hide the message display box
        /// </summary>
        public void HideUiElements()
        {
            NextRoundButton.IsVisible = false;
            StartBattleButton.IsVisible = false;
            AttackButton.IsVisible = false;
            //MessageDisplayBox.IsVisible = false;
        }

        /// <summary>
        ///     Show the proper Battle Mode
        /// </summary>
        public void ShowBattleMode()
        {
            // If running in UT mode,
            if (_unitTestSetting) return;

            HideUiElements();

            ClearMessages();

            DrawPlayerBoxes();

            // Update the Mode
            BattleModeValue.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel
                                                        .BattleModeEnum.ToMessage();

            ShowBattleModeDisplay();

            ShowBattleModeUiElements();
        }

        /// <summary>
        ///     Control the UI Elements to display
        /// </summary>
        public void ShowBattleModeUiElements()
        {
            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum)
            {
                case BattleStateEnum.Starting:
                    //GameUIDisplay.IsVisible = false;
                    //AttackerAttack.Source = ActionEnum.Unknown.ToImageURI();
                    StartBattleButton.IsVisible = true;
                    break;

                case BattleStateEnum.NewRound:
                    //UpdateMapGrid();
                    //AttackerAttack.Source = ActionEnum.Unknown.ToImageURI();
                    NextRoundButton.IsVisible = true;
                    break;

                case BattleStateEnum.GameOver:
                    // Hide the Game Board
                    GameUIDisplay.IsVisible = false;
                    //AttackerAttack.Source = ActionEnum.Unknown.ToImageURI();

                    // Show the Game Over Display
                    GameOverDisplay.IsVisible = true;
                    break;

                case BattleStateEnum.RoundOver:
                case BattleStateEnum.Battling:
                    GameUIDisplay.IsVisible = true;
                    //BattlePlayerInfomationBox.IsVisible = true;
                    //MessageDisplayBox.IsVisible = true;
                    AttackButton.IsVisible = true;
                    BlockButton.IsVisible = true;
                    break;

                // Based on the State disable buttons
            }
        }

        /// <summary>
        ///     Control the Map Mode or Simple
        /// </summary>
        public void ShowBattleModeDisplay()
        {
            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum)
            {
                case BattleModeEnum.MapAbility:
                case BattleModeEnum.MapFull:
                case BattleModeEnum.MapNext:
                    //GamePlayersTopDisplay.IsVisible = false;
                    //BattleMapDisplay.IsVisible = true;
                    break;
            }
        }

        #region BattleMapMode

        /// <summary>
        ///     Create the Initial Map Grid
        ///     All locations are empty
        /// </summary>
        /// <returns></returns>
        public bool InitializeMapGrid()
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.ClearMapGrid();

            return true;
        }

        // /// <summary>
        // /// Walk the current grid
        // /// check each cell to see if it matches the engine map
        // /// Update only those that need change
        // /// </summary>
        // /// <returns></returns>
        //public bool UpdateMapGrid()
        //{
        //    foreach (var data in BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.MapGridLocation)
        //    {
        //        // Use the ImageButton from the dictionary because that represents the player object
        //        object MapObject = GetMapGridObject(GetDictionaryImageButtonName(data));
        //        if (MapObject == null)
        //        {
        //            return false;
        //        }

        //        var imageObject = (ImageButton)MapObject;

        //        // Check automation ID on the Image, That should match the Player, if not a match, the cell is now different need to update
        //        if (imageObject.AutomationId.Equals(data.Player.Guid))
        //        {
        //            continue;
        //        }
        //        // The Image is different, so need to re-create the Image Object and add it to the Stack
        //        // That way the correct monster is in the box.

        //        MapObject = GetMapGridObject(GetDictionaryStackName(data));
        //        if (MapObject == null)
        //        {
        //            return false;
        //        }

        //        var stackObject = (StackLayout)MapObject;

        //        // Remove the ImageButton
        //        stackObject.Children.RemoveAt(0);

        //        var PlayerImageButton = DetermineMapImageButton(data);

        //        stackObject.Children.Add(PlayerImageButton);

        //        // Update the Image in the Datastructure
        //        MapGridObjectAddImage(PlayerImageButton, data);

        //        stackObject.BackgroundColor = DetermineMapBackgroundColor(data);
        //    }

        //    return true;
        //}

        /// <summary>
        ///     Convert the Stack to a name for the dictionary to lookup
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static string GetDictionaryFrameName(MapModelLocation data) =>
            $"MapR{data.Row}C{data.Column}Frame";

        /// <summary>
        ///     Convert the Stack to a name for the dictionary to lookup
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static string GetDictionaryStackName(MapModelLocation data) =>
            $"MapR{data.Row}C{data.Column}Stack";

        /// <summary>
        ///     Covert the player map location to a name for the dictionary to lookup
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static string GetDictionaryImageButtonName(MapModelLocation data) =>
            // Look up the Frame in the Dictionary
            $"MapR{data.Row}C{data.Column}ImageButton";

        /// <summary>
        ///     Get the Frame from the Dictionary
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        object GetMapGridObject(string name)
        {
            _mapLocationObject.TryGetValue(name, out var data);
            return data;
        }

        // /// <summary>
        // /// Make the Game Map Frame
        // /// Place the Character or Monster on the frame
        // /// If empty, place Empty
        // /// </summary>
        // /// <param name="mapLocationModel"></param>
        // /// <returns></returns>
        //public Frame MakeMapGridBox(MapModelLocation mapLocationModel)
        //{
        //    if (mapLocationModel.Player == null)
        //    {
        //        mapLocationModel.Player = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.EmptySquare;
        //    }

        //    var PlayerImageButton = DetermineMapImageButton(mapLocationModel);

        //    var PlayerStack = new StackLayout
        //    {
        //        Padding = 0,
        //        Style = Application.Current.Resources.TryGetValue("BattleMapImageBox", out object stackStyle)
        //                    ? (Style)stackStyle
        //                    : null,
        //        HorizontalOptions = LayoutOptions.Center,
        //        VerticalOptions = LayoutOptions.Center,
        //        BackgroundColor = DetermineMapBackgroundColor(mapLocationModel),
        //        Children = {PlayerImageButton},
        //    };

        //    MapGridObjectAddImage(PlayerImageButton, mapLocationModel);
        //    MapGridObjectAddStack(PlayerStack, mapLocationModel);

        //    var MapFrame = new Frame
        //    {
        //        //Style = (Style)Application.Current.Resources["BattleMapFrame"],
        //        Style = Application.Current.Resources.TryGetValue("BattleMapFrame", out object frameStyle)
        //                    ? (Style)frameStyle
        //                    : null,
        //        Content = PlayerStack,
        //        AutomationId = GetDictionaryFrameName(mapLocationModel)
        //    };

        //    return MapFrame;
        //}

        // /// <summary>
        // /// This add the ImageButton to the stack to kep track of
        // /// </summary>
        // /// <param name="data"></param>
        // /// <param name="mapModel"></param>
        // /// <returns></returns>
        //private bool MapGridObjectAddImage(ImageButton data, MapModelLocation mapModel)
        //{
        //    var name = GetDictionaryImageButtonName(mapModel);

        //    // First check to see if it has data, if so update rather than add
        //    if (MapLocationObject.ContainsKey(name))
        //    {
        //        // Update it
        //        MapLocationObject[name] = data;
        //        return true;
        //    }

        //    MapLocationObject.Add(name, data);

        //    return true;
        //}

        // /// <summary>
        // /// This adds the Stack into the Dictionary to keep track of
        // /// </summary>
        // /// <param name="data"></param>
        // /// <param name="mapModel"></param>
        // /// <returns></returns>
        //private bool MapGridObjectAddStack(StackLayout data, MapModelLocation mapModel)
        //{
        //    var name = GetDictionaryStackName(mapModel);

        //    // First check to see if it has data, if so update rather than add
        //    if (MapLocationObject.ContainsKey(name))
        //    {
        //        // Update it
        //        MapLocationObject[name] = data;
        //        return true;
        //    }

        //    MapLocationObject.Add(name, data);
        //    return true;
        //}

        // /// <summary>
        // /// Set the Image onto the map
        // /// The Image represents the player
        // ///
        // /// So a charcter is the character Image for that character
        // ///
        // /// The Automation ID equals the guid for the player
        // /// This makes it easier to identify when checking the map to update thigns
        // ///
        // /// The button action is set per the type, so Characters events are differnt than monster events
        // /// </summary>
        // /// <param name="mapLocationModel"></param>
        // /// <returns></returns>
        //private ImageButton DetermineMapImageButton(MapModelLocation mapLocationModel)
        //{
        //    var data = new ImageButton
        //    {
        //        Style = Application.Current.Resources.TryGetValue("ImageMediumStyle", out object imageStyle)
        //                    ? (Style)imageStyle
        //                    : null,
        //        Source = mapLocationModel.Player.ImageURI,

        //        // Store the guid to identify this button
        //        AutomationId = mapLocationModel.Player.Guid
        //    };

        //    switch (mapLocationModel.Player.PlayerType)
        //    {
        //        case PlayerTypeEnum.Character:
        //            data.Clicked += (sender, args) => SetSelectedCharacter(mapLocationModel);
        //            break;
        //        case PlayerTypeEnum.Monster:
        //            data.Clicked += (sender, args) => SetSelectedMonster(mapLocationModel);
        //            break;
        //        case PlayerTypeEnum.Unknown:
        //            break;
        //        default:
        //            data.Clicked += (sender, args) => SetSelectedEmpty(mapLocationModel);

        //            // Use the blank cell
        //            data.Source = BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.EmptySquare.ImageURI;
        //            break;
        //    }

        //    return data;
        //}

        /// <summary>
        ///     Set the Background color for the tile.
        ///     Monsters and Characters have different colors
        ///     Empty cells are transparent
        /// </summary>
        /// <param name="mapModel"></param>
        /// <returns></returns>
        static Color DetermineMapBackgroundColor(MapModelLocation mapModel)
        {
            string battleMapBackgroundColor = null;
            switch (mapModel.Player.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    battleMapBackgroundColor = "BattleMapCharacterColor";
                    break;
                case PlayerTypeEnum.Monster:
                    battleMapBackgroundColor = "BattleMapMonsterColor";
                    break;
                case PlayerTypeEnum.Unknown:
                    break;
                default:
                    battleMapBackgroundColor = "BattleMapTransparentColor";
                    break;
            }

            return Application.Current.Resources.TryGetValue(battleMapBackgroundColor ?? string.Empty, out var val)
                       ? (Color)val
                       : Color.Transparent;
        }

        #region MapEvents

        /// <summary>
        ///     Event when an empty location is clicked on
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetSelectedEmpty(MapModelLocation data)
        {
            // TODO: Info
            /*
             * This gets called when the characters is clicked on
             * Usefull if you want to select the location to move to etc.
             *
             * For Mike's simple battle grammar there is no selection of action so I just return true
             */
            return true;
        }

        /// <summary>
        ///     Event when a Monster is clicked on
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetSelectedMonster(MapModelLocation data)
        {
            // TODO: Info

            /*
             * This gets called when the Monster is clicked on
             * Usefull if you want to select the monster to attack etc.
             *
             * For Mike's simple battle grammar there is no selection of action so I just return true
             */

            data.IsSelectedTarget = true;
            return true;
        }

        /// <summary>
        ///     Event when a Character is clicked on
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool SetSelectedCharacter(MapModelLocation data)
        {
            // TODO: Info
            /*
             * This gets called when the characters is clicked on
             * Usefull if you want to select the character and then set state or do something
             *
             * For Mike's simple battle grammar there is no selection of action so I just return true
             */
            return true;
        }

        #endregion MapEvents

        #endregion BattleMapMode

        #region BasicBattleMode

        /// <summary>
        ///     Attack Action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AttackButton_Clicked(object sender, EventArgs e)
        {
            NextAttackExample();
        }

        /// <summary>
        ///     Settings Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Settings_Clicked(object sender, EventArgs e) => await ShowBattleSettingsPage();

        /// <summary>
        ///     Next Attack Example
        ///     This code example follows the rule of
        ///     Auto Select Attacker
        ///     Auto Select Defender
        ///     Do the Attack and show the result
        ///     So the pattern is Click Next, Next, Next until game is over
        /// </summary>
        public void NextAttackExample()
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            // Get the turn, set the current player and attacker to match
            SetAttackerAndDefender();

            // Hold the current state
            var roundCondition = BattleEngineViewModel.Instance.Engine.Round.RoundNextTurn();

            // Output the Message of what happened.
            GameMessage();

            // Show the outcome on the Board
            //DrawGameAttackerDefenderBoard();

            switch (roundCondition)
            {
                case RoundEnum.NewRound:
                    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.NewRound;

                    // Pause
                    Task.Delay(WaitTime);

                    Debug.WriteLine("New Round");

                    // Show the Round Over, after that is cleared, it will show the New Round Dialog
                    ShowModalRoundOverPage();
                    return;
                // Check for Game Over
                case RoundEnum.GameOver:
                    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.GameOver;

                    // Wrap up
                    BattleEngineViewModel.Instance.Engine.EndBattle();

                    // Pause
                    Task.Delay(WaitTime);

                    Debug.WriteLine("Game Over");

                    GameOver();
                    return;
                case RoundEnum.Unknown:
                    break;
                case RoundEnum.NextTurn:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///     Decide The Turn and who to Attack
        /// </summary>
        public void SetAttackerAndDefender()
        {
            BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(BattleEngineViewModel.Instance.Engine.Round
                                                                               .GetNextPlayerTurn());

            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    // User would select who to attack

                    // for now just auto selecting
                    BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(BattleEngineViewModel.Instance.Engine
                        .Round.Turn.AttackChoice(BattleEngineViewModel.Instance.Engine.EngineSettings
                                                                      .CurrentAttacker));
                    break;

                case PlayerTypeEnum.Unknown:
                    break;
                default:

                    // Monsters turn, so auto pick a Character to Attack
                    BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(BattleEngineViewModel.Instance.Engine
                        .Round.Turn.AttackChoice(BattleEngineViewModel.Instance.Engine.EngineSettings
                                                                      .CurrentAttacker));
                    break;
            }
        }

        /// <summary>
        ///     Game is over
        ///     Show Buttons
        ///     Clean up the Engine
        ///     Show the Score
        ///     Clear the Board
        /// </summary>
        public void GameOver()
        {
            // Save the Score to the Score View Model, by sending a message to it.
            var Score = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore;
            MessagingCenter.Send(this, "AddData", Score);

            ShowBattleMode();
        }

        #endregion BasicBattleMode

        #region MessageHandelers

        /// <summary>
        ///     Builds up the output message
        /// </summary>
        public void GameMessage()
        {
            // Output The Message that happened.
            //BattleMessages.Text =
            //$"{BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.TurnMessage} \n{BattleMessages.Text}";

            //Debug.WriteLine(BattleMessages.Text);

            if (!string.IsNullOrEmpty(BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel
                                                           .LevelUpMessage))
            {
                //BattleMessages.Text =
                //$"{BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.LevelUpMessage} \n{BattleMessages.Text}";
            }

            //htmlSource.Html = BattleEngineViewModel.Instance.Engine.BattleMessagesModel.GetHTMLFormattedTurnMessage();
            //HtmlBox.Source = HtmlBox.Source = htmlSource;
        }

        /// <summary>
        ///     Clears the messages on the UX
        /// </summary>
        public void ClearMessages()
        {
            //BattleMessages.Text = "";
            _htmlSource.Html = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel
                                                    .GetHtmlBlankMessage();
            //HtmlBox.Source = htmlSource;
        }

        #endregion MessageHandelers
        #region PageHandelers

        /// <summary>
        ///     Battle Over, so Exit Button
        ///     Need to show this for the user to click on.
        ///     The Quit does a prompt, exit just exits
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ExitButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        /// <summary>
        ///     The Next Round Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void NextRoundButton_Clicked(object sender, EventArgs e)
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;
            ShowBattleMode();
            await Navigation.PushModalAsync(new NewRoundPage());
        }

        /// <summary>
        ///     The Start Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void StartButton_Clicked(object sender, EventArgs e)
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            ShowBattleMode();
            await Navigation.PushModalAsync(new NewRoundPage());
        }

        /// <summary>
        ///     Show the Game Over Screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public async void ShowScoreButton_Clicked(object sender, EventArgs args)
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new ScorePage());
        }

        /// <summary>
        ///     Show the Round Over page
        ///     Round Over is where characters get items
        /// </summary>
        public async void ShowModalRoundOverPage()
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new RoundOverPage());
        }

        /// <summary>
        ///     Show Settings
        /// </summary>
        public async Task ShowBattleSettingsPage()
        {
            ShowBattleMode();
            await Navigation.PushModalAsync(new BattleSettingsPage());
        }

        public string SetBackgroundImage()
        {
            var roundLocation = BattleEngineViewModel.Instance.Engine.Round.RoundLocation;
            var imageUri = roundLocation.ToImageUri();
            ContentPageElement.BackgroundImageSource = imageUri;

            return imageUri;
        }

        #endregion PageHandelers
    }
}
