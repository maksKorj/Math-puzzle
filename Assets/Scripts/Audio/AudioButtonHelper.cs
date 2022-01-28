using UnityEngine;

public class AudioButtonHelper : MonoBehaviour
{
    public void PlaySound()
        => AudioController.Instance.PlaySound(SoundItem.BUTTON);
}
