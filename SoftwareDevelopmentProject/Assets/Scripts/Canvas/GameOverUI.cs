using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : SingletonMonobehaviour<GameOverUI>
{
    [SerializeField] private Transform gameOverUI;
    [SerializeField] private TextMeshProUGUI waveSurvivedText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainMenuButton;

    //===========================================================================
    protected override void Awake()
    {
        Singleton();
        Hide();

        retryButton.onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.EnumScene.GameScene);
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            GameSceneManager.Load(GameSceneManager.EnumScene.MainMenuScene);
        });
    }

    //===========================================================================
    public void Show()
    {
        waveSurvivedText.SetText("You Survived " + EnemyWaveManager.Instance.GetWaveNumber() + "Waves");

        gameOverUI.gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameOverUI.gameObject.SetActive(false);
    }
}
