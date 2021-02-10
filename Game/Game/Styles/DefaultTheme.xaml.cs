using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Game.Styles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DefaultTheme : ResourceDictionary
    {
        public DefaultTheme() => InitializeComponent();
    }
}