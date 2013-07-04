namespace EternalRacer.GameMap
{
    public static class SpotStateExtensions
    {
        public static SpotState ToSpotState(this int value)
        {
            return (value == 0) ? SpotState.Occupy : SpotState.Free;
        }

        public static int ToInt(this SpotState state)
        {
            return (int)state;
        }
    }
}
