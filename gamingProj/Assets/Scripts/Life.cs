using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.transform.name == "Player")
        {
            player.SendMessage("HealthUp");
            Destroy(gameObject);
        }
    }
}
