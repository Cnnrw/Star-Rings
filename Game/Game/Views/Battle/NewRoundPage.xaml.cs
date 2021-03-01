using System;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Models;
using Game.ViewModels;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewRoundPage: ContentPage
	{
		// This uses the Instance so it can be shared with other Battle Pages as needed
		public BattleEngineViewModel EngineViewModel = BattleEngineViewModel.Instance;

		/// <summary>
		/// Constructor
		/// </summary>
		public NewRoundPage ()
		{
			InitializeComponent ();

			// Draw the Monsters
			foreach (var data in EngineViewModel.Engine.EngineSettings.MonsterList)
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
			await Navigation.PopModalAsync();
		}

		/// <summary>
		/// Return a stack layout with the Player information inside
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public StackLayout CreatePlayerDisplayBox(PlayerInfoModel data)
		{
            if (data == null)
            {
                data = new PlayerInfoModel();
            }

            // Hookup the image
            var PlayerImage = new Image
            {
                //Style = (Style)Application.Current.Resources["ImageBattleLargeStyle"],
                Style = Application.Current.Resources.TryGetValue("ImageBattleLargeStyle", out object imageStyle)
                            ? (Style)imageStyle
                            : null,
                Source = data.ImageURI
            };

            // Add the Level
            var PlayerLevelLabel = new Label
            {
                Text = "Level : "+data.Level,
                //Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                Style = Application.Current.Resources.TryGetValue("ValueStyleMicro", out object valueStyle1)
                            ? (Style)valueStyle1
                            : null,
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
                Text = "HP : "+ data.GetCurrentHealthTotal,
                //Style = (Style)Application.Current.Resources["ValueStyleMicro"],
                Style = Application.Current.Resources.TryGetValue("ValueStyleMicro", out object valueStyle2)
                            ? (Style)valueStyle2
                            : null,
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
                //Style = (Style)Application.Current.Resources["ValueStyle"],
                Style = Application.Current.Resources.TryGetValue("ValueStyle", out object valueStyle3)
                            ? (Style)valueStyle3
                            : null,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                Padding = 0,
                LineBreakMode = LineBreakMode.TailTruncation,
                CharacterSpacing=1,
                LineHeight=1,
                MaxLines =1,
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
                Children = {
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
