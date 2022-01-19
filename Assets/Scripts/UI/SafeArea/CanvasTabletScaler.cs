using UnityEngine;
using UnityEngine.UI;

public class CanvasTabletScaler : MonoBehaviour
{
    [SerializeField] private TabletScaler _tabletScaler;

    private void Start()
    {
        if(_tabletScaler.IsTablet)
            GetComponent<CanvasScaler>().matchWidthOrHeight = 1f;
    }
}
