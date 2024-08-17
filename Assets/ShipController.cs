using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ShipController : MonoBehaviour
{
    Rigidbody2D m_rigidBody;
    Transform m_transform;
    InputSet m_inputs;
    Vector2 m_targetVelocity;
    Vector2 m_velocity;
    float m_speed;
    bool Firing;
    public float MASS;
    public float MAX_ACCELERATION;
    public float MAX_SPEED;
    public float ROTATION_SCALE;
    public float ROTATION_MIN;
    public bool ALTERNATE_MOVEMENT;
    float m_rotationSpeed;
    float m_maxRotationSpeed;
    Vector2 cameraPos = new(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        m_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        m_rotationSpeed = (float)(ROTATION_SCALE / MASS + ROTATION_MIN); //move this to OnMassChange
        m_maxRotationSpeed = (float)(m_rotationSpeed * 90); //move this to OnMassChange
    }

    void FixedUpdate()
    {
        GetInputs();

        if (ALTERNATE_MOVEMENT)
        {
            DoMovement2();
        }
        else
        {
            DoMovement();
        }
        
        
    }

    /// <summary>
    /// Gets user inputs for the frame and updates m_Inputs.
    /// </summary>
    void GetInputs()
    {
        float x = 0;
        float y = 0;
        if (Input.GetKey("a") && !Input.GetKey("d")) {x = -1;}
        else if (Input.GetKey("d") && !Input.GetKey("a")) {x = 1;}
        if (Input.GetKey("s") && !Input.GetKey("w")) {y = -1;}
        else if (Input.GetKey("w") && !Input.GetKey("s")) {y = 1;}

        bool mouseOnScreen;
        if (Input.mousePosition.x > Screen.width || Input.mousePosition.y > Screen.height) {mouseOnScreen = false;}
        else {mouseOnScreen = true;}

        m_inputs = new InputSet(){
            x = x,
            y = y,
            firing = Input.GetKey("space"),
            mouseDirection = GetMouseAngle(),
            mouseOnScreen = mouseOnScreen
            };
    }

    float GetMouseAngle()
    {
        Vector2 shipPos = new(Screen.width / 2, Screen.height / 2);
        Vector2 mousePos = new(Input.mousePosition.x, Input.mousePosition.y);
        
        Vector2 relativeVector = mousePos - shipPos; //get the vector representing the mouse's position relative to the point
        return VectorToAngle(relativeVector);
    }

    float VectorToAngle(Vector2 vector)
    {
        var angleRadians = Mathf.Atan2(vector.y, vector.x); //use atan2 to get the angle; Atan2 returns radians
        return angleRadians * Mathf.Rad2Deg;   //convert to degrees
    }

    /// <summary>
    /// Calculate and perform movement and rotation. Rotation is cosmetic.
    /// </summary>
    void DoMovement()
    {
        m_targetVelocity = new Vector2(m_inputs.x * MAX_SPEED, m_inputs.y * MAX_SPEED);
        m_velocity = Vector2.MoveTowards(m_velocity, m_targetVelocity, MAX_ACCELERATION);

        Firing = m_inputs.firing;

        if (m_velocity != new Vector2(0, 0))
        {
            RotateTowards(VectorToAngle(m_velocity), m_rotationSpeed * 2, m_maxRotationSpeed);  // rotation speed is multiplied by 2 here to compensate the additional velocity lerp
        }
        m_transform.position += new Vector3(m_velocity.x * Time.deltaTime, m_velocity.y * Time.deltaTime);
    }

    /// <summary>
    /// Calculate and perform movement and rotation. Rotation speed affects turning time.
    /// </summary>
    void DoMovement2()
    {
        m_targetVelocity = new Vector2(m_inputs.x * MAX_SPEED, m_inputs.y * MAX_SPEED);
        float targetSpeed = m_inputs.x != 0 || m_inputs.y != 0 ? MAX_SPEED : 0;
        m_speed = (targetSpeed - m_speed) * MAX_ACCELERATION + m_speed;
        Debug.Log((m_targetVelocity, m_speed, m_transform.forward * Time.deltaTime * m_speed));

        Firing = m_inputs.firing; 

        if (m_targetVelocity != new Vector2(0, 0))
        {
            RotateTowards(VectorToAngle(m_targetVelocity), m_rotationSpeed, m_maxRotationSpeed); 
        }
        
        m_transform.position += m_transform.right * Time.deltaTime * m_speed;
    }

    void RotateTowards(float targetRotation, float speed, float maxSpeed)
    {
        float toRotate = targetRotation - m_transform.rotation.eulerAngles.z;
        if (toRotate > 180) {toRotate -= 360;}
        else if (toRotate < -180) {toRotate += 360;}
        toRotate *= speed;
        if (toRotate > maxSpeed)
        {
            toRotate = maxSpeed;
        }
        m_transform.Rotate(Vector3.forward, toRotate);
    }

}

public struct InputSet
{
    public float x;
    public float y;
    public bool firing;
    public float mouseDirection;
    public bool mouseOnScreen;
}
