using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int DESPAWN_TIME;
    int despawnTime;
    public BulletConfig config;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        DoMovement();
        despawnTime += 1;
        if (despawnTime == DESPAWN_TIME)
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
        Destroy(gameObject);
    }
}

public struct BulletConfig
{
    public float speed;
}
