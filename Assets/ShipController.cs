using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class ShipController : MonoBehaviour
{
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
    public float GRID_SCALE;


    // Start is called before the first frame update
    void Start()
    {
        
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

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject tile = transform.GetChild(i).gameObject;
            tile.GetComponent<TileController>().attachedUpdate(new tileUpdateData() {
                inputs = m_inputs
            });
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
            mouseDirection = Utils.GetMouseAngleFrom(transform.position),
            mouseOnScreen = mouseOnScreen
            };
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
            Utils.RotateTowards(transform, Utils.VectorToAngle(m_velocity), m_rotationSpeed * 2, m_maxRotationSpeed);  // rotation speed is multiplied by 2 here to compensate the additional velocity lerp
        }
        transform.position += new Vector3(m_velocity.x * Time.deltaTime, m_velocity.y * Time.deltaTime);
    }

    /// <summary>
    /// Calculate and perform movement and rotation. Rotation speed affects turning time.
    /// </summary>
    void DoMovement2()
    {
        m_targetVelocity = new Vector2(m_inputs.x * MAX_SPEED, m_inputs.y * MAX_SPEED);
        float targetSpeed = m_inputs.x != 0 || m_inputs.y != 0 ? MAX_SPEED : 0;
        m_speed = (targetSpeed - m_speed) * MAX_ACCELERATION + m_speed;

        Firing = m_inputs.firing; 

        if (m_targetVelocity != new Vector2(0, 0))
        {
            Utils.RotateTowards(transform, Utils.VectorToAngle(m_targetVelocity), m_rotationSpeed, m_maxRotationSpeed); 
        }
        
        transform.position += transform.right * Time.deltaTime * m_speed;
    }

    public bool gridPosHasNeibours(Vector2Int position)
    {
        TileController[] tileScripts = GetComponentsInChildren<TileController>();
        Vector2Int[] neibourPositions = {position + Vector2Int.down, position + Vector2Int.up, position + Vector2Int.left, position + Vector2Int.right};
        foreach (TileController tile in tileScripts)
        {
            if (neibourPositions.Contains((Vector2Int)tile.m_gridPos))
            {
                return true;
            }
        }
        return false;
    }

    public bool gridPosIsFull(Vector2Int position)
    {
        TileController[] tileScripts = GetComponentsInChildren<TileController>();
        foreach (TileController tile in tileScripts)
        {
            if (position == tile.m_gridPos)
            {
                return true;
            }
        }
        return false;
    }
    

    public Vector2 worldPosToGridPos(Vector2 worldPos) // forwards is x
    {
        Debug.Log(transform.InverseTransformPoint(worldPos));
        return transform.InverseTransformPoint(worldPos);
    }
}