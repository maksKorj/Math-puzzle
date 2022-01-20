using UnityEngine;

public abstract class FreeButton : MonoBehaviour
{
    public void GetFree()
    {
        Debug.Log("Show Ads");
        Give();
    }

    protected abstract void Give();
}
