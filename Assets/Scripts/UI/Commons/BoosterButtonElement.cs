using UnityEngine;

public abstract class BoosterButtonElement : MonoBehaviour
{
    public BoosterItem BoosterItem { get; protected set; }

    public abstract void UpdateAmount();
}
