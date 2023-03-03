using System;
using UnityEngine;
using TMPro;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveNumberText;
    [SerializeField] private TextMeshProUGUI waveMessageText;
    [SerializeField] private RectTransform enemyWaveSpawnPositionIndicator;

    private Camera mainCamera;
    private float nextWaveSpawnTimer;

    //===========================================================================
    private void Start()
    {
        mainCamera = Camera.main;

        EnemyWaveManager.Instance.OnWaveNumberChanged += EnemyWaveManager_OnWaveNumberChangedHandler;

        SetWaveNumberText("Wave " + EnemyWaveManager.Instance.GetWaveNumber());
    }

    private void Update()
    {
        MessageTextHandler();
        UpdateSpawnIndicator();
    }

    private void OnDestroy()
    {
        EnemyWaveManager.Instance.OnWaveNumberChanged -= EnemyWaveManager_OnWaveNumberChangedHandler;
    }

    //===========================================================================
    private void MessageTextHandler()
    {
        nextWaveSpawnTimer = EnemyWaveManager.Instance.GetNextWaveSpawnTimeCounter();
        if (nextWaveSpawnTimer <= 0.0f)
        {
            SetMessageText("");
        }
        else
        {
            SetMessageText("Next Wave: " + nextWaveSpawnTimer.ToString("F1") + "s");
        }
    }

    private void SetWaveNumberText(string message)
    {
        waveNumberText.SetText(message);
    }

    private void SetMessageText(string message)
    {
        waveMessageText.SetText(message);
    }

    private void UpdateSpawnIndicator()
    {
        Vector3 directionToNextSpawnPosition = (EnemyWaveManager.Instance.GetSpawnPosition() - mainCamera.transform.position).normalized;
        enemyWaveSpawnPositionIndicator.anchoredPosition = directionToNextSpawnPosition * 600f;
        enemyWaveSpawnPositionIndicator.eulerAngles = new Vector3(0, 0, CultyMarbleHelper.GetAngleFromVector(directionToNextSpawnPosition));

        float distanceToNextSpawnPosition = Vector3.Distance(EnemyWaveManager.Instance.GetSpawnPosition(), mainCamera.transform.position);
        enemyWaveSpawnPositionIndicator.gameObject.SetActive(distanceToNextSpawnPosition > mainCamera.orthographicSize * 1.65f);
    }

    private void EnemyWaveManager_OnWaveNumberChangedHandler(object sender, EventArgs e)
    {
        SetWaveNumberText("Wave " + EnemyWaveManager.Instance.GetWaveNumber());
    }
}