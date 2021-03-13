using System;

namespace Game.Controls.TabView
{
    public class TabTappedEventArgs : EventArgs
    {
        public TabTappedEventArgs(int position) => Position = position;

        public int Position { get; set; }
    }
}
