using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    BulletConfig config;
    public string configId;

    // Start is called before the first frame update
    void Start()
    {
        config = GameObject.FindGameObjectWithTag("config").GetComponent<Config>().bulletCfg[configId];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        DoMovement();     
    }

    void DoMovement()
    {
        transform.position += transform.right * Time.deltaTime * config.speed;  
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        Destroy(gameObject);
    }
}

public struct BulletConfig
{
    public float speed;
    public float damage;
}
