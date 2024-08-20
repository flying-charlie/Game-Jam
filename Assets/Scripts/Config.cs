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
    public Dictionary<string, TileConfig> tileCfg = new(); 
    public EnemySpawnConfig enemySpawnCfg = new(); 
    public shipConfig shipCfg = new();
    //public asteroidSpawnConfig asteroidSpawnCfg = new();
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
    public GameObject bossEnemy;

    public void Awake()
    {
        float tileDespawnTime = 30;

        #region General Config
        enemySpawnCfg = new(){
            safeZone = 10,
            safeZoneScaling = 0.15F,
            enemySpawnHeight = 60,
            enemySpawnWidth = 80,
            spawnScaling = 1.1F,
            initialScale = 60,
            SpawnRates = new Dictionary<GameObject, float>(){
                {basicEnemy, 0.01F},
                {bossEnemy, 0.001F}
            }
        };

        shipCfg = new(){
            rotationScale = 0.01F,
            rotationMin = 0.02F,
            accelerationScale = 1F,
            maxSpeedScale = 10F,
            accelerationMin = 1,
            maxSpeedMin = 0.5F,
            playSpaceHeight = 60,
            playSpaceWidth = 80
        };
        #endregion

        #region Tiles
        tileCfg.Add("core", new TileConfig(){
            maxHealth = 10,
            despawnTime = tileDespawnTime,
        });

        tileCfg.Add("corridor", new TileConfig(){
            maxHealth = 1,
            despawnTime = tileDespawnTime,
        });

        tileCfg.Add("basicThruster", new TileConfig() {
            maxHealth = 3,
            despawnTime = tileDespawnTime,
        });

        tileCfg.Add("basicGun", new TileConfig(){
            maxHealth = 3,
            despawnTime = tileDespawnTime,
        });

        tileCfg.Add("railgun", new TileConfig(){
            maxHealth = 4,
            despawnTime = tileDespawnTime,
        });
                
        tileCfg.Add("laser", new TileConfig(){
            maxHealth = 4,
            despawnTime = tileDespawnTime,
        });
                
        tileCfg.Add("shotgun", new TileConfig(){
            maxHealth = 4,
            despawnTime = tileDespawnTime,
        });
        #endregion

        #region Guns
        gunCfg.Add("basicGun", new GunConfig(){
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
                speed = 14,
                damage = 5,
                duration = 3
            }
        });

        gunCfg.Add("railgun", new GunConfig(){
            damageScale = 1.2f,
            rangeScale = 0.5f,
            reloadScale = 0.1f,
            reloadTime = 5,
            rotationSpeed = 0.3F,
            fireAngleTolerance = 10,
            bulletSpreadAngle = 1,
            numberOfProjectiles = 1,
            bullet = railgunBullet,
            bulletConfig = new BulletConfig(){
                speed = 40,
                damage = 50,
                duration = 3
            }
        });

        gunCfg.Add("laser", new GunConfig(){
            damageScale = 1.2f,
            rangeScale = 0.6f,
            reloadScale = 0f,
            reloadTime = 0.01f,
            rotationSpeed = 0.5F,
            fireAngleTolerance = 10,
            bulletSpreadAngle = 0,
            numberOfProjectiles = 1,
            bullet = laserBullet,
            bulletConfig = new BulletConfig(){
                speed = 70,
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
            bulletSpreadAngle = 40,
            numberOfProjectiles = 20,
            bullet = shotgunBullet,
            bulletConfig = new BulletConfig(){
                speed = 20,
                damage = 0.7f,
                duration = 1.2f
            }
        });
        #endregion

        #region Thrusters
        thrusterCfg.Add("basicThruster", new ThrusterConfig(){
            thrust = 1
        });
        #endregion

        #region Enemies
        enemyCfg.Add("basic", new EnemyConfig(){
            ramDamage = 1,
            maxAcceleration = 1,
            maxspeed = 0.5F,
            rotationalSpeed = 0.2F,
            enemyHealth = 10,
            dropChance = 0.2F,
            dropWeights = new(){}
        });
        enemyCfg["basic"].dropWeights.Add(basicGun, 1.2f);
        enemyCfg["basic"].dropWeights.Add(basicThruster, 2.5f);
        enemyCfg["basic"].dropWeights.Add(corridor, 2.5f);
        enemyCfg["basic"].dropWeights.Add(speedPowerup, 0.1F); // should be more like 0.1
        enemyCfg["basic"].dropWeights.Add(railgunGun, 1f);
        enemyCfg["basic"].dropWeights.Add(laserGun, 1f);
        enemyCfg["basic"].dropWeights.Add(shotgunGun, 1f);

        enemyCfg.Add("firstEnemy", new EnemyConfig(){
            ramDamage = 1,
            maxAcceleration = 1,
            maxspeed = 0.5F,
            rotationalSpeed = 0.2F,
            enemyHealth = 10,
            dropChance = 1F,
            dropWeights = new(){}
        });
        enemyCfg["firstEnemy"].dropWeights.Add(basicGun, 1);

        enemyCfg.Add("bossEnemy", new EnemyConfig(){
            ramDamage = 10,
            maxAcceleration = 1,
            maxspeed = 0.75F,
            rotationalSpeed = 0.2F,
            enemyHealth = 100,
            dropChance = 1F,
            dropWeights = new(){}
        });
        enemyCfg["bossEnemy"].dropWeights.Add(railgunGun, 1f);
        enemyCfg["bossEnemy"].dropWeights.Add(shotgunGun, 1f);
        enemyCfg["bossEnemy"].dropWeights.Add(laserGun, 1f);
        enemyCfg["bossEnemy"].dropWeights.Add(speedPowerup, 1f);
        #endregion

        #region Powerups (temporary)
            powerupCfg.Add("speed", new PowerupConfig(){
                duration = 10,
                multiplier = 2
            });
        #endregion
        
    }
}