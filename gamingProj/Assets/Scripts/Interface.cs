using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    private Player player;
    public GameObject ParentLives;
    public GameObject ParentWeapons;
    // Start is called before the first frame update
    void Start()
    {
        ParentLives = GameObject.Find("LivesText");
        ParentWeapons = GameObject.Find("WeaponsText");
        player = GameObject.FindObjectOfType<Player>();
        getHearts();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Transform[] getLivesListChild()
    {
        List<Transform> childs = new List<Transform>();
        foreach (Transform child in ParentLives.transform)
        {
            childs.Add(child);
            //Debug.Log(child.transform.name);
        }
        return childs.ToArray();
    }
    private Transform[] getWeaponListChild()
        {
            List<Transform> childs = new List<Transform>();
            foreach (Transform child in ParentWeapons.transform)
            {
                childs.Add(child);
                //Debug.Log(child.transform.name);
            }
            return childs.ToArray();
        }
    private void getHearts()
    {
        for (var i = 1; i <= player.HealthPoint; i++)
        {
            GameObject NewObj = new GameObject();
            Image NewImage = NewObj.AddComponent<Image>();
            NewImage.color = Color.red;
            NewImage.name = "Heart" + (i);
            if (i > 1)
            {
                Transform lastChild = getLivesListChild()[getLivesListChild().Length - 1];
                NewObj.transform.position = new Vector3(lastChild.position.x + 150, ParentLives.transform.position.y, ParentLives.transform.position.z);
            }
            else
            {
                NewObj.transform.position = new Vector3(ParentLives.transform.position.x + 150, ParentLives.transform.position.y, ParentLives.transform.position.z);
            }
            NewObj.GetComponent<RectTransform>().SetParent(ParentLives.transform);
        }
    }
    
    void HeartLess()
    {
        Transform lastChild = getLivesListChild()[getLivesListChild().Length - 1];
        Destroy(GameObject.Find(lastChild.name));
    }

    void getWeapon(string weapon)
    {
        GameObject NewObj = new GameObject();
        Image NewImage = NewObj.AddComponent<Image>();
       
        Color color = Color.white;
        switch (weapon)
        {
            case "Gun":
                color = Color.blue;
                break;
            case "Shotgun":
                color = Color.black;
                break;
        }
        NewImage.color = color;
        NewImage.name = weapon+ getWeaponListChild().Length+1;
        if (getWeaponListChild().Length > 0)
        {
            Transform lastChild = getWeaponListChild()[getWeaponListChild().Length - 1];
            NewObj.transform.position = new Vector3(lastChild.position.x + 150, ParentWeapons.transform.position.y, ParentWeapons.transform.position.z);
        }
        else
        {
            NewObj.transform.position = new Vector3(ParentWeapons.transform.position.x + 200, ParentWeapons.transform.position.y, ParentWeapons.transform.position.z);
        }
        NewObj.GetComponent<RectTransform>().SetParent(ParentWeapons.transform);
    }

    void WeaponLess()
    {
        Transform lastChild = getWeaponListChild()[getWeaponListChild().Length - 1];
        Destroy(GameObject.Find(lastChild.name));
    }

}
