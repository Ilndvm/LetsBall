using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Button resetButton;
    [SerializeField] private LevelSelectUI levelSelectUI;

    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;   
    [SerializeField] private Slider musicSlider;   
    [SerializeField] private Slider soundFXSlider;   
    [SerializeField] private Slider UISlider;
    [Header("Buttons")]
    [SerializeField] private Image masterButton;
    [SerializeField] private Image musicButton;
    [SerializeField] private Image soundFXButton;
    [SerializeField] private Image UIButton;
    [Header("ButtonImages")]
    [SerializeField] private Sprite nullVolume;
    [SerializeField] private Sprite lowVolume;
    [SerializeField] private Sprite fullVolume;
    [Header("ButtonFlags")]
    bool isMasterButtonOn = true;
    bool isMusicButtonOn = true;
    bool isSoundFXButtonOn = true;
    bool isUIButtonOn = true;

    void Awake()
    {
        // Always remove any old listeners, then add ours
        resetButton.onClick.RemoveAllListeners();
        resetButton.onClick.AddListener(OnResetClicked);
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            LoadMusicSettings();
        }
        
        SetMasterVolume();
        SetMusicVolume();
        SetSoundFXVolume();
        SetUIVolume();
    }


    private void OnResetClicked()
    {
        ProgressManager.Instance.ResetProgress();

        // If you have a LevelSelectUI already in the scene, refresh it:
        if (levelSelectUI != null) 
        {
            levelSelectUI.LoadButtons();
        }
    }


    #region Audio Settings
    public void SetMasterVolume()
    {
        float level = masterSlider.value;
        audioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f );

        PlayerPrefs.SetFloat("masterVolume", level);

        isMasterButtonOn = SetVolumeButton(masterSlider, masterButton);
    }
    public void SetMusicVolume()
    {
        float level = musicSlider.value;
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);

        PlayerPrefs.SetFloat("musicVolume", level);

        isMusicButtonOn = SetVolumeButton(musicSlider, musicButton);
    }
    public void SetSoundFXVolume()
    {
        float level = soundFXSlider.value;
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(level) * 20f);

        PlayerPrefs.SetFloat("soundFXVolume", level);

        isSoundFXButtonOn = SetVolumeButton(soundFXSlider, soundFXButton);
    }
    public void SetUIVolume()
    {
        float level = UISlider.value;
        audioMixer.SetFloat("UIVolume", Mathf.Log10(level) * 20f);

        PlayerPrefs.SetFloat("UIVolume", level);

        isUIButtonOn = SetVolumeButton(UISlider, UIButton);
    }
    private void SetVolume(float level, string name)
    {
        audioMixer.SetFloat(name, Mathf.Log10(level) * 20f);
        PlayerPrefs.SetFloat(name, level);
    }

    public void ToggleMasterButton()
    {
        if (isMasterButtonOn)
        {
            SetVolume(0.002f, "masterVolume");
            masterButton.sprite = nullVolume;
            isMasterButtonOn = false;
        }
        else
        {
            SetMasterVolume();
            SetVolumeButton(masterSlider, masterButton);
            isMasterButtonOn = true;
        }

    }
    public void ToggleMusicButton()
    {
        if (isMusicButtonOn)
        {
            SetVolume(0.002f, "musicVolume");
            musicButton.sprite = nullVolume;
            isMusicButtonOn = false;
        }
        else
        {
            SetMusicVolume();
            SetVolumeButton(musicSlider, musicButton);
            isMusicButtonOn = true;
        }
    }
    public void ToggleSoundFXButton()
    {
        if (isSoundFXButtonOn)
        {
            SetVolume(0.002f, "soundFXVolume");
            soundFXButton.sprite = nullVolume;
            isSoundFXButtonOn = false;
        }
        else
        {
            SetSoundFXVolume();
            SetVolumeButton(soundFXSlider, soundFXButton);
            isSoundFXButtonOn = true;
        }
    }
    public void ToggleUIButton()
    {
        if (isUIButtonOn)
        {
            SetVolume(0.002f, "UIVolume");
            UIButton.sprite = nullVolume;
            isUIButtonOn = false;
        }
        else
        {
            SetUIVolume();
            SetVolumeButton(UISlider, UIButton);
            isUIButtonOn = true;
        }
    }

    private void LoadMusicSettings()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        soundFXSlider.value = PlayerPrefs.GetFloat("soundFXVolume");
        UISlider.value = PlayerPrefs.GetFloat("UIVolume");
    }
    private bool SetVolumeButton(Slider slider, Image image) //change image of desired sound button
    {
        if (slider.value <= 0.003)
        {
            image.sprite = nullVolume;
            return false;
        }
        else if (slider.value >= 0.5)
        {
            image.sprite = fullVolume;
            return true;
        }
        else
        {
            image.sprite = lowVolume;
            return true;
        }
    }
    #endregion
}
