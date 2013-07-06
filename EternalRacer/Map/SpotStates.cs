namespace EternalRacer.Map
{
    /// <summary>
    /// State of Spot.
    /// </summary>
    public enum SpotStates
    {
        /// <summary>
        /// Not reachable Spot.
        /// </summary>
        Occupied,
        /// <summary>
        /// Available Spot.
        /// </summary>
        Free,
        /// <summary>
        /// Spot for Player.
        /// Neighbourhood are available. Neighbours can not reach Spot.
        /// </summary>
        OccupyIncoming,
        /// <summary>
        /// Spot for Enemy.
        /// Can not reach Neighbourhood. Spot is available for Neighbours
        /// </summary>
        OccupyOutgoing
    }
}
