using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;

    private SoundGiver _soundGiver;

    private static AudioController _instance;
    public static AudioController Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }

        _soundGiver = GetComponent<SoundGiver>();

        _musicSource.volume = SettingsSaver.LoadValue(SettingItem.Music);
        if (_musicSource.volume == 0)
            _musicSource.Stop();
        else
            _musicSource.Play();

        _soundSource.volume = SettingsSaver.LoadValue(SettingItem.Sound);
    }

    public void SetAudioValue(SettingItem settingItem, float value)
    {
        if (settingItem == SettingItem.Music)
            SetMusicVolume(value);
        else
            SetSoundVolume(value);
    }

    private void SetMusicVolume(float value)
    {
        if(_musicSource.volume == 0)
        {
            _musicSource.Play();
        }

        _musicSource.volume = value;
    }

    private void SetSoundVolume(float value) => _soundSource.volume = value;

    public void PlaySound(SoundItem soundItem)
    {
        if (_soundSource.volume == 0)
            return;

        if(_soundSource.isPlaying == false)
            _soundSource.PlayOneShot(_soundGiver.GetAudio(soundItem));
    }

    public void PlayChipJumpSound()
    {
        if (_soundSource.volume == 0)
            return;

        AudioSource.PlayClipAtPoint(_soundGiver.GetAudio(SoundItem.JUMP), transform.position);
    }
}
