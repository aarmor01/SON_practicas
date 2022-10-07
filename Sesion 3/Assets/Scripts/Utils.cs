using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
     public static IEnumerable<Vector3> EvaluateSlerpPoints(Vector3 start, Vector3 end, Vector3 center, int count = 10)
    {
        var f = 1f / count;

        for (var i = 0f; i < 1 + f; i += f)
            yield return Slerp(start, end, center, i);
    }

    public static Vector3 Slerp(Vector3 start, Vector3 end, Vector3 center, float t)
    {
        var startRelativeCenter = start - center;
        var endRelativeCenter = end - center;

        return Vector3.Slerp(startRelativeCenter, endRelativeCenter, t) + center;
    }

    public static Vector2 GetInverted(this Vector2 vector2) => new Vector2(vector2.y, vector2.x);
}
