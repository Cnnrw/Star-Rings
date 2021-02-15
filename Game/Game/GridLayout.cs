using System;
using System.Diagnostics;

using Xamarin.Forms;

namespace Game
{
    public class GridLayout : Layout<View>
    {

        private double _childHeight;
        private double _childWidth;

        public int CellCount { get; set; }

        public StackOrientation Orientation { get; set; } = StackOrientation.Vertical;

        public int CellSpacing { get; set; }


        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            Measure(width, height);
            //var columns = GetColumnsCount(Children.Count, width, childWidth);
            //var rows = GetRowsCount(Children.Count, columns);

            int columns;
            int rows;
            double boundsWidth;
            double boundsHeight;
            Rectangle bounds;
            var count = 0;
            int primaryAxis;
            int secondaryAxis;

            if (Orientation == StackOrientation.Horizontal)
            {
                rows = CellCount;
                columns = Children.Count / CellCount;
                boundsWidth = _childWidth;
                boundsHeight = height / rows;
                primaryAxis = rows;
                secondaryAxis = columns;
            }
            else
            {
                rows = Children.Count / CellCount;
                columns = CellCount;
                boundsWidth = width / columns;
                boundsHeight = _childHeight;
                primaryAxis = columns;
                secondaryAxis = rows;
            }

            Debug.WriteLine($"BoundsWidth: {boundsWidth}, BoundsHeight: {boundsHeight}");

            bounds = new Rectangle(0, 0, boundsWidth, boundsHeight);

            for (var i = 0; i < primaryAxis; i++)
            {
                bounds.Y = (i * boundsHeight) + ((i + 1) * CellSpacing);
                Debug.WriteLine($"Y: {bounds.Y}");
                for (var j = 0; j < secondaryAxis && count < Children.Count; j++)
                {
                    var item = Children[count];
                    bounds.X = (j * boundsWidth) + ((j + 1) * CellSpacing);
                    item.Layout(bounds);
                    count++;
                    Debug.WriteLine($"X: {bounds.X}");
                }
            }
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            foreach (var child in Children)
            {
                if (!child.IsVisible)
                {
                    continue;
                }

                var sizeRequest = child.Measure(double.PositiveInfinity, double.PositiveInfinity);
                var minimum = sizeRequest.Minimum;
                var request = sizeRequest.Request;

                _childHeight = Math.Max(minimum.Height, request.Height - (CellSpacing * 2));
                _childWidth = Math.Max(minimum.Width, request.Width - (CellSpacing * 2));
            }

            int columns;
            int rows;

            if (Orientation == StackOrientation.Horizontal)
            {
                rows = CellCount;
                columns = Children.Count / CellCount;
            }
            else
            {
                rows = Children.Count / CellCount;
                columns = CellCount;
            }
            var size = new Size((columns * _childWidth) + ((columns + 1) * CellSpacing),
                                (rows * _childHeight) + ((rows + 1) * CellSpacing));
            return new SizeRequest(size, size);
        }
    }
}