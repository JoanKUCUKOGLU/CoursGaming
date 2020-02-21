using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    int damage = 1;
    void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SendMessage("Hitted",damage,SendMessageOptions.DontRequireReceiver);
        Destroy(this.gameObject);
    }

    void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}
