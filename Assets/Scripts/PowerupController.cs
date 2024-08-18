using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public abstract class basePowerupController : MonoBehaviour
{
    public string configId;
    protected PowerupConfig m_config;
    protected ShipController m_shipController;
    // Start is called before the first frame update
    void Start()
    {
        m_config = GameObject.FindGameObjectWithTag("config").GetComponent<Config>().powerupCfg[configId];
        m_shipController = GameObject.FindGameObjectWithTag("ship").GetComponent<ShipController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected abstract void Activate();

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("tile"))
        {
            Activate();
            Destroy(gameObject);
        }
    }
}

public struct PowerupConfig
{
    public float duration;
    public float? multiplier;
}