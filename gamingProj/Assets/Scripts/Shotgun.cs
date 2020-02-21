﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapons
{
    [SerializeField]
    protected GameObject bullet;
    [SerializeField]
    protected float bulletSpeed = 10;
    [SerializeField]
    protected int bulletNumber = 7;
    AudioSource audio ;
/// <summary>
/// Awake is called when the script instance is being loaded.
/// </summary>
void Awake()
{
    audio = GetComponent<AudioSource>();
    audio.enabled = false;
}

    override protected void Shoot(Vector3 shootPos)
    {
        if (isLooted)
        {
            audio.enabled = true;
            SprayShoot(bullet, bulletNumber, bulletSpeed, 3, shootPos, Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized, Vector3.up, 40, 1.5f);
            audio.Play();
        }
    }
}
