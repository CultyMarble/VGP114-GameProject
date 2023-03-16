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
    [SerializeField] private float initialEnemyHealth;
    [SerializeField] private float enemyHealthIncreasePerWave;
    [SerializeField] private float initialEnemyMovementSpeed;
    [SerializeField] private float enemySpeedIncreasePerWave;

    private int waveNumber;
    private State state;
    private Vector3 spawnPosition;
    private float nextEnemySpawnTimer;
    private float nextWaveSpawnTimeCounter;
    private int remainingEnemySpawnAmountCounter;
    private readonly float waveSpawnTimeLimit = 1;
    private readonly int enemySpawnAmountLimit = 30;

    private readonly float enemySpawnDelayMin = 0.15f;
    private readonly float enemySpawnDelayMax = 0.45f;
    private readonly float minSpawnRadius = 0.0f;
    private readonly float maxSpawnRadius = 5.0f;

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

            ResourceManager.Instance.AddResource(CurrencyType.Gas, 1);

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

    public Enemy Spawn(Vector3 position)
    {
        Transform enemyTransform = Instantiate(pfEnemy, position, Quaternion.identity, pfEnemyParent);

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

    //===========================================================================
    public void IncreaseNextWaveSpawnTime()
    {
        nextWaveSpawnTime += 1.0f;
    }
}
