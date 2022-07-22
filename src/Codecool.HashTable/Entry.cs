namespace Codecool.HashTable
{
    /// <summary>
    /// Basic hash table entry node
    /// </summary>
    /// <typeparam name="TKey">Type of key</typeparam>
    /// <typeparam name="TValue">Type of value</typeparam>
    public class Entry<TKey, TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entry{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public Entry(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Gets or sets the Key of entry.
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// Gets or sets the Value of entry.
        /// </summary>
        public TValue Value { get; set; }
    }
}
