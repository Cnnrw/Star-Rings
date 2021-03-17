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
    public partial class BattlePage : BaseContentPage
    {
        // Wait time before proceeding
        const int WaitTime = 1500;
        // HTML Formatting for message output box
        readonly HtmlWebViewSource _htmlSource = new HtmlWebViewSource();

        // Keep track of the Player Figurines on the battlefield
        readonly Dictionary<string, StackLayout> _playerFigures = new Dictionary<string, StackLayout>();

        // Empty Constructor for UTs
        readonly bool _unitTestSetting;

        internal BattlePage(bool unitTest) =>
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
        ///     Update the page to display entry into a new Round
        ///     Hides the UI, updates the location background, and shows a button to enter
        /// </summary>
        public void EnterNewRound()
        {
            // Set new round location
            //var GameRound = (RoundEngine)BattleEngineViewModel.Instance.Engine.Round;
            //GameRound.ChooseRoundLocation();

            // Hide battle UI
            HideBattleUiElements();

            // Set the background image
            var roundLocation = BattleEngineViewModel.Instance.Engine.EngineSettings.RoundLocation;
            PageBackground = roundLocation.ToImageURI();

            Debug.WriteLine(PageBackground);

            // Show the Start Round button
            StartRoundButton.IsVisible = true;
            StartRoundButton.Text = $"Explore {BattleEngineViewModel.Instance.Engine.EngineSettings.RoundLocation.ToMessageWithArticle()}";

            // Hide the End Round button
            EndRoundButton.IsVisible = false;
            EndRoundButton.Text = $"Leave {BattleEngineViewModel.Instance.Engine.EngineSettings.RoundLocation.ToMessageWithArticle()}";
        }

        /// <summary>
        ///     Hides the Monster, Character, and message boxes
        /// </summary>
        public void HideBattleUiElements()
        {
            BattleBottomBox.IsVisible = false;
            TopMonstersDisplay.IsVisible = false;
        }

        /// <summary>
        ///     Shows the Monster, Character, and message boxes
        /// </summary>
        public void ShowBattleUiElements()
        {
            BattleBottomBox.IsVisible = true;
            TopMonstersDisplay.IsVisible = true;
        }

        /// <summary>
        ///     Draws the Player figures
        /// </summary>
        public void DrawPlayerFigures()
        {
            _playerFigures.Clear();

            CharacterFigureArea.Children.Clear();
            MonsterFigureArea.Children.Clear();

            // Draw the Character figures
            foreach (var player in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList
                                                        .Where(m => m.PlayerType == PlayerTypeEnum.Character)
                                                        .ToList())
            {
                var playerFigure = CreatePlayerFigure(player);
                _playerFigures.Add(player.Guid, playerFigure);
                CharacterFigureArea.Children.Add(playerFigure);
            }

            // Draw the Monster figures
            foreach (var player in BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList
                                                        .Where(m => m.PlayerType == PlayerTypeEnum.Monster)
                                                        .ToList())
            {
                var playerFigure = CreatePlayerFigure(player);
                _playerFigures.Add(player.Guid, playerFigure);
                MonsterFigureArea.Children.Add(playerFigure);
            }
        }

        /// <summary>
        ///     Creates a Player figure
        /// </summary>
        /// <param name="player">The Player to make the figure for</param>
        /// <returns></returns>
        public StackLayout CreatePlayerFigure(PlayerInfoModel player)
        {
            player ??= new PlayerInfoModel
            {
                Name = "",
                ImageURI = ""
            };

            var playerFigureImageButton = new ImageButton
            {
                Source = player.ImageURI,
                WidthRequest = 100,
                HeightRequest = 100,
                Aspect = Aspect.AspectFit,
                BindingContext = player.Guid
            };
            playerFigureImageButton.Clicked += FigureButton_Clicked;

            var playerFigureLabel = new Label
            {
                Text = player.Name,
                HorizontalTextAlignment = TextAlignment.Center
            };

            var playerFigureStackLayout = new StackLayout
            {
                Children =
                {
                    playerFigureImageButton,
                    playerFigureLabel
                }
            };

            return playerFigureStackLayout;
        }

        public void ClearUI()
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

        // /// <summary>
        // ///     Behavior just before the page appears
        // /// </summary>
        // protected override void OnAppearing()
        // {
        //     base.OnAppearing();
        // }

        /// <summary>
        ///     Hide the different button states
        ///     Hide the message display box
        /// </summary>
        public void HideUIElements()
        {
            StartRoundButton.IsVisible = false;
            AttackButton.IsVisible = false;
            //MessageDisplayBox.IsVisible = false;
        }

        /// <summary>
        ///     Shows the proper Battle Mode
        /// </summary>
        public void ShowBattleMode()
        {
            // If running in UT mode,
            if (_unitTestSetting) return;

            HideUIElements();

            ClearMessages();

            DrawPlayerFigures();

            // Update the Mode
            PageTitle = BattleEngineViewModel.Instance
                                             .Engine
                                             .EngineSettings
                                             .RoundLocation
                                             .ToMessage();

            ShowBattleModeUiElements();
        }

        /// <summary>
        /// Control the UI Elements to display
        /// </summary>
        public void ShowBattleModeUiElements()
        {
            switch (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum)
            {
                case BattleStateEnum.Starting:
                    //GameUIDisplay.IsVisible = false;
                    //AttackerAttack.Source = ActionEnum.Unknown.ToImageURI();
                    StartRoundButton.IsVisible = true;
                    break;

                case BattleStateEnum.NewRound:
                    //UpdateMapGrid();
                    //AttackerAttack.Source = ActionEnum.Unknown.ToImageURI();
                    //NextRoundButton.IsVisible = true;
                    StartRoundButton.IsVisible = true;
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
                    //BattlePlayerInformationBox.IsVisible = true;
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
        /// <param name="player"></param>
        public void UpdatePlayerDetailsBox(PlayerInfoModel player)
        {
            // Update either the Character details box or the Monster details box
            switch (player.PlayerType)
            {
                case PlayerTypeEnum.Character:
                    SelectedCharacterIconImage.Source = player.IconImageURI;
                    SelectedCharacterNameLabel.Text = player.Name;
                    SelectedCharacterLevelLabel.Text = "Level: " + player.Level;
                    SelectedCharacterHealthLabel.Text = "HP: " + player.CurrentHealth;
                    SelectedCharacterAttackLabel.Text = "ATK: " + player.Attack;
                    SelectedCharacterDefenseLabel.Text = "DEF: " + player.Defense;
                    SelectedCharacterSpeedLabel.Text = "SPD: " + player.Speed;
                    break;

                case PlayerTypeEnum.Monster:
                default:
                    SelectedMonsterIconImage.Source = player.IconImageURI;
                    SelectedMonsterNameLabel.Text = player.Name;
                    SelectedMonsterLevelLabel.Text = "Level: " + player.Level;
                    SelectedMonsterHealthLabel.Text = "HP: " + player.CurrentHealth;
                    SelectedMonsterAttackLabel.Text = "ATK: " + player.Attack;
                    SelectedMonsterDefenseLabel.Text = "DEF: " + player.Defense;
                    SelectedMonsterSpeedLabel.Text = "SPD: " + player.Speed;
                    break;
            }
        }

        /// <summary>
        /// Clears the values from the player details boxes
        /// </summary>
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
            var score = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleScore;
            MessagingCenter.Send(this, "AddData", score);

            ShowBattleMode();
        }

        #region MessageHandelers

        /// <summary>
        ///     Builds up the output message
        /// </summary>
        internal static void GameMessage()
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

        #region Page EventHandlers

        /// <summary>
        ///     Set Character attack action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AttackButton_Clicked(object sender, EventArgs e)
        {
            var currentPlayer = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;

            BattleMessages.Text = "Who should " + currentPlayer.Name + " try to attack?";

            // Update battle state and current action
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.ChoosingMonsterTarget;
            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAction = ActionEnum.Attack;

            // Hide action buttons
            AttackButton.IsVisible = false;
            BlockButton.IsVisible = false;
        }

        /// <summary>
        ///     Set Character block action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BlockButton_Clicked(object sender, EventArgs e)
        {
            var currentPlayer = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;

            BattleMessages.Text = "Who should " + currentPlayer.Name + " try to block?";

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
        public async void Settings_Clicked(object sender, EventArgs e) =>
            await ShowBattleSettingsPage();

        /// <summary>
        /// When a figure is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FigureButton_Clicked(object sender, EventArgs e)
        {
            // Ignore selection if it's not time to choose
            if (BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum != BattleStateEnum.ChoosingMonsterTarget)
                return;

            // Set the selected Monster as the target
            var targetPlayerGuid = ((ImageButton)sender).BindingContext as string;
            var targetMonster = BattleEngineViewModel.Instance.Engine.EngineSettings.PlayerList.Find(m => m.Guid == targetPlayerGuid);

            // Don't allow the user to select a Character target
            if (targetMonster.PlayerType == PlayerTypeEnum.Character)
            {
                BattleMessages.Text = $"Hey, {targetMonster.Name} is on your side! Pick a monster!";
                return;
            }

            BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender = targetMonster;

            // Update the Monster details box
            UpdatePlayerDetailsBox(targetMonster);

            // Highlight their figure
            var targetPlayerFigure = _playerFigures[targetPlayerGuid];
            targetPlayerFigure.BackgroundColor = Color.FromHex("#88ff6666");

            DoCharacterTurn();

            // Show next button
            NextButton.IsVisible = true;
            NextButton.IsEnabled = true;
        }

        /// <summary>
        /// When the Exit button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ExitButton_Clicked(object sender, EventArgs e) =>
            await Navigation.PopModalAsync();

        /// <summary>
        /// When the Start button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void StartRoundButton_Clicked(object sender, EventArgs e)
        {
            // Open a new round page
            await Navigation.PushModalAsync(new NewRoundPage());

            // Set battle state to battling
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.Battling;

            // Show UI
            ShowBattleMode();
            ShowBattleUiElements();

            // Start the first turn
            StartTurn();
        }

        /// <summary>
        /// When the Next button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            }
        }

        /// <summary>
        /// When the End Round button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void EndRoundButton_Clicked(object sender, EventArgs e)
        {
            // Start a new Round
            BattleEngineViewModel.Instance.Engine.Round.NewRound();
            EnterNewRound();
        }

        /// <summary>
        /// Perform a Character's turn
        /// </summary>
        public void DoCharacterTurn()
        {
            var activeCharacter = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;
            BattleEngineViewModel.Instance.Engine.Round.Turn.TakeTurn(activeCharacter);

            var targetMonster = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender;

            // Highlight the targeted Monster's figure
            UpdatePlayerDetailsBox(targetMonster);

            // Update battle messages
            BattleMessages.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.TurnMessage;

            // Set battle state
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.EndingMonsterTurn;
        }

        /// <summary>
        /// Perform a Monster's turn.
        /// </summary>
        public void DoMonsterTurn()
        {
            var activeMonster = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;
            BattleEngineViewModel.Instance.Engine.Round.Turn.TakeTurn(activeMonster);

            // Choose which Character to target
            var targetCharacter = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentDefender;

            // Highlight the targeted Character's figure
            UpdatePlayerDetailsBox(targetCharacter);

            // Highlight their figure
            var currentPlayerFigure = _playerFigures[targetCharacter.Guid];
            currentPlayerFigure.BackgroundColor = Color.FromHex("#88ff6666");

            // Update battle messages
            BattleMessages.Text = BattleEngineViewModel.Instance.Engine.EngineSettings.BattleMessagesModel.TurnMessage;

            // Set battle state
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.EndingMonsterTurn;
        }

        /// <summary>
        /// Ends current turn.
        /// </summary>
        public void EndTurn()
        {
            var roundCondition = BattleEngineViewModel.Instance.Engine.Round.RoundNextTurn();

            switch (roundCondition)
            {
                case RoundEnum.NewRound:
                    BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.NewRound;

                    // Show the Round Over page
                    ShowModalRoundOverPage();

                    // Hide battle UI
                    HideBattleUiElements();

                    StartRoundButton.IsVisible = false;
                    EndRoundButton.IsVisible = true;

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

                case RoundEnum.NextTurn:
                    StartTurn();
                    break;
            }
        }

        /// <summary>
        ///     Determines the next Player and lets them act.
        /// </summary>
        public void StartTurn()
        {
            ClearUI();

            // Determine the current Player
            var currentPlayer = BattleEngineViewModel.Instance.Engine.Round.GetNextPlayerTurn();
            BattleEngineViewModel.Instance.Engine.Round.SetCurrentAttacker(currentPlayer);

            // Show their details in the corresponding details box
            UpdatePlayerDetailsBox(currentPlayer);

            // Highlight their figure
            var currentPlayerFigure = _playerFigures[currentPlayer.Guid];
            currentPlayerFigure.BackgroundColor = Color.FromHex("#88a6cc7e");

            // A Character's turn action is chosen by the game player. A Monster's action is chosen automatically
            if (currentPlayer.PlayerType == PlayerTypeEnum.Character)
                StartCharacterTurn();
            else
                StartMonsterTurn();
        }

        /// <summary>
        ///     Start's a Character's turn.
        /// </summary>
        public void StartCharacterTurn()
        {
            var activeCharacter = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;

            // Show battle message stating whose turn it is
            var battleMessage = "It's " + activeCharacter.Name + "'s turn! What should they do?";
            BattleMessages.Text = battleMessage;

            // Show/Enable action buttons
            AttackButton.IsVisible = true;
            BlockButton.IsVisible = true;

            AttackButton.IsEnabled = true;
            BlockButton.IsEnabled = true;
        }

        /// <summary>
        ///     Starts a Monster's turn.
        /// </summary>
        public void StartMonsterTurn()
        {
            BattleEngineViewModel.Instance.Engine.EngineSettings.BattleStateEnum = BattleStateEnum.StartingMonsterTurn;

            var activeMonster = BattleEngineViewModel.Instance.Engine.EngineSettings.CurrentAttacker;

            // Show battle message stating whose turn it is
            var battleMessage = "It's " + activeMonster.Name + "'s turn! Watch out!";
            BattleMessages.Text = battleMessage;

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

        #endregion Page EventHandlers
    }
}
