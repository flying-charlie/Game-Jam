using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameObject m_ship;
    public EnemySpawnConfig m_config;
    Dictionary<GameObject, float> SpawnRates;

    // Start is called before the first frame update
    void Start()
    {
        m_config = GameObject.FindGameObjectWithTag("config").GetComponent<Config>().enemySpawnCfg;
        m_ship = GameObject.FindGameObjectWithTag("ship");
        SpawnRates = m_config.SpawnRates;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        ScaleSpawning();
        DoEnemySpawning();
    }

    void ScaleSpawning()
    {

    }

    void DoEnemySpawning()
    {
        foreach (KeyValuePair<GameObject, float> enemyRate in m_config.SpawnRates)
        if (UnityEngine.Random.Range(0.0F, 1.0F) < enemyRate.Value * Time.deltaTime * Mathf.Pow(Time.fixedTime + m_config.initialScale, m_config.spawnScaling))
        {
            Vector2 enemyPosition;
            do
            {
                enemyPosition = new Vector2(UnityEngine.Random.Range(-m_config.enemySpawnWidth/2, m_config.enemySpawnWidth/2), UnityEngine.Random.Range(-m_config.enemySpawnHeight/2, m_config.enemySpawnHeight/2));
            }
            while ((enemyPosition - (Vector2)m_ship.transform.position).magnitude < m_config.safeZone);
            Instantiate(enemyRate.Key, enemyPosition, Quaternion.identity);
        }
    }
}

public struct EnemySpawnConfig
{
    public float safeZone;
    public float safeZoneScaling;
    public float enemySpawnWidth;
    public float enemySpawnHeight;
    public Dictionary<GameObject, float> SpawnRates;
    public float spawnScaling;
    public float initialScale;
}