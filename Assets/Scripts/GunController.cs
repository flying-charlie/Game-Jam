using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class GunController : MonoBehaviour
{
    public void RotateTowards(float targetRotation, float speed)
    {
        Utils.RotateTowards(transform, targetRotation, speed);
    }

    public void Fire(GunConfig config, BulletConfig bulletConfig, int mass)
    {
        for (int i = 0; i < config.numberOfProjectiles; i++)
        {
            GameObject newBullet = Instantiate(config.bullet, transform.position, transform.rotation);
            newBullet.GetComponent<BulletController>().config = bulletConfig;
            newBullet.transform.Rotate(0, 0, UnityEngine.Random.Range(-config.bulletSpreadAngle / 2, config.bulletSpreadAngle / 2));
            newBullet.transform.localScale = new Vector3(Mathf.Pow(mass, 0.3f), Mathf.Pow(mass, 0.3f));
        }
    }
}
