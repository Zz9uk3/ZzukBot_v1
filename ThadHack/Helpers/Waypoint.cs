using ZzukBot.Constants;

namespace ZzukBot.Helpers
{
    internal class Waypoint
    {
        internal XYZ Position;
        internal Enums.PositionType Type;

        internal Waypoint()
        {
        }

        internal Waypoint(XYZ parPos, Enums.PositionType parPosType)
        {
            Position = parPos;
            Type = parPosType;
        }
    }
}