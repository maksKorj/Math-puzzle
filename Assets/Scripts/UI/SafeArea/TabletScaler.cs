using UnityEngine;
using UnityEngine.UI;

public class TabletScaler : MonoBehaviour
{
    public bool IsTablet { get; private set; }

    public void CheckResolution()
    {
        var canvas = GetComponent<CanvasScaler>();
        int divider = Screen.height.GCD(Screen.width);

        if (Screen.height / divider - Screen.width / divider < 2 && Screen.height / Screen.width != 2)
        {
            IsTablet = true;
            canvas.matchWidthOrHeight = 1f;
        }
    }
}
