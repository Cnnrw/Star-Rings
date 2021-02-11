using Xamarin.Forms;

[assembly: ExportFont("gamefont.ttf", Alias = "8bit")]
[assembly: ExportFont("gamefont_bold.ttf", Alias = "8bit_bold")]
[assembly: ExportFont("rpgawesome.ttf", Alias = "rpg")]
namespace Common.Styles
{
    public partial class DefaultTheme : ResourceDictionary
    {
        public DefaultTheme() => InitializeComponent();
    }
}