using UnityEngine;

public static class RectTransformExtension
{
    public static RectTransform Left(this RectTransform rt, float x)
    {
        rt.offsetMin = new Vector2(x, rt.offsetMin.y);
        return rt;
    }

    public static RectTransform Right(this RectTransform rt, float x)
    {
        rt.offsetMax = new Vector2(-x, rt.offsetMax.y);
        return rt;
    }

    public static RectTransform Bottom(this RectTransform rt, float y)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, y);
        return rt;
    }

    public static RectTransform Top(this RectTransform rt, float y)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -y);
        return rt;
    }
}
