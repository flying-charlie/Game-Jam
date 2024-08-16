using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;
    public float JUMP_FORCE;
    public float MAX_SPEED;
    public float ACCELERATION;
    public float FRICTION_GROUND;
    public float FRICTION_AIR;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            m_Rigidbody.AddForce(new Vector2(0, JUMP_FORCE));
        }
        if (Input.GetKey("a") && Input.GetKey("d"))
        {

        }
        else if (Input.GetKey("a") && m_Rigidbody.velocity.x > -MAX_SPEED)
        {
            m_Rigidbody.AddForce(new Vector2(-ACCELERATION, 0));
        }
        else if (Input.GetKey("d") && m_Rigidbody.velocity.x < MAX_SPEED)
        {
            m_Rigidbody.AddForce(new Vector2(ACCELERATION, 0));
        }
    }
}
