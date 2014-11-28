using UnityEngine;
using System.Collections;

public class Utility : MonoBehaviour
{
    public static Vector2 RotateVec2D(Vector2 vec, float rad)
    {
        vec.x = vec.x * Mathf.Cos(rad) - vec.y * Mathf.Sin(rad);
        vec.y = vec.x * Mathf.Sin(rad) + vec.y * Mathf.Cos(rad);

        return vec;
    }

    public static float GetRandomDegInRad(float min = 0, float max = 360)
    {
        min *= Mathf.Deg2Rad;
        max *= Mathf.Deg2Rad;

        return Random.Range(min, max);
    }

    public static Vector2 GetRandUnitVec(float rad)
    {
        Vector2 vec = new Vector2();

        vec.x = Mathf.Cos(rad);
        vec.y = Mathf.Sin(rad);

        return vec.normalized;
    }

}
