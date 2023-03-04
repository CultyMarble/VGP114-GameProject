using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : SingletonMonobehaviour<GameOverUI>
{
    [SerializeField] private Transform mainMenuUITranform;
    [SerializeField] private Transform tutorialsUITranform;

    [SerializeField] private Button startGameButton;
    [SerializeField] private Button tutorialsButton;
    [SerializeField] private Button backButton;

    //===========================================================================
    protected override void Awake()
    {
        Singleton();

        startGameButton.onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.Scene.GameScene);
        });

        tutorialsButton.onClick.AddListener(() =>
        {
            ShowTutorials();
        });

        tutorialsButton.onClick.AddListener(() =>
        {
            HideTutorials();
        });

        mainMenuUITranform.gameObject.SetActive(true);
        HideTutorials();
    }

    //===========================================================================
    private void ShowTutorials()
    {
        tutorialsUITranform.gameObject.SetActive(true);
    }

    private void HideTutorials()
    {
        tutorialsUITranform.gameObject.SetActive(false);
    }
}
