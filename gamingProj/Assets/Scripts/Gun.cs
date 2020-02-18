using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    float cooldown = 1;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    float bulletSpeed;

    MeshRenderer meshRenderer;
    BoxCollider boxCollider;
    bool isLooted = false;
    float internalClock = 0;
    GameObject firedBullet;
    GameObject hand;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void OnEnable()
    {
        internalClock = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isLooted && Input.GetButton("Fire1") && internalClock >= cooldown)
        {
            firedBullet = Instantiate(bullet,hand.transform);
            firedBullet.transform.position = firedBullet.transform.parent.position;
            firedBullet.GetComponent<Rigidbody>().AddForce(firedBullet.transform.forward * bulletSpeed);
            
            Destroy(firedBullet, 2); //Temporaire pour tests
            internalClock = 0;
        }
        internalClock += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collider)
    {
        Debug.Log(collider.gameObject.name);
        if(collider.transform.name == "Player")
        {
            hand = GameObject.Find("Hand");
            isLooted = true;
            transform.parent = hand.transform;
            meshRenderer.enabled = false;
            boxCollider.enabled = false;
        }
    }
}
