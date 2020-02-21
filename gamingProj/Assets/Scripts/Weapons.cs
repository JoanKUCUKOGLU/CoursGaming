using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

abstract public class Weapons : MonoBehaviour
{
    
    [SerializeField]
    protected float cooldown;
    [SerializeField]
    protected int maxRound;

    protected int round;
    [SerializeField]
    protected SpriteRenderer weaponVisual;
    protected Collider collider;
    protected GameObject hand;
    protected bool isLooted = false;
    private GameObject iu;

    void Awake()
    {
        //Récupération des éléments visuels et physique du gun pour désactiver au loot
        weaponVisual = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider>();
    }

    private void Start() 
    {
        iu = GameObject.Find("Interface");
    }
    // Appel quand entrée en collision
    void OnTriggerEnter(Collider collision)
    {

        Debug.Log(collision.gameObject.name);
        //Met l'arme en arme active et la fait disparaitre du sol
        if (collision.transform.name == "Player")
        {
            round = maxRound;
            CalculateRounds();
            hand = GameObject.Find("Hand");
            isLooted = true;
            transform.parent = hand.transform;
            transform.rotation = hand.transform.rotation;
            weaponVisual.enabled = false;
            collider.enabled = false;
            hand.SendMessage("SetGun", this.gameObject); //this optionnel, mis pour la clareté de code
            hand.SendMessage("SetCooldown", cooldown);
            iu.SendMessage("GetWeapons",hand.transform.childCount - 1);
            
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
            //Destroy(newBulletGO, bulletLifeTime);
        }
    }

    abstract protected void Shoot(Vector3 shootPos);

    virtual protected void LooseMunitions()
    {
        round--;
        CalculateRounds();
        if(round <= 0)
        {
            Destroy(this.gameObject);
            hand.SendMessage("ChangeWeapon","Knife");
            iu.SendMessage("GetWeapons");
        }
    }
    protected void CalculateRounds()
    {
        Rounds rounds = new Rounds()
        {
            ActualRound = round,
            MaxRound = maxRound
        };
        iu.SendMessage("UpdadateRounds",rounds);
    }
}

public class Rounds
{
    public int ActualRound { get; set; }
    public int MaxRound { get; set; }
}