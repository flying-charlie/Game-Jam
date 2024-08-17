using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float ENEMY_SPAWN_WIDTH;
    public float ENEMY_SPAWN_HEIGHT;
    public float ENEMY_SPAWN_CHANCE;
    public GameObject enemy;
    public float safeZone;
    GameObject m_ship;
    // Start is called before the first frame update
    void Start()
    {
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
        if (Random.Range(0.0F, 1.0F) < ENEMY_SPAWN_CHANCE)
        {
            Vector2 enemyPosition;
            do
            {
                enemyPosition = new Vector2(Random.Range(-ENEMY_SPAWN_WIDTH/2, ENEMY_SPAWN_WIDTH/2), Random.Range(-ENEMY_SPAWN_HEIGHT/2, ENEMY_SPAWN_HEIGHT/2));
            }
            while ((enemyPosition - (Vector2)m_ship.transform.position).magnitude < safeZone);
            Instantiate(enemy, enemyPosition, Quaternion.identity);
        }
    }
}
