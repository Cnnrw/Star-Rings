using System.Collections.Generic;

namespace Game.Components
{
    /// <summary>
    /// Grouping of items by key into ObservableRange
    /// </summary>
    public class Grouping<TKey, TItem> : ObservableRangeCollection<TItem>
    {

        /// <summary>
        /// Initializes a new instance of the Grouping class.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="items"></param>
        public Grouping(TKey key, IEnumerable<TItem> items)
        {
            Key = key;
            AddRange(items);
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public TKey Key { get; }

        /// <summary>
        /// Returns list of items in the grouping.
        /// </summary>
        public new IList<TItem> Items => base.Items;
    }

    /// <summary>
    /// Grouping of items by key into ObservableRange
    /// </summary>
    public class Grouping<TKey, TSubKey, TItem> : ObservableRangeCollection<TItem>
    {

        /// <summary>
        /// Initializes a new instance of the Grouping class.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="subkey">subkey.</param>
        /// <param name="items">Items.</param>
        public Grouping(TKey key, TSubKey subkey, IEnumerable<TItem> items)
        {
            Key = key;
            SubKey = subkey;
            AddRange(items);
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <value>The key.</value>
        public TKey Key { get; }

        /// <summary>
        /// Gets the subkey of the grouping.
        /// </summary>
        public TSubKey SubKey { get; }

        /// <summary>
        /// Returns list of items in the grouping.
        /// </summary>
        public new IList<TItem> Items => base.Items;
    }
}