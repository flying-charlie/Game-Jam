using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bullet;
    
    public void RotateTowards(float targetRotation, float speed)
    {
        Utils.RotateTowards(transform, targetRotation, speed);
    }

    public void Fire()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation);
    }
}
