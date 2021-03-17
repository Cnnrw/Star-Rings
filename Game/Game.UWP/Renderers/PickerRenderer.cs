using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Picker), typeof(Game.UWP.Renderers.PickerRenderer))]
namespace Game.UWP.Renderers
{
    public class PickerRenderer : Xamarin.Forms.Platform.UWP.PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Picker> e)
        {
            base.OnElementChanged(e);

            Control.BorderThickness = new Thickness(0,0,0,1);

            Control.Margin = new Thickness(0);
            Control.Padding = new Thickness(0);

            Control.Background = new SolidColorBrush(Colors.Transparent);
        }
    }
}
