using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionUI : MonoBehaviour
{
    [SerializeField] private Transform optionUI;

    [SerializeField] private SoundEffectManager soundEffectManager;
    [SerializeField] private MusicManager musicManager;

    [SerializeField] private Button soundIncButton;
    [SerializeField] private Button soundDecButton;
    [SerializeField] private Button musicIncButton;
    [SerializeField] private Button musicDecButton;
    [SerializeField] private Button mainMenuButton;

    [SerializeField] private TextMeshProUGUI currentSoundVolumeText;
    [SerializeField] private TextMeshProUGUI currentMusicVolumeText;

    //===========================================================================
    private void Start()
    {
        soundIncButton.onClick.AddListener(() =>
        {
            soundEffectManager.IncreaseVolume();
            UpdateText();
        });

        soundDecButton.onClick.AddListener(() =>
        {
            soundEffectManager.DecreaseVolume();
            UpdateText();
        });

        musicIncButton.onClick.AddListener(() =>
        {
            musicManager.IncreaseVolume();
            UpdateText();
        });

        musicDecButton.onClick.AddListener(() =>
        {
            musicManager.DecreaseVolume();
            UpdateText();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1.0f;
            GameSceneManager.Load(GameSceneManager.Scene.MainMenuScene);
        });

        UpdateText();
        optionUI.gameObject.SetActive(false);
    }

    //===========================================================================
    private void UpdateText()
    {
        currentSoundVolumeText.SetText(Mathf.RoundToInt(soundEffectManager.GetVolume() * 100).ToString());
        currentMusicVolumeText.SetText(Mathf.RoundToInt(musicManager.GetVolume() * 100).ToString());
    }

    public void ToggleOptionUIVisibility()
    {
        if (optionUI.gameObject.activeSelf == true)
        {
            optionUI.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
        else
        {
            optionUI.gameObject.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
}