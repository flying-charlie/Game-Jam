using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public void RotateTowards(float targetRotation, float speed)
    {
        Utils.RotateTowards(transform, targetRotation, speed);
    }

    public void Fire(GameObject bullet, BulletConfig bulletConfig)
    {
        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
        newBullet.GetComponent<BulletController>().config = bulletConfig;
    }
}
