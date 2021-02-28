using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Game.Engine.EngineKoenig;
using Game.Models;
using Game.ViewModels;
using Game.Engine.EngineInterfaces;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BattleHomePage : ContentPage
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public BattleHomePage ()
		{
			InitializeComponent ();
		}
	}
}
