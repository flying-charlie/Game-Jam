using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Config : MonoBehaviour
{
    public Dictionary<string, GunConfig> gunCfg = new();
    public Dictionary<string, BulletConfig> bulletCfg = new();
    public GameObject basicBullet;

    void Start()
    {
        gunCfg.Add("basic", new GunConfig(){
            reloadTime = 1,
            rotationSpeed = 0.5F,
            fireAngleTolerance = 10,
            bullet = basicBullet
        });

        bulletCfg.Add("basic", new BulletConfig(){
            speed = 5,
            damage = 5
        });
    }
}
