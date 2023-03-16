using UnityEngine;
using TMPro;

public class PlayerLevelSystem : SingletonMonobehaviour<PlayerLevelSystem>
{
    public class OnPlayerLevelChangedEventAgrs { public int playerLevel; }
    public event System.EventHandler<OnPlayerLevelChangedEventAgrs> OnPlayerLevelChanged;

    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private TextMeshProUGUI playerExpText;
    [SerializeField] private int intialExpToLevelUp;
    [SerializeField] private int expIncreaseToLevelUpPerLevel;

    private int playerLevel;
    private int expToLevelUp;
    private int currentExp;

    //===========================================================================
    protected override void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        playerLevel = 1;
        UpdateExpToLevelUp();
        UpdateLevelText();
        UpdateExpText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            OnPlayerLevelChanged?.Invoke(this, new OnPlayerLevelChangedEventAgrs { playerLevel = playerLevel });
        }

        PlayerLevelUp();
    }

    //===========================================================================
    private void PlayerLevelUp()
    {
        if (currentExp >= expToLevelUp)
        {
            currentExp -= expToLevelUp;
            ++playerLevel;

            UpdateExpToLevelUp();
            UpdateLevelText();
            UpdateExpText();

            OnPlayerLevelChanged?.Invoke(this, new OnPlayerLevelChangedEventAgrs { playerLevel = playerLevel });
        }
    }

    public void IncreaseExp(int amount)
    {
        currentExp += amount;
        UpdateExpText();
    }

    private void UpdateExpToLevelUp()
    {
        expToLevelUp = intialExpToLevelUp + expIncreaseToLevelUpPerLevel * playerLevel;
    }

    private void UpdateLevelText()
    {
        playerLevelText.SetText("Level: " + playerLevel);
    }

    private void UpdateExpText()
    {
        playerExpText.SetText("(" + currentExp + "/" + expToLevelUp + ")");
    }
}