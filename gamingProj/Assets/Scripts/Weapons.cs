using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

abstract public class Weapons : MonoBehaviour
{
    
    [SerializeField]
    protected float cooldown;

    protected MeshRenderer meshRenderer;
    protected Collider collider;
    protected GameObject hand;
    protected bool isLooted = false;

    void Awake()
    {
        //Récupération des éléments visuels et physique du gun pour désactiver au loot
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    // Appel quand entrée en collision
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collider.gameObject.name);
        //Met l'arme en arme active et la fait disparaitre du sol
        if (collision.transform.name == "Player")
        {
            hand = GameObject.Find("Hand");
            isLooted = true;
            transform.parent = hand.transform;
            transform.rotation = hand.transform.rotation;
            meshRenderer.enabled = false;
            collider.enabled = false;
            hand.SendMessage("SetGun", this.gameObject); //this optionnel, mis pour la clareté de code
            hand.SendMessage("SetCooldown", cooldown);
        }
    }

    static public void SprayShoot(GameObject bulletPrefab, int nbBullets, float bulletSpeed, float bulletLifeTime, Vector3 sprayCenter, Vector3 sprayDir, Vector3 sprayNormal, float sprayHalfAngleDeg, float sprayRadius)
    {
        for (int i = 0; i < nbBullets; i++)
        {
            GameObject newBulletGO = Instantiate(bulletPrefab);
            Vector3 dir = Quaternion.AngleAxis(Mathf.Lerp(-sprayHalfAngleDeg, sprayHalfAngleDeg, (float)i / (nbBullets - 1)), sprayNormal) * sprayDir;
            dir.Normalize();
            newBulletGO.transform.position = sprayCenter + dir * sprayRadius;
            newBulletGO.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;
            Destroy(newBulletGO, bulletLifeTime);
        }
    }

    abstract protected void Shoot(Vector3 shootPos);
}
