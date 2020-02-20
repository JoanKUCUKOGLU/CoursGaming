using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    private Player player;
    public GameObject ParentLives;
    public GameObject ParentWeapons;
    public GameObject ParentEnergy;

    private Font Arialfont;
    // Start is called before the first frame update
    void Start()
    {
        ParentLives = GameObject.Find("LivesText");
        ParentWeapons = GameObject.Find("WeaponsText");
        ParentEnergy = GameObject.Find("WeaponMunitionsText");
        player = GameObject.FindObjectOfType<Player>();
        Arialfont = Resources.GetBuiltinResource<Font>("Arial.ttf");
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
    private Transform[] getEnergyListChild()
    {
        List<Transform> childs = new List<Transform>();
        foreach (Transform child in ParentEnergy.transform)
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
    void getLife()
    {
        GameObject NewObj = new GameObject();
        Image NewImage = NewObj.AddComponent<Image>();
        NewImage.color = Color.red;
        NewImage.name = "Heart" + getLivesListChild().Length + 1;
        if (getLivesListChild().Length > 0)
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
        NewImage.name = weapon + getWeaponListChild().Length + 1;
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
        getWeaponEnergy(weapon);
    }

    void WeaponLess()
    {
        Transform lastChild = getWeaponListChild()[getWeaponListChild().Length - 1];
        Destroy(GameObject.Find(lastChild.name));
        //Transform lastEChild = getEnergyListChild()[getEnergyListChild().Length - 1];
        //Destroy(GameObject.Find(lastEChild.name));
    }
    void getWeaponEnergy(string weapon)
    {
        var cibledWeapon = GameObject.FindObjectOfType<Gun>();
        switch (weapon)
        {
            case "Gun":
                cibledWeapon = GameObject.FindObjectOfType<Gun>();
                break;
            case "Shogun":
                cibledWeapon = GameObject.FindObjectOfType<Gun>();
                break;
        }

        GameObject newGO = new GameObject();
        Text myText = newGO.AddComponent<Text>();
        myText.name = "WeaponMunitions";
        myText.text = cibledWeapon.energy + "/" + cibledWeapon.Maxenergy;
        myText.font = Arialfont;
        myText.color = Color.green;
        myText.fontSize = ParentEnergy.GetComponent<Text>().fontSize + 10;
        myText.fontStyle = FontStyle.Bold;
        myText.transform.position = new Vector3(ParentEnergy.transform.position.x + 150, ParentEnergy.transform.position.y - 25, ParentEnergy.transform.position.z);
        newGO.GetComponent<RectTransform>().SetParent(ParentEnergy.transform);
    }

    void LoseEnergy(string weapon)
    {
        Transform lastChild = getEnergyListChild()[getEnergyListChild().Length - 1];
        Destroy(GameObject.Find(lastChild.name));
        getWeaponEnergy(weapon);
    }
}
