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
            //var GameRound = (RoundEngine)BattleEngineViewModel.Instance.Engine.Round;
            //GameRound.ChooseRoundLocation();

            // Hide battle UI
            HideBattleUIElements();

            // Set the background image
            BattleLocationEnum roundLocation = BattleEngineViewModel.Instance.Engine.EngineSettings.RoundLocation;
            string imageUri = roundLocation.ToImageUri();
            ContentPageElement.BackgroundImageSource = imageUri;

            Debug.WriteLine(imageUri);

            // Update the Start Round button
            StartBattleButton.IsVisible = true;
            StartBattleButton.Text = "Explore " + BattleEngineViewModel.Instance.Engine.EngineSettings.RoundLocation.ToMessageWithArticle();
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

        /// <summary>
        /// Behavior just before the page appears
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();
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
            // TODO: Should use this instead of manually calling TakeTurn
            var RoundCondition = BattleEngineViewModel.Instance.Engine.Round.RoundNextTurn();

            if (RoundCondition == RoundEnum.NewRound)
            {
                BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.NewRound;

                // Show the Round Over, after that is cleared, it will show the New Round Dialog
                ShowModalRoundOverPage();

                // Reset to a new Round
                BattleEngineViewModel.Instance.Engine.Round.NewRound();

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
