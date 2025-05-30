using UnityEngine;
using static AudioManager;
using UnityEngine.Rendering;
public class AudioManager : MonoBehaviour
{
    #region Instance
    public static AudioManager Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
    #endregion

    [SerializeField] private AudioSource soundFXObject;
    [SerializeField] private AudioSource soundUIObject;

    public enum Sound {
        BoosterPad,
        GravityPad,
        GrowPad,
        ShrinkPad,
        JumpPad,
        SplitterPad,
        KillPad,
        BlockPlaced,
        BlockRemoved,
        BlockChoice,
        FinishPoint,
        StartPoint,
        ButtonSelect,
        ButtonCancel,
        Collectible
    }

    #region soundAudioClipArray
    [SerializeField] private SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip {
        public Sound sound;
        public AudioClip audioClip;
    }
    #endregion


    public void PlaySound(Sound sound)
    {
        AudioSource audioSource = Instantiate(soundFXObject, this.transform);

        audioSource.clip = GetAudioClip(sound);
        audioSource.volume = 1;
        audioSource.Play();
        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlaySound(Sound sound, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, this.transform);

        audioSource.clip = GetAudioClip(sound);
        audioSource.volume = volume;
        audioSource.Play();
        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
    public void PlayUISound(Sound sound)
    {
        AudioSource audioSource = Instantiate(soundUIObject, this.transform);

        audioSource.clip = GetAudioClip(sound);
        audioSource.volume = 1;
        audioSource.Play();
        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }

    public void PlaySound(AudioClip clip)
    {
        // Create a temporary AudioSource
        AudioSource audioSource = Instantiate(soundUIObject, this.transform);
        audioSource.clip = clip;
        audioSource.volume = 1f;
        audioSource.Play();

        // Destroy the temporary object after the clip finishes
        Destroy(audioSource.gameObject, clip.length);
    }
    public AudioClip GetAudioClip(Sound sound) {

        foreach (SoundAudioClip soundAudioClip in soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " is not found");
        return null;
    }
}
