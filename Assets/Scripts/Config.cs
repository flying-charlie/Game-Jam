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
    public GameObject railgunBullet;
    public GameObject laserBullet;
    public GameObject shotgunBullet;
    public GameObject basicGun;
    public GameObject railgunGun;
    public GameObject laserGun;
    public GameObject shotgunGun;
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
            damageScale = 1.1f,
            rangeScale = 0.6f,
            reloadScale = 0.1f,
            reloadTime = 1,
            rotationSpeed = 0.5F,
            fireAngleTolerance = 10,
            bulletSpreadAngle = 5,
            numberOfProjectiles = 1,
            bullet = basicBullet,
            bulletConfig = new BulletConfig(){
                speed = 7,
                damage = 5,
                duration = 3
            }
        });

        gunCfg.Add("railgun", new GunConfig(){
            damageScale = 1.1f,
            rangeScale = 0.5f,
            reloadScale = 0.9f,
            reloadTime = 5,
            rotationSpeed = 0.3F,
            fireAngleTolerance = 10,
            bulletSpreadAngle = 1,
            numberOfProjectiles = 1,
            bullet = railgunBullet,
            bulletConfig = new BulletConfig(){
                speed = 35,
                damage = 50,
                duration = 3
            }
        });

        gunCfg.Add("laser", new GunConfig(){
            damageScale = 1.2f,
            rangeScale = 0.5f,
            reloadScale = 0f,
            reloadTime = 0.01f,
            rotationSpeed = 0.5F,
            fireAngleTolerance = 10,
            bulletSpreadAngle = 0,
            numberOfProjectiles = 1,
            bullet = railgunBullet,
            bulletConfig = new BulletConfig(){
                speed = 100,
                damage = 0.1f,
                duration = 0.1f
            }
        });

        gunCfg.Add("shotgun", new GunConfig(){
            damageScale = 1.3f,
            rangeScale = 0.6f,
            reloadScale = -0.1f,
            reloadTime = 2,
            rotationSpeed = 0.7F,
            fireAngleTolerance = 10,
            bulletSpreadAngle = 60,
            numberOfProjectiles = 20,
            bullet = railgunBullet,
            bulletConfig = new BulletConfig(){
                speed = 10,
                damage = 0.7f,
                duration = 1.2f
            }
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
            enemyHealth = 10,
            dropChance = 0.5F,
            dropWeights = new(){}
        });
        enemyCfg["basic"].dropWeights.Add(basicGun, 1);
        enemyCfg["basic"].dropWeights.Add(basicThruster, 1);
        enemyCfg["basic"].dropWeights.Add(corridor, 2);
        enemyCfg["basic"].dropWeights.Add(speedPowerup, 0.1F); // should be more like 0.1
        enemyCfg["basic"].dropWeights.Add(railgunGun, 1);
        enemyCfg["basic"].dropWeights.Add(laserGun, 1);
        enemyCfg["basic"].dropWeights.Add(shotgunGun, 1);

        enemyCfg.Add("firstEnemy", new EnemyConfig(){
            maxAcceleration = 1,
            maxspeed = 0.5F,
            rotationalSpeed = 0.2F,
            enemyHealth = 10,
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
