using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GunTileController : TileController
{   
    public GameObject bullet;
    GunController m_gunController;
    public float fireAngleTolerance;
    public GunConfig config;
    bool m_firing = false;
    float timeSinceLastFire;

    new void Start()
    {
        m_gunController = GetComponentInChildren<GunController>();
        m_gunController.bullet = bullet;
        config.rotationSpeed = 0.5F;
        config.reloadTime = 1;
        base.Start();
    }

    public override void attachedUpdate(tileUpdateData data)
    {
        m_firing = data.inputs.firing;
        DoFiring();
    }

    void DoFiring()
    {
        float targetAngle = Utils.GetMouseAngleFrom(transform.position);
        m_gunController.RotateTowards(targetAngle, config.rotationSpeed);

        timeSinceLastFire += Time.deltaTime;
        if (Mathf.Abs(((targetAngle + 360) % 360) - m_gunController.transform.rotation.eulerAngles.z) < fireAngleTolerance && m_firing && timeSinceLastFire > config.reloadTime)
        {
            Fire();
        }
    }

    void Fire()
    {
        timeSinceLastFire = 0;
        m_gunController.Fire();
    }
}

public struct GunConfig
{
    public float reloadTime;
    public float rotationSpeed;
}