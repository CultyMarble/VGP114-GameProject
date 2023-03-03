using System;
using UnityEngine;

public class EnemyWaveManager : SingletonMonobehaviour<EnemyWaveManager>
{
    public event EventHandler OnWaveNumberChanged;

    private enum State
    {
        WaitingToSpawnNextWave,
        SpawningWave,
    }

    [SerializeField] private Transform pfEnemy;
    [SerializeField] private Transform pfEnemyParent;
    [SerializeField] private Transform spawnPositionList;
    [SerializeField] private Transform nextWaveSpawnPositionTransform;

    [SerializeField] private float nextWaveSpawnTime;
    [SerializeField] private float waveSpawnTimeReducePerWave;
    [SerializeField] private int initialEnemySpawnAmount;
    [SerializeField] private int enemyAmountIncreasePerWave;

    private int waveNumber;
    private State state;
    private Vector3 spawnPosition;
    private float nextEnemySpawnTimer;
    private float nextWaveSpawnTimeCounter;
    private int remainingEnemySpawnAmountCounter;
    private float waveSpawnTimeLimit = 1;
    private int enemySpawnAmountLimit = 30;

    private float enemySpawnDelayMin = 0.15f;
    private float enemySpawnDelayMax = 0.45f;
    private float minSpawnRadius = 0.0f;
    private float maxSpawnRadius = 10.0f;

    //===========================================================================
    protected override void Awake()
    {
        Singleton();
    }

    void Start()
    {
        state = State.WaitingToSpawnNextWave;

        int index = UnityEngine.Random.Range(0, spawnPositionList.childCount);
        spawnPosition = new Vector3(spawnPositionList.GetChild(index).position.x, spawnPositionList.GetChild(index).position.y);
        nextWaveSpawnPositionTransform.position = spawnPosition;

        nextWaveSpawnTimeCounter = nextWaveSpawnTime;
    }

    void Update()
    {
        switch(state)
        {
            case State.WaitingToSpawnNextWave:
                WaitingToSpawnNextWave();
                break;

            case State.SpawningWave:
                SpawnWaveGradually();
                break;
        }
    }

    //===========================================================================
    private void WaitingToSpawnNextWave()
    {
        nextWaveSpawnTimeCounter -= Time.deltaTime;

        if (nextWaveSpawnTimeCounter < 0f)
        {
            SpawnWave();
            waveNumber++;
            OnWaveNumberChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void SpawnWave()
    {
        // Pick new spawnPos
        int _index = UnityEngine.Random.Range(0, spawnPositionList.childCount);
        spawnPosition = new Vector3(spawnPositionList.GetChild(_index).position.x, spawnPositionList.GetChild(_index).position.y);
        nextWaveSpawnPositionTransform.position = spawnPosition;

        UpdateRemainingEnemySpawnAmountCounter();

        state = State.SpawningWave;
    }

    private void UpdateRemainingEnemySpawnAmountCounter()
    {
        remainingEnemySpawnAmountCounter = initialEnemySpawnAmount + enemyAmountIncreasePerWave * waveNumber;

        if (remainingEnemySpawnAmountCounter > enemySpawnAmountLimit)
            remainingEnemySpawnAmountCounter = enemySpawnAmountLimit;
    }

    private void UpdateNextWaveSpawnTimeCounter()
    {
        nextWaveSpawnTimeCounter = nextWaveSpawnTime - waveSpawnTimeReducePerWave * waveNumber;

        if (nextWaveSpawnTimeCounter < waveSpawnTimeLimit)
            nextWaveSpawnTimeCounter = waveSpawnTimeLimit;
    }

    private void SpawnWaveGradually()
    {
        if (remainingEnemySpawnAmountCounter > 0)
        {
            nextEnemySpawnTimer -= Time.deltaTime;
            if (nextEnemySpawnTimer < 0f)
            {
                nextEnemySpawnTimer = UnityEngine.Random.Range(enemySpawnDelayMin, enemySpawnDelayMax);
                Spawn(spawnPosition + CultyMarbleHelper.GetRamdomDirection() * UnityEngine.Random.Range(minSpawnRadius, maxSpawnRadius));

                remainingEnemySpawnAmountCounter--;

                if (remainingEnemySpawnAmountCounter <= 0)
                {
                    state = State.WaitingToSpawnNextWave;
                    UpdateNextWaveSpawnTimeCounter();
                }
            }
        }
    }

    public Enemy Spawn(Vector3 positition)
    {
        Transform enemyTransform = Instantiate(pfEnemy, positition, Quaternion.identity, pfEnemyParent);

        Enemy enemy = enemyTransform.GetComponent<Enemy>();
        return enemy;
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public float GetNextWaveSpawnTimeCounter()
    {
        return nextWaveSpawnTimeCounter;
    }

    public Vector3 GetSpawnPosition()
    {
        return spawnPosition;
    }
}
