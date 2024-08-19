using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float health;
    float m_speed;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(Vector3.forward, Random.Range((float)0, (float)360));
        m_speed = Random.Range(0.1F, 2F);
        health = Random.Range(1, 100);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        transform.position += transform.right * Time.deltaTime * m_speed;
    }

    public void OnHealthChange()
    {
        if (health <= 0)
        {
            GameObject.FindGameObjectWithTag("scoreManager").GetComponent<ScoreManager>().IncreaseScore(100);
        }
        Destroy(gameObject);
    }
}