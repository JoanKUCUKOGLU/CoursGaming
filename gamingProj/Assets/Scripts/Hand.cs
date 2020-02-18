using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    float cooldown = 1;    
    float internalClock = 0;
    GameObject gun;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && internalClock >= cooldown)
        {
            gun.SendMessage("Shoot");
            internalClock = 0;
        }
        internalClock += Time.deltaTime;
    }

    /// <summary>
    /// Change l'arme distance active
    /// </summary>
    /// <param name="gun">la nouvelle arme active</param>
    void SetGun(GameObject gun)
    {
        Debug.Log("Gun changed for " + gun.name);
        this.gun = gun;
        Debug.Log("Gun is now " + this.gun.name);
    }

    /// <summary>
    /// Change le cooldown
    /// </summary>
    /// <param name="newCooldown">le nouveau cooldown à appliquer</param>
    void SetCooldown(float newCooldown)
    {
        Debug.Log("Cooldown changed for " + newCooldown);
        cooldown = newCooldown;
        Debug.Log("Cooldown is now " + cooldown);
    }
   
}
