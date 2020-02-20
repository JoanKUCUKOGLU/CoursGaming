using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapons
{
    [SerializeField]
    float distance = 1;
    [SerializeField]
    float speed = 1;

    bool isFiring = false;
    bool isRetiring = false;
    int i = 0;

    protected override void Shoot(Vector3 shootPos)
    {
        if (!isFiring && !isRetiring)
        {
            isFiring = true;
        }
    }

    private void Update()
    {
        float travelDist = i * speed * Time.deltaTime;
        if (isFiring)
        {
            transform.parent.SendMessage("SetIsFixed", true);
            collider.enabled = true;
            weaponVisual.enabled = true;
            transform.position += transform.parent.forward * Mathf.Lerp(0, distance, travelDist);
            if (travelDist >= 0.1)
            {
                isFiring = false;
                isRetiring = true;
            }
            i++;
        }
        else if (isRetiring)
        {
            i--;
            transform.position -= transform.parent.forward * Mathf.Lerp(0, distance, travelDist);
            if (travelDist <= 0)
            {
                isFiring = false;
                isRetiring = false;
                transform.parent.SendMessage("SetIsFixed", false);
                collider.enabled = false;
                weaponVisual.enabled = false;
                transform.position = transform.parent.position;
            }
        }
    }

    protected override void LooseMunitions()
    {
        
    }
}
