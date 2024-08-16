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
    InputSet m_Inputs;
    Vector2 m_AimVelocity;
    Vector2 Velocity;
    bool Firing;
    public float MAX_ACCELERATION;
    public float MAX_SPEED;
    Vector2 cameraPos = new(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        GetInputs();

        DoMovement();
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

        m_Inputs = new InputSet(){
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
        
        Vector2 relativeVector = mousePos - shipPos; //get the vector representing the mouse's position relative to the point...
        var angleRadians=Mathf.Atan2(relativeVector.y, relativeVector.x); //use atan2 to get the angle; Atan2 returns radians
        return angleRadians * Mathf.Rad2Deg + 90;   //convert to degrees
    }

    void DoMovement()
    {
        m_AimVelocity = new Vector2(m_Inputs.x * MAX_SPEED, m_Inputs.y * MAX_SPEED);
        m_rigidBody.velocity = Vector2.MoveTowards(m_rigidBody.velocity, m_AimVelocity, MAX_ACCELERATION);
        Firing = m_Inputs.firing;
        if (m_Inputs.mouseOnScreen)
        {
            float toRotate = m_Inputs.mouseDirection - m_transform.rotation.eulerAngles.z;
            m_transform.Rotate(Vector3.forward, toRotate, Space.World); // Space.World
        }
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
