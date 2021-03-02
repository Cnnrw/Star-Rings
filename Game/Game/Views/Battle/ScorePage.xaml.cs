using System.Collections.Generic;
using System.Linq;

using Game.Models;
using Game.Templates.Pages;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    ///     The Main Game Page
    /// </summary>
    public partial class ScorePage : ModalPage
    {
        // This uses the Instance so it can be shared with other Battle Pages as needed
        readonly BattleEngineViewModel _engineViewModel = BattleEngineViewModel.Instance;

        /// <summary>
        ///     Constructor
        /// </summary>
        public ScorePage()
        {
            InitializeComponent();

            var deadCharacters = new List<PlayerInfoModel>
            {
                new PlayerInfoModel(CharacterIndexViewModel.Instance.Dataset[0]),
                new PlayerInfoModel(CharacterIndexViewModel.Instance.Dataset[1]),
            };

            var deadMonsters = new List<PlayerInfoModel>
            {
                new PlayerInfoModel(MonsterIndexViewModel.Instance.Dataset[0]),
                new PlayerInfoModel(MonsterIndexViewModel.Instance.Dataset[2])
            };

            // mock data
            var battleScore = new ScoreModel
            {
                AutoBattle = false,
                BattleNumber = 15,
                ScoreTotal = 100,
                CharacterAtDeathList = null,
                CharacterModelDeathList = deadCharacters,
                MonsterModelDeathList = deadMonsters,
                MonsterSlainNumber = deadMonsters.Count(),
                // ItemsDroppedList = newData.ItemsDroppedList,
            };

            _engineViewModel.Engine.EngineSettings.BattleScore = battleScore;
            DrawOutput();
        }

        /// <summary>
        ///     Draw data for
        ///     Character
        ///     Monster
        ///     Item
        /// </summary>
        public void DrawOutput()
        {
            // Draw the Characters
            foreach (var data in _engineViewModel.Engine.EngineSettings.BattleScore.CharacterModelDeathList)
                CharacterListFrame.Children.Add(CreateCharacterDisplayBox(data));

            // Draw the Monsters
            foreach (var data in _engineViewModel.Engine.EngineSettings.BattleScore.MonsterModelDeathList.Distinct())
                MonsterListFrame.Children.Add(CreateMonsterDisplayBox(data));

            // // Draw the Items
            // foreach (var data in _engineViewModel.Engine.EngineSettings.BattleScore.ItemModelDropList.Distinct())
            //     ItemListFrame.Children.Add(CreateItemDisplayBox(data));
            //
            // Update Values in the UI
            TotalKilled.Text = _engineViewModel.Engine.EngineSettings.BattleScore.MonsterModelDeathList.Count().ToString();
            // TotalCollected.Text = _engineViewModel.Engine.EngineSettings.BattleScore.ItemModelDropList.Count().ToString();
            TotalScore.Text = _engineViewModel.Engine.EngineSettings.BattleScore.ScoreTotal.ToString();
        }

        /// <summary>
        ///     Return a stack layout for the Characters
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static StackLayout CreateCharacterDisplayBox(PlayerInfoModel data)
        {
            data ??= new PlayerInfoModel();

            // Hookup the image
            var playerImage = new Image
            {
                Source = data.IconImageURI,
                Style = Application.Current.Resources.TryGetValue("ImageBattleMediumStyle", out var playerImgStyle)
                            ? (Style)playerImgStyle
                            : null
            };

            // Add the Level
            var playerLevelLabel = new Label
            {
                Text = "Level : " + data.Level,
                Style = Application.Current.Resources.TryGetValue("ValueStyleMicro", out var playerLvlStyle)
                            ? (Style)playerLvlStyle
                            : null,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1
            };

            // Put the Image Button and Text inside a layout
            var playerStack = new StackLayout
            {
                Style = Application.Current.Resources.TryGetValue("ScoreCharacterInfoBox", out var playerScoreInfoBoxStyle)
                            ? (Style)playerScoreInfoBoxStyle
                            : null,
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children =
                {
                    playerImage,
                    playerLevelLabel
                }
            };

            return playerStack;
        }

        /// <summary>
        ///     Return a stack layout for the Monsters
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static StackLayout CreateMonsterDisplayBox(PlayerInfoModel data)
        {
            data ??= new PlayerInfoModel();

            // Hookup the image
            var playerImage = new Image
            {
                Source = data.IconImageURI,
                Style = Application.Current.Resources.TryGetValue("ImageBattleMediumStyle", out var playerImgStyle)
                            ? (Style)playerImgStyle
                            : null
            };

            // Add the Level
            var playerLevelLabel = new Label
            {
                Text = "Level : " + data.Level,
                Style = Application.Current.Resources.TryGetValue("ValueStyleMicro", out var playerLvlStyle)
                            ? (Style)playerLvlStyle
                            : null,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1
            };

            // Put the Image Button and Text inside a layout
            var playerStack = new StackLayout
            {
                Style = Application.Current.Resources.TryGetValue("ScoreCharacterInfoBox", out var playerScoreInfoBoxStyle)
                            ? (Style)playerScoreInfoBoxStyle
                            : null,
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children =
                {
                    playerImage,
                    playerLevelLabel
                }
            };

            return playerStack;
        }

        /// <summary>
        ///     Return a stack layout with the Player information inside
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static StackLayout CreateItemDisplayBox(ItemModel data)
        {
            data ??= new ItemModel();

            // Hookup the image
            var playerImage = new Image
            {
                //Style = (Style)Application.Current.Resources["ImageBattleSmallStyle"],
                Source = data.ImageURI
            };

            // Put the Image Button and Text inside a layout
            var playerStack = new StackLayout
            {
                //Style = (Style)Application.Current.Resources["ScoreItemInfoBox"],
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children =
                {
                    playerImage
                }
            };

            return playerStack;
        }

        // /// <summary>
        // ///     Close the Page
        // /// </summary>
        // /// <param name="sender"></param>
        // /// <param name="e"></param>
        // public async void CloseButton_Clicked(object sender, EventArgs e) =>
        //     await Navigation.PopModalAsync();
    }
}
