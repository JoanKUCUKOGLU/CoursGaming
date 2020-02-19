using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField]
    float cooldown;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    float bulletSpeed;

    GameObject firedBullet;
    MeshRenderer meshRenderer;
    BoxCollider boxCollider;
    GameObject hand;
    bool isLooted = false;

    private Interface iu;
    void Awake()
    {
        //Récupération des éléments visuels et physique du gun pour désactiver au loot
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        iu = GameObject.FindObjectOfType<Interface>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Appel quand entrée en collision
    void OnCollisionEnter(Collision collider)
    {
        Debug.Log(collider.gameObject.name);
        //Met l'arme en arme active et la fait disparaitre du sol
        if (collider.transform.name == "Player")
        {
            iu.SendMessage("getWeapon", "Shotgun");
            hand = GameObject.Find("Hand");
            isLooted = true;
            transform.parent = hand.transform;
            meshRenderer.enabled = false;
            boxCollider.enabled = false;
            hand.SendMessage("SetGun", this.gameObject); //this optionnel, mis pour la clareté de code
            hand.SendMessage("SetCooldown", cooldown);
        }
    }

    void Shoot()
    {
        if (isLooted)
        {
            for(int i = 0; i < 8; i++)
            {
                firedBullet = Instantiate(bullet, hand.transform.position, hand.transform.rotation);
                firedBullet.GetComponent<Rigidbody>().AddForce(firedBullet.transform.forward * bulletSpeed);
                iu.SendMessage("WeaponLess");
                Destroy(firedBullet, 2); //Temporaire pour tests
            }
        }
    }
}
