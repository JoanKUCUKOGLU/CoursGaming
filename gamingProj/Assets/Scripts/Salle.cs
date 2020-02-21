using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salle : MonoBehaviour
{
    bool IsRunning = false;
    bool HasBeenCompleted = false;

    List<Collider> DoorList = new List<Collider>();
    List<GameObject> SpawnerList = new List<GameObject>();

    [SerializeField]
    GameObject MobPrefab;

    int remainingMobs;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Transform tChild = transform.GetChild(i);

            if (tChild.gameObject.tag.Equals("DoorBlocker"))
            {
                Collider door = tChild.gameObject.GetComponent<Collider>();
                door.enabled = false;
                DoorList.Add(door);
            }
            else if (tChild.gameObject.tag.Equals("Spawner"))
            {
                GameObject spawner = tChild.gameObject;
                SpawnerList.Add(spawner);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsRunning && Time.frameCount % 40 == 0)
        {
            remainingMobs = 0;

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform tChild = transform.GetChild(i);

                if (tChild.gameObject.tag.Equals("Mob"))
                {
                    remainingMobs += 1;
                }
            }

            Debug.Log(remainingMobs);

            if(remainingMobs == 0)
            {
                StopRoom();
            }
        }
    }

    void StartRoom()
    {
        Debug.Log("start");
        if(!HasBeenCompleted && !IsRunning)
        {
            foreach(Collider door in DoorList)
            {
                door.enabled = true;
            }

            foreach(GameObject spawner in SpawnerList)
            {
                GameObject mob = Instantiate(MobPrefab, spawner.transform.position, spawner.transform.rotation);
                mob.transform.parent = transform;
            }
            remainingMobs = SpawnerList.Count;
            IsRunning = true;
        }
    }

    void StopRoom()
    {
        Debug.Log("Stop");
        IsRunning = false;
        HasBeenCompleted = true;
        foreach (Collider door in DoorList)
        {
            door.enabled = false;
        }
    }
}
