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
    float m_mass;
    shipConfig config;
    public float MAX_ACCELERATION;
    public float MAX_SPEED;
    public float ROTATION_SCALE;
    public float ROTATION_MIN;
    float m_rotationSpeed;
    float m_maxRotationSpeed;
    float m_thrust;
    public float GRID_SCALE;


    // Start is called before the first frame update
    void Start()
    {
        OnMassChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMassChange()
    {
        m_mass = transform.childCount;
        m_speed = 0;
        m_rotationSpeed = (float)(config.rotationScale / m_mass + config.rotationMin);
        m_maxRotationSpeed = (float)(m_rotationSpeed * 90);
    }

    void FixedUpdate()
    {
        GetInputs();
        DoMovement();

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject tile = transform.GetChild(i).gameObject;
            tile.GetComponent<TileController>().attachedUpdate(new TileUpdateData() {
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
    /// Calculate and perform movement and rotation. Rotation speed affects turning time.
    /// </summary>
    void DoMovement()
    {
        m_targetVelocity = new Vector2(m_inputs.x * MAX_SPEED, m_inputs.y * MAX_SPEED);
        float targetSpeed = m_inputs.x != 0 || m_inputs.y != 0 ? MAX_SPEED : 0;
        m_speed = (targetSpeed - m_speed) * MAX_ACCELERATION + m_speed;

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

public struct shipConfig
{
    public float rotationScale;
    public float rotationMin;
}