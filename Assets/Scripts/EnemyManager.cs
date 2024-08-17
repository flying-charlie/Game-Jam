using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    GameObject m_ship;
    EnemySpawnConfig m_config;
    // Start is called before the first frame update
    void Start()
    {
        m_config = GameObject.FindGameObjectWithTag("config").GetComponent<Config>().enemySpawnCfg;
        m_ship = GameObject.FindGameObjectWithTag("ship");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        DoEnemySpawning();
    }

    void DoEnemySpawning()
    {
        if (Random.Range(0.0F, 1.0F) < m_config.enemySpawnChance)
        {
            Vector2 enemyPosition;
            do
            {
                enemyPosition = new Vector2(Random.Range(-m_config.enemySpawnWidth/2, m_config.enemySpawnWidth/2), Random.Range(-m_config.enemySpawnHeight/2, m_config.enemySpawnHeight/2));
            }
            while ((enemyPosition - (Vector2)m_ship.transform.position).magnitude < m_config.safeZone);
            Instantiate(m_config.enemy, enemyPosition, Quaternion.identity);
        }
    }
}

public struct EnemySpawnConfig
{
    public float safeZone;
    public float enemySpawnWidth;
    public float enemySpawnHeight;
    public float enemySpawnChance;
    public GameObject enemy;
}