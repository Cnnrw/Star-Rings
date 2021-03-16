namespace Game.Views
{
    /// <summary>
    ///     The Main Game Page
    /// </summary>
    public partial class ScorePage : BaseContentPage
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public ScorePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Back Button override
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed() => true;
    }
}
