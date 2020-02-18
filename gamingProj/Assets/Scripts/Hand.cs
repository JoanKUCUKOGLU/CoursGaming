using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    float cooldown = 1;    
    float internalClock = 0;
    float shootingAngle;
    GameObject gun;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        shootingAngle = Mathf.Atan2(Input.GetAxis("R3X"), Input.GetAxis("R3Y")) * Mathf.Rad2Deg;
        transform.position = new Vector3(Input.GetAxis("R3X") + transform.parent.position.x, transform.position.y, Input.GetAxis("R3Y") + transform.parent.position.z);
        transform.rotation = Quaternion.Euler(0, shootingAngle, 0);

        if ((Input.GetAxis("R3X") != 0 || Input.GetAxis("R3Y") != 0) && internalClock >= cooldown)
        {
            gun.SendMessage("Shoot");   //Appelle la fonction "Shoot" de tout les composants du gun actuel
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
