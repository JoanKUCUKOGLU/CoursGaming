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

    void Awake()
    {
        //Récupération des éléments visuels et physique du gun pour désactiver au loot
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
            for(int i = 0; i < 4; i++)
            {
                firedBullet = Instantiate(bullet, hand.transform);
                firedBullet.transform.position = firedBullet.transform.parent.position;
                firedBullet.GetComponent<Rigidbody>().AddForce(firedBullet.transform.forward * bulletSpeed);

                Destroy(firedBullet, 2); //Temporaire pour tests
            }
        }
    }
}
