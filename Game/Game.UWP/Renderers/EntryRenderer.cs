using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Entry), typeof(Game.UWP.Renderers.EntryRenderer))]
namespace Game.UWP.Renderers
{
    public class EntryRenderer : Xamarin.Forms.Platform.UWP.EntryRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
                return;

            Control.Background = new SolidColorBrush(Colors.Transparent);
            Control.BackgroundFocusBrush = new SolidColorBrush(Colors.Transparent);

            Control.BorderBrush = new SolidColorBrush(Colors.Black);
            Control.BorderThickness = new Thickness(0,0,0,1);

            Control.VerticalAlignment = VerticalAlignment.Center;
        }
    }
}
