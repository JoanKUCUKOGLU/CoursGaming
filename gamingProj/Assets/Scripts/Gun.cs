using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Gun : Weapons
{
    [SerializeField]
    protected GameObject bullet;
    [SerializeField]
    protected float bulletSpeed;
    protected GameObject firedBullet;

    override protected void Shoot(Vector3 shootPos)
    {
        if (isLooted)
        {
            firedBullet = Instantiate(bullet, shootPos,hand.transform.rotation);
            firedBullet.GetComponent<Rigidbody>().AddForce(firedBullet.transform.forward * bulletSpeed);
            Destroy(firedBullet, 2); //Temporaire pour tests
        }
    }
}
