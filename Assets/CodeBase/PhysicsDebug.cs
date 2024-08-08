using UnityEngine;

namespace CodeBase
{
    public static class PhysicsDebug
    {
        public static void DrawRays(Vector3 worldPosition, float radius, float seconds)
        {
            Debug.DrawRay(worldPosition, radius * Vector3.up, Color.red, seconds);
            Debug.DrawRay(worldPosition, radius * Vector3.down, Color.red, seconds);
            Debug.DrawRay(worldPosition, radius * Vector3.left, Color.red, seconds);
            Debug.DrawRay(worldPosition, radius * Vector3.right, Color.red, seconds);
            Debug.DrawRay(worldPosition, radius * Vector3.forward, Color.red, seconds);
        }

        public static void DrawBox(Vector3 position, Vector3 size)
        {
            Gizmos.DrawCube(position, size);
        }
    }
}