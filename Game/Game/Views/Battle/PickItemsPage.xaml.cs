using System;

using Game.ViewModels;

namespace Game.Views
{
    /// <summary>
    /// The Main Game Page
    /// </summary>
    public partial class PickItemsPage : BaseContentPage
    {
        // Empty Constructor for UTs
        internal PickItemsPage(bool unitTest) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public PickItemsPage()
        {
            InitializeComponent();

            BindingContext = BattleEngineViewModel.Instance;
        }

        /// <summary>
        /// Handles the select event for dropped items for that round
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnItemInPoolSelected(object sender, EventArgs e)
        {
            // TODO: implement
        }

        /// <summary>
        /// Automatically distribute items amongst the character party
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DistributeButton_Clicked(object sender, EventArgs e)
        {
            // TODO: implement distribution
        }

        /// <summary>
        /// Handles the select event for party members
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnPartyMemberSelected(object sender, EventArgs e)
        {
            // TODO: implement
        }
    }
}
