using System.Collections.Generic;
using System.Linq;

using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    ///     The Main Game Page
    /// </summary>
    public partial class ScorePage : BaseContentPage
    {
        // This uses the Instance so it can be shared with other Battle Pages as needed
        readonly BattleEngineViewModel _engineViewModel = BattleEngineViewModel.Instance;

        /// <summary>
        ///     Constructor
        /// </summary>
        public ScorePage()
        {
            InitializeComponent();
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
            foreach (var data in _engineViewModel.Engine.EngineSettings.BattleScore.ItemModelDropList.Distinct())
                ItemListFrame.Children.Add(CreateItemDisplayBox(data));
            
            // Update Values in the UI
            TotalKilled.Text = _engineViewModel.Engine.EngineSettings.BattleScore.MonsterModelDeathList.Count().ToString();

            TotalCollected.Text = _engineViewModel.Engine.EngineSettings.BattleScore.ItemModelDropList.Count().ToString();
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
                            : null,
                Aspect = Aspect.AspectFit
            };

            // Add the Name
            var playerNameLabel = new Label
            {
                Text = data.Name,
                Style = Application.Current.Resources.TryGetValue("ValueStyleMicro", out var playerNameStyle)
                            ? (Style)playerNameStyle
                            : null,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.NoWrap,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1
            };

            // Add the Level
            var playerLevelLabel = new Label
            {
                Text = "Level: " + data.Level,
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
                    playerNameLabel,
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
                            : null,
                Aspect = Aspect.AspectFit
            };

            // Add the Name
            var playerNameLabel = new Label
            {
                Text = data.Name,
                Style = Application.Current.Resources.TryGetValue("ValueStyleMicro", out var playerNameStyle)
                            ? (Style)playerNameStyle
                            : null,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.NoWrap,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1
            };

            // Add the Level
            var playerLevelLabel = new Label
            {
                Text = "Level: " + data.Level,
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
                    playerNameLabel,
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
            var itemImage = new Image
            {
                Style = Application.Current.Resources.TryGetValue("ImageBattleSmallStyle", out var itemImgStyle)
                            ? (Style)itemImgStyle
                            : null,
                Aspect = Aspect.AspectFit,
                Source = data.ImageURI
            };

            // Put the Image Button and Text inside a layout
            var itemStack = new StackLayout
            {
                Style = Application.Current.Resources.TryGetValue("ScoreItemInfoBox", out var itemStackStyle)
                            ? (Style)itemStackStyle
                            : null,
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children =
                {
                    itemImage
                }
            };

            return itemStack;
        }
    }
}
