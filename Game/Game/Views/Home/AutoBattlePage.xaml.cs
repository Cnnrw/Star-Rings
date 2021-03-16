using System;

using Game.Engine.EngineInterfaces;
using Game.ViewModels;

namespace Game.Views
{
	/// <summary>
	/// The Main Game Page
	/// </summary>
	public partial class AutoBattlePage : BaseContentPage
	{
		// Hold the Engine, so it can be swapped out for unit testing
		public readonly IAutoBattleInterface AutoBattle = BattleEngineViewModel.Instance.AutoBattleEngine;

		/// <summary>
		/// Constructor
		/// </summary>
		public AutoBattlePage () =>
			InitializeComponent ();

		public async void AutoBattleButton_Clicked(object sender, EventArgs e)
		{
			await AutoBattle.RunAutoBattle();

			var battleMessage = $"Done {AutoBattle.Battle.EngineSettings.BattleScore.RoundCount} Rounds";

			BattleMessageValue.Text = battleMessage;
		}
	}
}
