using UnityEngine;

public class SoundGiver : MonoBehaviour
{
    [SerializeField] private AudioClip _buttonSound;
    [SerializeField] private AudioClip _chipCollideSound;
    [SerializeField] private AudioClip _symbolGiverSound;
    [SerializeField] private AudioClip _comboSound;
    [SerializeField] private AudioClip _endFlyingSound;
    [SerializeField] private AudioClip _explosionSound;
    [SerializeField] private AudioClip _startEffect;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _loseSound;
    [SerializeField] private AudioClip _selectSound;

    public AudioClip GetAudio(SoundItem soundItems)
    {
        return soundItems switch
        {
            SoundItem.BUTTON => _buttonSound,
            SoundItem.CHIP_COLLIDE => _chipCollideSound,
            SoundItem.SYMBOL_GIVER => _symbolGiverSound,
            SoundItem.COMBO => _comboSound,
            SoundItem.END_FLYING => _endFlyingSound,
            SoundItem.EXPLOSION => _explosionSound,
            SoundItem.JUMP => _startEffect,
            SoundItem.WIN => _winSound,
            SoundItem.LOSE => _loseSound,
            SoundItem.SELECT => _selectSound,
            _ => null,
        };
    }
}

public enum SoundItem
{
    BUTTON,
    CHIP_COLLIDE,
    SYMBOL_GIVER,
    COMBO,
    END_FLYING,
    EXPLOSION,
    JUMP,
    WIN,
    LOSE,
    SELECT
}