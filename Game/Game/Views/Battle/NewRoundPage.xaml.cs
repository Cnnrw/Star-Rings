using System;
using System.Diagnostics;

using Game.Enums;
using Game.Models;
using Game.ViewModels;

using Xamarin.Forms;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    public partial class NewRoundPage : BaseContentPage
    {
        // This uses the Instance so it can be shared with other Battle Pages as needed
        readonly BattleEngineViewModel _engineViewModel = BattleEngineViewModel.Instance;

        /// <summary>
        /// Constructor
        /// </summary>
        public NewRoundPage()
        {
            InitializeComponent();

            var roundLocationName = BattleLocationEnumExtensions.ToMessageWithArticle(BattleEngineViewModel.Instance.Engine.EngineSettings.RoundLocation);
            RoundDetailsLabel.Text = "Your party is ambushed while traveling through " + roundLocationName + "!";
            Debug.WriteLine(BattleEngineViewModel.Instance.Engine.EngineSettings.RoundLocation);
            // Draw the Monsters
            foreach (var data in _engineViewModel.Engine.EngineSettings.MonsterList)
            {
                MonsterListFrame.Children.Add(CreatePlayerDisplayBox(data));
            }
        }

        /// <summary>
        /// Start next Round, returning to the battle screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void BeginButton_Clicked(object sender, EventArgs e)
        {
            await App.NavigationService.GoBack();
        }

        /// <summary>
        /// Return a stack layout with the Player information inside
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public StackLayout CreatePlayerDisplayBox(PlayerInfoModel data)
        {
            data ??= new PlayerInfoModel();

            // Hookup the image
            var PlayerImage = new Image
            {
                Style = Application.Current.Resources.TryGetValue("ImageBattleLargeStyle", out object imageStyle)
                            ? (Style)imageStyle
                            : null,
                Source = data.ImageURI
            };

            var ValueStyleMicro = Application.Current.Resources.TryGetValue("ValueStyleMicro", out object valueStyle1)
                                      ? (Style)valueStyle1
                                      : null;

            // Add the Level
            var PlayerLevelLabel = new Label
            {
                Text = "Level: " + data.Level,
                Style = ValueStyleMicro,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            // Add the HP
            var PlayerHPLabel = new Label
            {
                Text = "HP: " + data.GetCurrentHealthTotal,
                Style = ValueStyleMicro,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            var PlayerNameLabel = new Label()
            {
                Text = data.Name,
                Style = ValueStyleMicro,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing = 1,
                LineHeight = 1,
                MaxLines = 1,
            };

            // Put the Image Button and Text inside a layout
            var PlayerStack = new StackLayout
            {
                //Style = (Style)Application.Current.Resources["PlayerInfoBox"],
                Style = Application.Current.Resources.TryGetValue("PlayerInfoBox", out object boxStyle)
                            ? (Style)boxStyle
                            : null,
                HorizontalOptions = LayoutOptions.Center,
                Padding = 0,
                Spacing = 0,
                Children =
                {
                    PlayerImage,
                    PlayerNameLabel,
                    PlayerLevelLabel,
                    PlayerHPLabel,
                },
            };

            return PlayerStack;
        }
    }
}
