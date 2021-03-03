using Xamarin.Forms;

[assembly: ExportFont("gamefont.ttf", Alias = "8bit")]
[assembly: ExportFont("gamefont_bold.ttf", Alias = "8bit_bold")]
namespace Game.Styles
{
    public partial class DefaultTheme : ResourceDictionary
    {
        public DefaultTheme() => InitializeComponent();
    }
}
