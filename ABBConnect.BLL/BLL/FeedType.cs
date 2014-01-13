using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// Class that rappresent the type of the user that made the feed.
    /// It could be a human user, a sensor, or None like null value.
    /// </summary>
    public class FeedType
    {
        /// <summary>
        /// Enumerative that rappresent the type of the user that made the feed.
        /// </summary>
        public enum FeedSource
        {
            /// <summary>
            /// This is used like null value
            /// </summary>
            None,
            /// <summary>
            /// This type means that the feeds is made by a human user
            /// </summary>
            Human,
            /// <summary>
            /// This type means that the feeds is made by a sensor
            /// </summary>
            Sensor
        };
    }
}
