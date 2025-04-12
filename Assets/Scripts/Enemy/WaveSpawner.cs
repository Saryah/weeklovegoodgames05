using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner instance;
    public GameObject shipPrefab;
    public List<Transform> spawnPoints = new List<Transform>();
    public int shipsToSpawn;
    public bool isWaveStarted;
    public float waveTimer, waveCountdown;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waveCountdown = 10f;
        isWaveStarted = false;
        Transform[] pointsToAdd = GroundGeneration.instance.spawnPointHolder.GetComponentsInChildren<Transform>();
        foreach (var points in pointsToAdd)
        {
            spawnPoints.Add(points);
        }
    }

    void Update()
    {
        if (!isWaveStarted)
        {
            if (waveCountdown <= 0)
            {
                StartWave();
                waveCountdown = waveTimer;
            }
            waveCountdown -= Time.deltaTime;
        }
    }
    public void StartWave()
    {
        ShipsToSpawn();
        SpawnShip();
        isWaveStarted = true;
    }

    public void SpawnShip()
    {
        for (int i = 0; i < shipsToSpawn; i++)
        {
            int rand = UnityEngine.Random.Range(0, spawnPoints.Count);
            shipPrefab.GetComponent<SpaceShip>().OriginPoint(spawnPoints[rand]);
            Instantiate(shipPrefab, spawnPoints[rand].position, Quaternion.identity);
        }
    }

    public void ShipsToSpawn()
    {
        shipsToSpawn = (int)(GameManager.instance.level * 1.5f);
        Debug.Log(shipsToSpawn + " ships spawned");
    }
}
