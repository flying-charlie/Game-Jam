using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GunTileController : TileController
{   
    public string configId;
    GunController m_gunController;
    GunConfig config;
    bool m_firing = false;
    float timeSinceLastFire;

    new void Start()
    {
        m_gunController = GetComponentInChildren<GunController>();
        config = GameObject.FindGameObjectWithTag("config").GetComponent<Config>().gunCfg[configId];
        base.Start();
    }

    public override void attachedUpdate(TileUpdateData data)
    {
        m_firing = data.inputs.firing;
        DoFiring();
    }

    protected override void OnSizeChange()
    {
        base.OnSizeChange();
        transform.localScale = new Vector3(1, 1);

        transform.GetChild(0).transform.localScale = new Vector3(size.x, size.y);
        GetComponent<BoxCollider2D>().size = new Vector2(size.x, size.y);

        float gunScale = Mathf.Min(size.x, size.y);
        transform.GetChild(1).transform.localScale = new Vector3(gunScale, gunScale);
    }

    void DoFiring()
    {
        float targetAngle = Utils.GetMouseAngleFrom(transform.position);
        m_gunController.RotateTowards(targetAngle, config.rotationSpeed);

        timeSinceLastFire += Time.deltaTime;
        if (Mathf.Abs(((targetAngle + 360) % 360) - m_gunController.transform.rotation.eulerAngles.z) < config.fireAngleTolerance && m_firing && timeSinceLastFire > config.reloadTime)
        {
            Fire();
        }
    }

    void Fire()
    {
        timeSinceLastFire = 0;
        m_gunController.Fire(config.bullet, config.bulletConfig);
    }
}

public struct GunConfig
{
    public float reloadTime;
    public float rotationSpeed;
    public float fireAngleTolerance;
    public GameObject bullet;
    public BulletConfig bulletConfig;
}