using UnityEngine;
using UnityEngine.UI;

public class TabletScaler : MonoBehaviour
{
    public bool IsTablet { get; private set; }

    public void CheckResolution()
    {
        var canvas = GetComponent<CanvasScaler>();
        int divider = GCD(Screen.height, Screen.width);

        if (Screen.height / divider - Screen.width / divider < 2 && Screen.height / Screen.width != 2)
        {
            IsTablet = true;
            canvas.matchWidthOrHeight = 1f;
        }
    }

    private int GCD(int num1, int num2)
    {
        int Remainder;

        while (num2 != 0)
        {
            Remainder = num1 % num2;
            num1 = num2;
            num2 = Remainder;
        }

        return num1;
    }
}
