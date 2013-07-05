using System;

namespace EternalRacer.Map
{
    /// <summary>
    /// Relationship between two Spot, as the Directions between them.
    /// </summary>
    public class SpotDirection
    {
        /// <summary>
        /// The Spot which is designated Directions.
        /// </summary>
        public Spot From { get; private set; }
        /// <summary>
        /// Spot to indicate the Directions
        /// </summary>
        public Spot ToThat { get; private set; }
        /// <summary>
        /// The Directions between From and ToThat.
        /// </summary>
        public Directions Direction { get; private set; }


        /// <summary>
        /// SpotDirection constructor, sets the read-only Spot and a Directions between them.
        /// </summary>
        /// <param name="from">Designate direction</param>
        /// <param name="toThat">Direction indicated on it.</param>
        /// <exception cref="ArgumentNullException"/>
        public SpotDirection(Spot from, Spot toThat)
        {
            if (from == null)
            {
                throw new ArgumentNullException("from");
            }
            if (toThat == null)
            {
                throw new ArgumentNullException("ToThat");
            }

            From = from;
            ToThat = toThat;
            Direction = From.Direction(ToThat);
        }
    }
}
