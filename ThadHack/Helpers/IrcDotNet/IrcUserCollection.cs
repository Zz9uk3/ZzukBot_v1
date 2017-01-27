using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ZzukBot.Helpers.IrcDotNet
{
    /// <summary>
    /// Represents a collection of <see cref="IrcUser"/> objects.
    /// </summary>
    /// <threadsafety static="true" instance="false"/>
    /// <seealso cref="IrcUser"/>
    public class IrcUserCollection : ReadOnlyCollection<IrcUser>
    {
        private IrcClient client;
        
        internal IrcUserCollection(IrcClient client, IList<IrcUser> list)
            : base(list)
        {
            this.client = client;
        }

        /// <summary>
        /// Gets the client to which the collection of users belongs.
        /// </summary>
        /// <value>The client to which the collection of users belongs.</value>
        public IrcClient Client
        {
            get { return this.client; }
        }
    }
}
