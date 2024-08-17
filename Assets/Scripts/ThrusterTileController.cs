using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

public class ThrusterController : TileController
{
    public string configId;
    ThrusterConfig m_config;
    public float thrust {get => m_config.thrust;}

    new void Start()
    {
        m_config = GameObject.FindGameObjectWithTag("config").GetComponent<Config>().thrusterCfg[configId];
        base.Start();
    }
}

public struct ThrusterConfig
{
    public float thrust;
}