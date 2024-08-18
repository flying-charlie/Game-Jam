using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class GunController : MonoBehaviour
{
    public void RotateTowards(float targetRotation, float speed)
    {
        Utils.RotateTowards(transform, targetRotation, speed);
    }

    public void Fire(GunConfig config, BulletConfig bulletConfig)
    {
        for (int i = 0; i < config.numberOfProjectiles; i++)
        {
            GameObject newBullet = Instantiate(config.bullet, transform.position, transform.rotation);
            newBullet.GetComponent<BulletController>().config = bulletConfig;
            newBullet.transform.Rotate(0, 0, Random.Range(-config.bulletSpreadAngle / 2, config.bulletSpreadAngle / 2));
        }
    }
}
