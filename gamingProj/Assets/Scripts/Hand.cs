﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    float cooldown = 1;    
    float internalClock = 0;
    float shootingAngle;
    GameObject gun;
    bool isFixed = false;
    Vector3 shootPos;

    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.Find("Knife");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFixed)
        {
            shootingAngle = Mathf.Atan2(Input.GetAxis("R3X"), Input.GetAxis("R3Y")) * Mathf.Rad2Deg;
            shootPos = new Vector3(Input.GetAxis("R3X") * 0.8f + transform.parent.position.x, transform.position.y, Input.GetAxis("R3Y") * 0.8f + transform.parent.position.z);
            transform.rotation = Quaternion.Euler(0, shootingAngle, 0);

            if ((Input.GetAxis("R3X") != 0 || Input.GetAxis("R3Y") != 0) && (internalClock >= cooldown || cooldown == 0))
            {
                gun.SendMessage("Shoot",shootPos);   //Appelle la fonction "Shoot" de tout les composants du gun actuel
                internalClock = 0;
            }
            internalClock += Time.deltaTime;
        }
    }

    /// <summary>
    /// Change l'arme distance active
    /// </summary>
    /// <param name="gun">la nouvelle arme active</param>
    void SetGun(GameObject gun)
    {
        this.gun = gun;
    }

    /// <summary>
    /// Change le cooldown
    /// </summary>
    /// <param name="newCooldown">le nouveau cooldown à appliquer</param>
    void SetCooldown(float newCooldown)
    {
        internalClock = 0;
        cooldown = newCooldown;
    }

    /// <summary>
    /// Bloque la rotation de la main (utilisée pour le couteau)
    /// </summary>
    /// <param name="value">true pour bloquer la rotation de la main</param>
    void SetIsFixed(bool value)
    {
        isFixed = value;
    }
}
