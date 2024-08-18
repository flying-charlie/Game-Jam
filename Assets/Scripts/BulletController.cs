using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    float despawnTime;
    public BulletConfig config;

    // Start is called before the first frame update
    void Start()
    {
        // config = GameObject.FindGameObjectWithTag("config").GetComponent<Config>().bulletCfg[configId];
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        DoMovement();
        despawnTime += Time.deltaTime;
        if (despawnTime > config.duration)
        {
            Destroy(gameObject);
        }     
    }

    void DoMovement()
    {
        transform.position += transform.right * Time.deltaTime * config.speed;  
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (!collision2D.gameObject.CompareTag("powerup") && !collision2D.gameObject.CompareTag("tile"))
        {
            Destroy(gameObject);
        }
    }
}

public struct BulletConfig
{
    public float speed;
    public float damage;
    public float duration;
}
