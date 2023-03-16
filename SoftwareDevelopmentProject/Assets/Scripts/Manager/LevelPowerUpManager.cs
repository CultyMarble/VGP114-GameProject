using UnityEngine;
using UnityEngine.UI;

public class LevelPowerUpManager : MonoBehaviour
{
    [SerializeField] private PlayerLevelSystem player;
    [SerializeField] private Transform LevelUpUI;
    [SerializeField] private SO_LevelPowerUpList powerUpList;

    [SerializeField] private Button firstButton;
    [SerializeField] private Button secondButton;
    [SerializeField] private Button thirstButton;

    //===========================================================================
    private void Start()
    {
        LevelUpUI.gameObject.SetActive(false);

        firstButton.onClick.AddListener(() =>
        {
            player.GetComponent<PlayerControl>().DecreaseFireRate();
            LevelUpUI.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        });

        secondButton.onClick.AddListener(() =>
        {
            player.GetComponent<PlayerControl>().IncreaseMovementSpeed();
            LevelUpUI.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        });

        thirstButton.onClick.AddListener(() =>
        {
            EnemyWaveManager.Instance.IncreaseNextWaveSpawnTime();
            LevelUpUI.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        });
    }

    private void OnEnable()
    {
        player.OnPlayerLevelChanged += Instance_OnPlayerLevelChangedHandler;
    }

    private void OnDisable()
    {
        player.OnPlayerLevelChanged -= Instance_OnPlayerLevelChangedHandler;
    }

    //===========================================================================
    private void Instance_OnPlayerLevelChangedHandler(object sender, PlayerLevelSystem.OnPlayerLevelChangedEventAgrs e)
    {
        int _index = 0;
        foreach (Transform child in LevelUpUI.transform)
        {
            child.GetComponent<PowerUpPanel>().powerName.SetText(powerUpList.ListOfLevelPowerUp[_index].powerName);
            child.GetComponent<PowerUpPanel>().powerDiscription.SetText(powerUpList.ListOfLevelPowerUp[_index].discription);
            ++_index;
        }

        Time.timeScale = 0.0f;
        LevelUpUI.gameObject.SetActive(true);
    }
}
