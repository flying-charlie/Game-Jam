using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float m_speed;
    Vector2 m_targetVelocity;
    public float MAX_ACCELERATION;
    public float MAX_SPEED;
    public float ROTATIONAL_SPEED;
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
        DoMovement();
    }
    
    void DoMovement()
    {
        float targetAngle = Utils.GetAngleFromTo(transform.position, m_ship.transform.position);
        Utils.RotateTowards(transform, targetAngle, ROTATIONAL_SPEED);

        m_targetVelocity = new Vector2(MAX_SPEED,MAX_SPEED);
        m_speed = (MAX_SPEED - m_speed) * MAX_ACCELERATION + m_speed;

        transform.position += transform.right * Time.deltaTime * m_speed;
    }
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        Destroy(gameObject);
    }
}
