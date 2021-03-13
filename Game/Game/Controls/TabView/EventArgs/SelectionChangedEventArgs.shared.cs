using System;

namespace Game.Controls.TabView
{
    public class TabSelectionChangedEventArgs : EventArgs
    {
        public int NewPosition { get; set; }
        public int OldPosition { get; set; }
    }
}
