using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Config : MonoBehaviour
{
    public Dictionary<string, GunConfig> gunCfg = new();
    public Dictionary<string, BulletConfig> bulletCfg = new();
    public Dictionary<string, ThrusterConfig> thrusterCfg = new();
    public Dictionary<string, EnemyConfig> enemyCfg = new();
    public Dictionary<string, PowerupConfig> powerupCfg = new();
    public EnemySpawnConfig enemySpawnCfg = new(); 
    public shipConfig shipCfg = new();
    public GameObject basicBullet;
    public GameObject basicGun;
    public GameObject basicThruster;
    public GameObject corridor;
    public GameObject speedPowerup;
    public GameObject basicEnemy;

    public void Awake()
    {
        #region General Config
        enemySpawnCfg = new(){
            safeZone = 10,
            enemySpawnHeight = 40,
            enemySpawnWidth = 60,
            enemySpawnChance = 0.05F,
            enemy = basicEnemy
        };

        shipCfg = new(){
            rotationScale = 0.01F,
            rotationMin = 0.05F,
            accelerationScale = 1F,
            maxSpeedScale = 10F,
            accelerationMin = 1,
            maxSpeedMin = 0.5F
        };
        #endregion

        #region Guns
        gunCfg.Add("basic", new GunConfig(){
            reloadTime = 1,
            rotationSpeed = 0.5F,
            fireAngleTolerance = 10,
            bullet = basicBullet
        });
        #endregion
        
        #region Bullets
        bulletCfg.Add("basic", new BulletConfig(){
            speed = 5,
            damage = 5,
            duration = 3
        });
        #endregion

        #region Thrusters
        thrusterCfg.Add("basic", new ThrusterConfig(){
            thrust = 1
        });
        #endregion

        #region Enemies
        enemyCfg.Add("basic", new EnemyConfig(){
            maxAcceleration = 1,
            maxspeed = 0.5F,
            rotationalSpeed = 0.2F,
            dropChance = 0.5F,
            dropWeights = new(){}
        });
        enemyCfg["basic"].dropWeights.Add(basicGun, 1);
        enemyCfg["basic"].dropWeights.Add(basicThruster, 1);
        enemyCfg["basic"].dropWeights.Add(corridor, 2);
        enemyCfg["basic"].dropWeights.Add(speedPowerup, 0.1F); // should be more like 0.1

        enemyCfg.Add("firstEnemy", new EnemyConfig(){
            maxAcceleration = 1,
            maxspeed = 0.5F,
            rotationalSpeed = 0.2F,
            dropChance = 1F,
            dropWeights = new(){}
        });
        enemyCfg["firstEnemy"].dropWeights.Add(basicGun, 1);
        #endregion

        #region Powerups (temporary)
            powerupCfg.Add("speed", new PowerupConfig(){
                duration = 10,
                multiplier = 2
            });
        #endregion

    }
}
