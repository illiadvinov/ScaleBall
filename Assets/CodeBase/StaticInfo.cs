using UnityEngine;

namespace CodeBase
{
    public static class StaticInfo
    {
        public static readonly float MinimumShootBallScale = .20f;
        public static readonly Vector3 DecreaseAmount = new(.001f, .001f, .001f);
        public static readonly Vector3 IncreaseAmount = new(.002f, .002f, .002f);
    }
}