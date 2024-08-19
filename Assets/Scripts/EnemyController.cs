using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator m_animator;
    bool isDead = false;
    float m_health;
    public string configId;
    float m_speed;
    GameObject m_ship;
    EnemyConfig m_config;
    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_config = GameObject.FindGameObjectWithTag("config").GetComponent<Config>().enemyCfg[configId];
        m_ship = GameObject.FindGameObjectWithTag("ship");
        m_health = m_config.enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead && !Utils.AnimatorIsPlaying(m_animator) && m_animator.GetCurrentAnimatorStateInfo(0).IsName("Base.Dieing"))
        {
            DoDrop();
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        DoMovement();
    }
    
    void DoMovement()
    {
        float targetAngle = Utils.GetAngleFromTo(transform.position, m_ship.transform.position);
        Utils.RotateTowards(transform, targetAngle, m_config.rotationalSpeed);

        m_speed = (m_config.maxspeed - m_speed) * m_config.maxAcceleration + m_speed;

        transform.position += transform.right * Time.deltaTime * m_speed;
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("bullet"))
        {
            m_health -= collision2D.gameObject.GetComponent<BulletController>().damage;
            if (m_health <= 0 && isDead == false)
            {
                isDead = true;
                m_animator.SetTrigger("Death");
                GetComponent<CircleCollider2D>().enabled = false;
                GetComponent<Rigidbody2D>().isKinematic = true;
            }
        }
        if (collision2D.gameObject.CompareTag("tile"))
        {
            Destroy(gameObject);
        }
    }

    void DoDrop()
    {
        if (m_config.dropChance > UnityEngine.Random.Range(0F, 1F))
        {
            GameObject droppedItem = Utils.WeightedRandom<GameObject>(m_config.dropWeights);
            Instantiate(droppedItem, transform.position, quaternion.identity);
        }
    }
}

public struct EnemyConfig
{
    public float enemyHealth;
    public float maxAcceleration;
    public float maxspeed;
    public float rotationalSpeed;
    public float dropChance;
    public Dictionary<GameObject, float> dropWeights;
}
