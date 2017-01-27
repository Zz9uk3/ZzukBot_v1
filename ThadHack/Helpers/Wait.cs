using System;
using System.Collections.Generic;
using System.Linq;

namespace ZzukBot.Helpers
{
    internal static class Wait
    {
        // internal list which stores all items
        private static List<Item> Items = new List<Item>();

        /// <summary>
        ///     Did X ms pass after starting the stopwatch for name
        /// </summary>
        internal static bool For(string parName, int parMs, bool parAutoReset = true)
        {
            // get the item with name XX
            var tmpItem = Items
                .FirstOrDefault(i => i.Name == parName);
            // did we find a item?
            if (tmpItem == null)
            {
                // we didnt found one! lets create it
                tmpItem = new Item(parName, parAutoReset);
                // and add it to the list
                Items.Add(tmpItem);
                // the time supplied in parMs didnt elapsed since
                // item creation
                return false;
            }
            // the item exists! lets check when it got created
            var Elapsed = (DateTime.Now - tmpItem.Added).TotalMilliseconds >= parMs;
            // the time passed in parMs elapsed since the item creation
            // remove the item and return true
            if (Elapsed && tmpItem.AutoReset) Items.Remove(tmpItem);
            return Elapsed;
        }

        internal static void Remove(string parName)
        {
            var tmpItem = Items
                .FirstOrDefault(i => i.Name == parName);
            if (tmpItem == null) return;
            Items.Remove(tmpItem);
        }

        internal static void RemoveAll()
        {
            Items = new List<Item>();
        }

        /// <summary>
        ///     internal class item to save the date when we first asked
        ///     if the item is ready
        /// </summary>
        private class Item
        {
            // the date we asked for the item with name the first time
            internal readonly DateTime Added;
            // Should we auto reset after enough time elapsed?
            internal readonly bool AutoReset;
            // Name ís the identifier
            internal readonly string Name;
            // constructor
            internal Item(string parName, bool parAutoReset = true)
            {
                Name = parName;
                Added = DateTime.Now;
                AutoReset = parAutoReset;
            }
        }
    }
}