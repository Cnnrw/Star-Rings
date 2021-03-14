using Game.Engine.EngineGame;
using Game.Enums;
using Game.Models;
using Game.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        //readonly Dictionary<string, object> _mapLocationObject = new Dictionary<string, object>();

        // Keep track of the Player Figurines on the battlefield
        Dictionary<string, StackLayout> PlayerFigures = new Dictionary<string, StackLayout>();

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

            // Start the Battle Engine
            BattleEngineViewModel.Instance.Engine.StartBattle(false);

            EnterNewRound();
        }

        /// <summary>
        /// Update the page to display entry into a new Round
        /// Hides the UI, updates the location background, and shows a button to enter 
        /// </summary>
        public void EnterNewRound()
        {
            // Set new round location
            var GameRound = (RoundEngine)BattleEngineViewModel.Instance.Engine.Round;
            GameRound.ChooseRoundLocation();

            // Hide battle UI
            HideBattleUIElements();

            // Set the background image
            BattleLocationEnum roundLocation = BattleEngineViewModel.Instance.Engine.Round.RoundLocation;
            string imageUri = roundLocation.ToImageUri();
            ContentPageElement.BackgroundImageSource = imageUri;

            // Update the Start Round button
            StartBattleButton.IsVisible = true;
            StartBattleButton.Text = "Explore " + BattleEngineViewModel.Instance.Engine.Round.RoundLocation.ToMessageWithArticle();
        }

        /// <summary>
        /// Hides the Monster, Character, and message boxes
        /// </summary>
        public void HideBattleUIElements()
        {
            BattleBottomBox.IsVisible = false;
            TopMonstersDisplay.IsVisible = false;
        }

        /// <summary>
        /// Shows the Monster, Character, and message boxes
        /// </summary>
        public void ShowBattleUIElements()
        {
            BattleBottomBox.IsVisible = true;
            TopMonstersDisplay.IsVisible = true;
        }

        /// <summary>
        /// Draws the Player figures
        /// </summary>
        public void DrawPlayerFigures()
        {
            PlayerFigures.Clear();

            // Clear the Character figure area
            var CharacterFigures = CharacterFigureArea.Children.ToList();
            foreach (var Figure in CharacterFigures)
            {
                CharacterFigureArea.Children.Remove(Figure);
            }

            // Clear the Monster figure area
            var MonsterFigures = MonsterFigureArea.Children.ToList();
            foreach (var Figure in MonsterFigures)
            {
                MonsterFigureArea.Children.Remove(Figure);
            }

            // Draw the Character figures
            foreach (var Player in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList
                .Where(m => m.PlayerType == PlayerTypeEnum.Character).ToList())
            {
                var PlayerFigure = CreatePlayerFigure(Player);
                PlayerFigures.Add(Player.Guid, PlayerFigure);
                CharacterFigureArea.Children.Add(PlayerFigure);
            }

            // Draw the Monster figures
            foreach (var Player in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList
                .Where(m => m.PlayerType == PlayerTypeEnum.Monster).ToList())
            {
                var PlayerFigure = CreatePlayerFigure(Player);
                PlayerFigures.Add(Player.Guid, PlayerFigure);
                MonsterFigureArea.Children.Add(PlayerFigure);
            }

            // Add one blank Figure to hold space in case the Character list is empty
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.CharacterList.Count() == 0)
            {
                CharacterFigureArea.Children.Add(CreatePlayerFigure(null));
            }

            // Add one blank Figure to hold space in case the Monster list is empty
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.MonsterList.Count() == 0)
            {
                MonsterFigureArea.Children.Add(CreatePlayerFigure(null));
            }
        }

        /// <summary>
        /// Creates a Player figure
        /// </summary>
        /// <param name="Player">The Player to make the figure for</param>
        /// <returns></returns>
        public StackLayout CreatePlayerFigure(PlayerInfoModel Player)
        {
            Player ??= new PlayerInfoModel {
                Name = "",
                ImageURI = ""
            };

            var PlayerFigureImageButton = new ImageButton
            {
                Source = Player.ImageURI,
                WidthRequest = 100,
                HeightRequest = 100,
                Aspect = Aspect.AspectFit,
                BindingContext = Player.Guid
            };
            PlayerFigureImageButton.Clicked += FigureButton_Clicked;

            var PlayerFigureLabel = new Label
            {
                Text = Player.Name,
                HorizontalTextAlignment = TextAlignment.Center
            };

            var PlayerFigureStackLayout = new StackLayout
            {
                Children =
                {
                    PlayerFigureImageButton,
                    PlayerFigureLabel
                }
            };

            return PlayerFigureStackLayout;
        }

        public void ClearUi()
        {
            // Clear Player details boxes
            ClearPlayerDetailsBoxes();

            // Redraw figures
            DrawPlayerFigures();

            // Clear battle message
            BattleMessages.Text = "";

            // Hide action buttons
            AttackButton.IsVisible = false;
            BlockButton.IsVisible = false;
            NextButton.IsVisible = false;
        }

        ///// <summary>
        ///// Pust the Player into a Display Box
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public StackLayout PlayerInfoDisplayBox(PlayerInfoModel data)
        //{
        //    data ??= new PlayerInfoModel {ImageURI = ""};

        //    // Hookup the image
        //    var playerImage = new Image
        //    {
        //        Source = data.ImageURI,
        //        Style = Application.Current.Resources.TryGetValue("PlayerBattleMediumStyle", out var imageStyle)
        //                    ? (Style)imageStyle
        //                    : null
        //    };

        //    // Put the Image Button and Text inside a layout
        //    var playerStack = new StackLayout
        //    {
        //        //Style = (Style)Application.Current.Resources["PlayerBattleDisplayBox"],
        //        Style = Application.Current.Resources.TryGetValue("PlayerBattleDisplayBox", out var stackStyle)
        //                    ? (Style)stackStyle
        //                    : null,
        //        Children = {playerImage}
        //    };

        //    return playerStack;
        //}

        /// <summary>
        /// Behavior just before the page appears
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            //ShowBattleMode();
        }

        /// <summary>
        ///     Hide the different button states
        ///     Hide the message display box
        /// </summary>
        public void HideUiElements()
        {
            StartBattleButton.IsVisible = false;
            AttackButton.IsVisible = false;
            //MessageDisplayBox.IsVisible = false;
        }

        /// <summary>
        /// Shows the proper Battle Mode
        /// </summary>
        public void ShowBattleMode()
        {
            // If running in UT mode,
            if (_unitTestSetting) return;

            HideUiElements();

            ClearMessages();

            DrawPlayerFigures();

            // Update the Mode
            BattleModeValue.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel
                .BattleModeEnum.ToMessage();

            //ShowBattleModeDisplay();

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
                    //NextRoundButton.IsVisible = true;
                    StartBattleButton.IsVisible = true;
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
        /// Updates the information displayed in a given Player's details box.
        /// </summary>
        /// <param name="Player"></param>
        public void UpdatePlayerDetailsBox(PlayerInfoModel Player)
        {
            // Update either the Character details box or the Monster details box
            switch (Player.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    SelectedCharacterIconImage.Source = Player.IconImageURI;
                    SelectedCharacterNameLabel.Text = Player.Name;
                    SelectedCharacterLevelLabel.Text = "Level: " + Player.Level;
                    SelectedCharacterHealthLabel.Text = "HP: " + Player.CurrentHealth;
                    SelectedCharacterAttackLabel.Text = "ATK: " + Player.Attack;
                    SelectedCharacterDefenseLabel.Text = "DEF: " + Player.Defense;
                    SelectedCharacterSpeedLabel.Text = "SPD: " + Player.Speed;
                    break;

                case PlayerTypeEnum.Monster:
                default:
                    SelectedMonsterIconImage.Source = Player.IconImageURI;
                    SelectedMonsterNameLabel.Text = Player.Name;
                    SelectedMonsterLevelLabel.Text = "Level: " + Player.Level;
                    SelectedMonsterHealthLabel.Text = "HP: " + Player.CurrentHealth;
                    SelectedMonsterAttackLabel.Text = "ATK: " + Player.Attack;
                    SelectedMonsterDefenseLabel.Text = "DEF: " + Player.Defense;
                    SelectedMonsterSpeedLabel.Text = "SPD: " + Player.Speed;
                    break;
            }
        }

        public void ClearPlayerDetailsBoxes()
        {
            SelectedCharacterIconImage.Source = "";
            SelectedCharacterNameLabel.Text = "";
            SelectedCharacterLevelLabel.Text = "";
            SelectedCharacterHealthLabel.Text = "";
            SelectedCharacterAttackLabel.Text = "";
            SelectedCharacterDefenseLabel.Text = "";
            SelectedCharacterSpeedLabel.Text = "";

            SelectedMonsterIconImage.Source = "";
            SelectedMonsterNameLabel.Text = "";
            SelectedMonsterLevelLabel.Text = "";
            SelectedMonsterHealthLabel.Text = "";
            SelectedMonsterAttackLabel.Text = "";
            SelectedMonsterDefenseLabel.Text = "";
            SelectedMonsterSpeedLabel.Text = "";
        }

        ///// <summary>
        /////     Control the Map Mode or Simple
        ///// </summary>
        //public void ShowBattleModeDisplay()
        //{
        //    switch (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleSettingsModel.BattleModeEnum)
        //    {
        //        case BattleModeEnum.MapAbility:
        //        case BattleModeEnum.MapFull:
        //        case BattleModeEnum.MapNext:
        //            //GamePlayersTopDisplay.IsVisible = false;
        //            //BattleMapDisplay.IsVisible = true;
        //            break;
        //    }
        //}

        #region BattleMapMode

        ///// <summary>
        /////     Create the Initial Map Grid
        /////     All locations are empty
        ///// </summary>
        ///// <returns></returns>
        //public bool InitializeMapGrid()
        //{
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.MapModel.ClearMapGrid();

        //    return true;
        //}

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

        ///// <summary>
        /////     Convert the Stack to a name for the dictionary to lookup
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //static string GetDictionaryFrameName(MapModelLocation data) =>
        //    $"MapR{data.Row}C{data.Column}Frame";

        ///// <summary>
        /////     Convert the Stack to a name for the dictionary to lookup
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //static string GetDictionaryStackName(MapModelLocation data) =>
        //    $"MapR{data.Row}C{data.Column}Stack";

        ///// <summary>
        /////     Covert the player map location to a name for the dictionary to lookup
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //static string GetDictionaryImageButtonName(MapModelLocation data) =>
        //    // Look up the Frame in the Dictionary
        //    $"MapR{data.Row}C{data.Column}ImageButton";

        ///// <summary>
        /////     Get the Frame from the Dictionary
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //object GetMapGridObject(string name)
        //{
        //    _mapLocationObject.TryGetValue(name, out var data);
        //    return data;
        //}

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

        ///// <summary>
        /////     Set the Background color for the tile.
        /////     Monsters and Characters have different colors
        /////     Empty cells are transparent
        ///// </summary>
        ///// <param name="mapModel"></param>
        ///// <returns></returns>
        //internal static Color DetermineMapBackgroundColor(MapModelLocation mapModel)
        //{
        //    string battleMapBackgroundColor = null;
        //    switch (mapModel.Player.PlayerType)
        //    {
        //        case PlayerTypeEnum.Character:
        //            battleMapBackgroundColor = "BattleMapCharacterColor";
        //            break;
        //        case PlayerTypeEnum.Monster:
        //            battleMapBackgroundColor = "BattleMapMonsterColor";
        //            break;
        //        case PlayerTypeEnum.Unknown:
        //            break;
        //        default:
        //            battleMapBackgroundColor = "BattleMapTransparentColor";
        //            break;
        //    }

        //    return Application.Current.Resources.TryGetValue(battleMapBackgroundColor ?? string.Empty, out var val)
        //               ? (Color)val
        //               : Color.Transparent;
        //}

        #region MapEvents

        ///// <summary>
        /////     Event when an empty location is clicked on
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public bool SetSelectedEmpty(MapModelLocation data)
        //{
        //    // TODO: Info
        //    /*
        //     * This gets called when the characters is clicked on
        //     * Usefull if you want to select the location to move to etc.
        //     *
        //     * For Mike's simple battle grammar there is no selection of action so I just return true
        //     */
        //    return true;
        //}

        ///// <summary>
        /////     Event when a Monster is clicked on
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public bool SetSelectedMonster(MapModelLocation data)
        //{
        //    // TODO: Info

        //    /*
        //     * This gets called when the Monster is clicked on
        //     * Usefull if you want to select the monster to attack etc.
        //     *
        //     * For Mike's simple battle grammar there is no selection of action so I just return true
        //     */

        //    data.IsSelectedTarget = true;
        //    return true;
        //}

        ///// <summary>
        /////     Event when a Character is clicked on
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public bool SetSelectedCharacter(MapModelLocation data)
        //{
        //    // TODO: Info
        //    /*
        //     * This gets called when the characters is clicked on
        //     * Usefull if you want to select the character and then set state or do something
        //     *
        //     * For Mike's simple battle grammar there is no selection of action so I just return true
        //     */
        //    return true;
        //}

        #endregion MapEvents

        #endregion BattleMapMode

        #region BasicBattleMode

        /// <summary>
        /// Set Character attack action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AttackButton_Clicked(object sender, EventArgs e)
        {
            PlayerInfoModel CurrentPlayer = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;

            BattleMessages.Text = "Who should " + CurrentPlayer.Name + " try to attack?";

            // Update battle state and current action
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.ChoosingMonsterTarget;
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Attack;

            // Hide action buttons
            AttackButton.IsVisible = false;
            BlockButton.IsVisible = false;
        }

        /// <summary>
        /// Set Character block action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BlockButton_Clicked(object sender, EventArgs e)
        {
            PlayerInfoModel CurrentPlayer = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;

            BattleMessages.Text = "Who should " + CurrentPlayer.Name + " try to block?";

            // Update battle state and current action
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.ChoosingMonsterTarget;
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Block;

            // Hide action buttons
            AttackButton.IsVisible = false;
            BlockButton.IsVisible = false;
        }

        /// <summary>
        /// Settings Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void Settings_Clicked(object sender, EventArgs e) => await ShowBattleSettingsPage();

        public void FigureButton_Clicked(object sender, EventArgs e)
        {
            // Ignore selection if it's not time to choose
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum != BattleStateEnum.ChoosingMonsterTarget)
            {
                return;
            }

            // Set the selected Monster as the target
            string TargetPlayerGuid = ((ImageButton)sender).BindingContext as string;
            PlayerInfoModel TargetMonster = BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Find(m => m.Guid == TargetPlayerGuid);

            // Don't allow the user to select a Character target
            if (TargetMonster.PlayerType == PlayerTypeEnum.Character)
            {
                BattleMessages.Text = "Hey, " + TargetMonster.Name + " is on your side! Pick a monster!";
                return;
            }

            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender = TargetMonster;

            // Update the Monster details box
            UpdatePlayerDetailsBox(TargetMonster);

            // Highlight their figure
            StackLayout TargetPlayerFigure = PlayerFigures[TargetPlayerGuid];
            TargetPlayerFigure.BackgroundColor = Color.FromHex("#88ff6666");

            DoCharacterTurn();

            // Show next button
            NextButton.IsVisible = true;
            NextButton.IsEnabled = true;
        }

        ///// <summary>
        /////     Next Attack Example
        /////     This code example follows the rule of
        /////     Auto Select Attacker
        /////     Auto Select Defender
        /////     Do the Attack and show the result
        /////     So the pattern is Click Next, Next, Next until game is over
        ///// </summary>
        //public void NextAttackExample()
        //{
        //    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

        //    // Get the turn, set the current player and attacker to match
        //    SetAttackerAndDefender();

        //    // Hold the current state
        //    var roundCondition = BattleEngineViewModel.Instance.Engine.Round.RoundNextTurn();

        //    // Output the Message of what happened.
        //    GameMessage();

        //    // Show the outcome on the Board
        //    //DrawGameAttackerDefenderBoard();

        //    switch (roundCondition)
        //    {
        //        case RoundEnum.NewRound:
        //            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.NewRound;

        //            // Pause
        //            Task.Delay(WaitTime);

        //            Debug.WriteLine("New Round");

        //            // Show the Round Over, after that is cleared, it will show the New Round Dialog
        //            ShowModalRoundOverPage();
        //            return;
        //        // Check for Game Over
        //        case RoundEnum.GameOver:
        //            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.GameOver;

        //            // Wrap up
        //            BattleEngineViewModel.Instance.Engine.EndBattle();

        //            // Pause
        //            Task.Delay(WaitTime);

        //            Debug.WriteLine("Game Over");

        //            GameOver();
        //            return;
        //        case RoundEnum.Unknown:
        //            break;
        //        case RoundEnum.NextTurn:
        //            break;
        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }
        //}

        ///// <summary>
        /////     Decide The Turn and who to Attack
        ///// </summary>
        //public void SetAttackerAndDefender()
        //{
        //    BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(BattleEngineViewModel.Instance.Engine.Round
        //                                                                       .GetNextPlayerTurn());

        //    switch (BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker.PlayerType)
        //    {
        //        case PlayerTypeEnum.Character:
        //            // User would select who to attack

        //            // for now just auto selecting
        //            BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(BattleEngineViewModel.Instance.Engine
        //                .Round.Turn.AttackChoice(BattleEngineViewModel.Instance.Engine.EngineSettings
        //                                                              .CurrentAttacker));
        //            break;

        //        case PlayerTypeEnum.Unknown:
        //            break;
        //        default:

        //            // Monsters turn, so auto pick a Character to Attack
        //            BattleEngineViewModel.Instance.Engine.Round.SetCurrentDefender(BattleEngineViewModel.Instance.Engine
        //                .Round.Turn.AttackChoice(BattleEngineViewModel.Instance.Engine.EngineSettings
        //                                                              .CurrentAttacker));
        //            break;
        //    }
        //}

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
        public void NextRoundButton_Clicked(object sender, EventArgs e)
        {
            //BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;
            //ShowBattleMode();
            //await Navigation.PushModalAsync(new NewRoundPage());
            //StartRound();
        }

        /// <summary>
        /// The Start Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void StartButton_Clicked(object sender, EventArgs e)
        {
            // Open a new round page
            await Navigation.PushModalAsync(new NewRoundPage());

            // Set battle state to battling
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            // Show UI
            ShowBattleMode();
            ShowBattleUIElements();

            // Start the first turn
            StartTurn();
        }

        public void NextButton_Clicked(object sender, EventArgs e)
        {
            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum)
            {
                case BattleStateEnum.StartingMonsterTurn:
                    DoMonsterTurn();
                    break;
                case BattleStateEnum.EndingCharacterTurn:
                case BattleStateEnum.EndingMonsterTurn:
                    EndTurn();
                    break;
                default:
                    break;
            }
        }

        public void DoCharacterTurn()
        {
             PlayerInfoModel ActivePlayer = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;
            BattleEngineViewModel.Instance.Engine.Round.Turn.TakeTurn(ActivePlayer);

            PlayerInfoModel TargetCharacter = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender;

            // Highlight the targeted Character's figure
            UpdatePlayerDetailsBox(TargetCharacter);

            // Update battle messages
            BattleMessages.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.TurnMessage;

            // Set battle state
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.EndingMonsterTurn;
        }

        public void DoMonsterTurn()
        {
            PlayerInfoModel ActiveMonster = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;
            BattleEngineViewModel.Instance.Engine.Round.Turn.TakeTurn(ActiveMonster);

            // Choose which Character to target
            //PlayerInfoModel TargetCharacter = BattleEngineViewModel.Instance.Engine.Round.Turn.SelectCharacterToAttack();
            PlayerInfoModel TargetCharacter = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender;

            // Highlight the targeted Character's figure
            UpdatePlayerDetailsBox(TargetCharacter);

            // Highlight their figure
            StackLayout CurrentPlayerFigure = PlayerFigures[TargetCharacter.Guid];
            CurrentPlayerFigure.BackgroundColor = Color.FromHex("#88ff6666");

            // Update battle messages
            BattleMessages.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.TurnMessage;

            // Set battle state
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.EndingMonsterTurn;
        }

        /// <summary>
        /// Ends turn.
        /// </summary>
        public void EndTurn()
        {
            var RoundCondition = BattleEngineViewModel.Instance.Engine.Round.RoundNextTurn();

            if (RoundCondition == RoundEnum.NewRound)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.NewRound;

                // Show the Round Over, after that is cleared, it will show the New Round Dialog
                ShowModalRoundOverPage();

                EnterNewRound();

                return;
            }

            // Check for Game Over
            if (RoundCondition == RoundEnum.GameOver)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.GameOver;

                // Wrap up
                BattleEngineViewModel.Instance.Engine.EndBattle();

                // Pause
                Task.Delay(WaitTime);

                Debug.WriteLine("Game Over");

                GameOver();
                return;
            }

            if (RoundCondition == RoundEnum.NextTurn)
            {
                StartTurn();
            }
        }

        /// <summary>
        /// Determines the next Player and lets them act.
        /// </summary>
        public void StartTurn()
        {
            ClearUi();

            // Determine the current Player
            PlayerInfoModel CurrentPlayer = BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn();
            BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(CurrentPlayer);

            // Show their details in the corresponding details box
            UpdatePlayerDetailsBox(CurrentPlayer);

            // Highlight their figure
            StackLayout CurrentPlayerFigure = PlayerFigures[CurrentPlayer.Guid];
            CurrentPlayerFigure.BackgroundColor = Color.FromHex("#88a6cc7e");

            // A Character's turn action is chosen by the game player. A Monster's action is chosen automatically
            if (CurrentPlayer.PlayerType == PlayerTypeEnum.Character)
            {
                StartCharacterTurn();
            } else
            {
                StartMonsterTurn();
            }
        }

        /// <summary>
        /// Start's a Character's turn.
        /// </summary>
        public void StartCharacterTurn()
        {
            PlayerInfoModel ActiveCharacter = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;

            // Show battle message stating whose turn it is
            string BattleMessage = "It's " + ActiveCharacter.Name + "'s turn! What should they do?";
            BattleMessages.Text = BattleMessage;

            // Set the BattleState to ChoosingTarget
            //BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.ChoosingMonsterTarget;

            // Show/Enable action buttons
            AttackButton.IsVisible = true;
            BlockButton.IsVisible = true;

            AttackButton.IsEnabled = true;
            BlockButton.IsEnabled = true;
        }

        /// <summary>
        /// Starts a Monster's turn.
        /// </summary>
        public void StartMonsterTurn()
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.StartingMonsterTurn;

            PlayerInfoModel ActiveMonster = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;

            // Show battle message stating whose turn it is
            string BattleMessage = "It's " + ActiveMonster.Name + "'s turn! Watch out!";
            BattleMessages.Text = BattleMessage;

            NextButton.IsVisible = true;
            NextButton.IsEnabled = true;
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

        #endregion PageHandelers
    }
}
