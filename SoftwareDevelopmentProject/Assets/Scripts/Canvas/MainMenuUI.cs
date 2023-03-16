using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : SingletonMonobehaviour<GameOverUI>
{
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject tutorialsUI;

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
            ShowTutorials(true);
        });

        backButton.onClick.AddListener(() =>
        {
            ShowTutorials(false);
        });

        mainMenuUI.SetActive(true);
        ShowTutorials(false);
    }

    //===========================================================================
    private void ShowTutorials(bool state)
    {
        tutorialsUI.SetActive(state);
    }
}
