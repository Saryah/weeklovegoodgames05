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
    public AudioSource audio;
    public AudioClip clip;
    public float multiplier;
    
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (LevelSettings.instance.difficulty == "Easy")
        {
            multiplier = 1f;
        }
        if (LevelSettings.instance.difficulty == "Normal")
        {
            multiplier = 1.5f;
        }
        if (LevelSettings.instance.difficulty == "Hard")
        {
            multiplier = 2f;
        }
        if (LevelSettings.instance.difficulty == "Nightmare")
        {
            multiplier = 4f;
        }
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
        if (GameManager.instance.gamePaused || GameManager.instance.gameOver)
            return;
        if (!isWaveStarted)
        {
            if (waveCountdown <= 0)
            {
                StartWave();
                waveCountdown = waveTimer;
                audio.clip = clip;
                audio.Play();
            }
            waveCountdown -= Time.deltaTime;
        }
    }
    public void StartWave()
    {
        GameManager.instance.UpdateWave();
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
        shipsToSpawn = (int)(GameManager.instance.level * multiplier);
        Debug.Log(shipsToSpawn + " ships spawned");
    }
}
