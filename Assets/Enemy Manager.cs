using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    Camera m_camera;
    GameObject m_ship;
    // Start is called before the first frame update
    void Start()
    {
        m_camera = Camera.main;
        m_ship = GameObject.FindGameObjectWithTag("ship");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
