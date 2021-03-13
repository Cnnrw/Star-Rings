using System.Threading.Tasks;

using Xamarin.Forms;

namespace Game.Controls.TabView
{
    public interface ITabViewItemAnimation
    {
		Task OnSelected(View tabViewItem);

		Task OnDeSelected(View tabViewItem);
	}
}
