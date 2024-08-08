using UnityEngine;

namespace CodeBase
{
    public static class StaticInfo
    {
        public static readonly float MinimumShootBallScale = 0.1f;
        public static readonly Vector3 DecreaseAmount = new(0.004f, 0.004f, 0.004f);
        public static readonly Vector3 PlatformDecreaseAmount = new(0.004f, 0f, 0f);
        public static readonly Vector3 IncreaseAmount = new(0.004f, 0.004f, 0.004f);
    }
}