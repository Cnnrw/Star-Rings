using System;

using Xamarin.Forms;

namespace Game.Templates.Pages
{
    /// <summary>
    /// ModalPage adds a page background and a close button that pops the
    /// top page from the navigation stack.
    /// </summary>
    public partial class ModalPage : ContentPage
    {
        static readonly BindableProperty PageBackgroundProperty =
            BindableProperty.Create(nameof(pageBackground), typeof(ImageSource), typeof(ModalPage), (ImageSource)"page_background.png");
        public ImageSource pageBackground => (ImageSource)GetValue(PageBackgroundProperty);

        public ModalPage()
        {
            InitializeComponent();

            // Remove the navigation bar from the page
            NavigationPage.SetHasNavigationBar(this, false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void CloseButton_OnClick(object sender, EventArgs e) =>
            await Navigation.PopAsync();
    }
}
