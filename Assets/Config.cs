using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Config : MonoBehaviour
{
    public Dictionary<string, GunConfig> gunCfg = new();
    public Dictionary<string, BulletConfig> bulletCfg = new();
    public shipConfig shipCfg;
    public GameObject basicBullet;

    public Config()
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

        shipCfg = new(){
            rotationScale = 0.5F,
            rotationMin = 0.2F,
            accelerationScale = 0.1F,
            maxSpeedScale = 0.2F,
            accelerationMin = 1,
            maxSpeedMin = 0.5F,
        };
    }
}
