using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    [SerializeField]
    private int lifeEspacement = 0;
    [SerializeField]
    private int espacementWparent = 0;

    private Player player;
    public GameObject ParentLives;
    //public GameObject ParentWeapons;
    public GameObject roundText;

    private Font Arialfont;
    // Start is called before the first frame update
    void Start()
    {
        ParentLives = GameObject.Find("LivesText");
        //ParentWeapons = GameObject.Find("WeaponsText");
        roundText = GameObject.Find("WeaponMunitionsText");
        player = GameObject.FindObjectOfType<Player>();
        Arialfont = Resources.GetBuiltinResource<Font>("Arial.ttf");
        PrintHearts();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Transform GetLastLive()
    {
        return ParentLives.transform.GetChild(ParentLives.transform.childCount - 1);
    }

    //private Transform[] getWeaponListChild()
    //{
    //    List<Transform> childs = new List<Transform>();
    //    foreach (Transform child in ParentWeapons.transform)
    //    {
    //        childs.Add(child);
    //        //Debug.Log(child.transform.name);
    //    }
    //    return childs.ToArray();
    //}
    //private Transform[] getEnergyListChild()
    //{
    //    List<Transform> childs = new List<Transform>();
    //    foreach (Transform child in ParentEnergy.transform)
    //    {
    //        childs.Add(child);
    //        //Debug.Log(child.transform.name);
    //    }
    //    return childs.ToArray();
    //}


    private void PrintHearts()
    {
        for (var i = 1; i <= player.HealthPoint; i++)
        {
            CreateHeart();
        }
    }
    private void CreateHeart()
    {
        GameObject NewObj = new GameObject();
        Image NewImage = NewObj.AddComponent<Image>();
        NewImage.color = Color.red;
        NewObj.GetComponent<RectTransform>().localScale = new Vector3(1,1,0);
        if (ParentLives.transform.childCount > 0)
        {
            Transform lastChild = GetLastLive();
            NewObj.transform.position = new Vector3(lastChild.position.x + lifeEspacement, ParentLives.transform.position.y, ParentLives.transform.position.z);
        }
        else
        {
            NewObj.transform.position = new Vector3(ParentLives.transform.position.x + espacementWparent, ParentLives.transform.position.y, ParentLives.transform.position.z);
        }
        NewObj.GetComponent<RectTransform>().SetParent(ParentLives.transform);
    }
    void LooseHeart()
    {
        Transform lastChild = GetLastLive();
        Destroy(lastChild.gameObject);
    }
    void GetLife()
    {
        CreateHeart();
    }

    void UpdadateRounds(Rounds rounds)
    {
        roundText.GetComponent<Text>().text = rounds.ActualRound + " / " + rounds.MaxRound;
    }

//    void getWeapon(string weapon)
//    {
//        GameObject NewObj = new GameObject();
//        Image NewImage = NewObj.AddComponent<Image>();

//        Color color = Color.white;
//        switch (weapon)
//        {
//            case "Gun":
//                color = Color.blue;
//                break;
//            case "Shotgun":
//                color = Color.black;
//                break;
//        }
//        NewImage.color = color;
//        NewImage.name = weapon + getWeaponListChild().Length + 1;
//        if (getWeaponListChild().Length > 0)
//        {
//            Transform lastChild = getWeaponListChild()[getWeaponListChild().Length - 1];
//            NewObj.transform.position = new Vector3(lastChild.position.x + 150, ParentWeapons.transform.position.y, ParentWeapons.transform.position.z);
//        }
//        else
//        {
//            NewObj.transform.position = new Vector3(ParentWeapons.transform.position.x + 200, ParentWeapons.transform.position.y, ParentWeapons.transform.position.z);
//        }
//        NewObj.GetComponent<RectTransform>().SetParent(ParentWeapons.transform);
//        getWeaponEnergy(weapon);
//    }

//    void WeaponLess()
//    {
//        Transform lastChild = getWeaponListChild()[getWeaponListChild().Length - 1];
//        Destroy(GameObject.Find(lastChild.name));
//        //Transform lastEChild = getEnergyListChild()[getEnergyListChild().Length - 1];
//        //Destroy(GameObject.Find(lastEChild.name));
//    }
//    void getWeaponEnergy(string weapon)
//    {
//        var cibledWeapon = GameObject.FindObjectOfType<Gun>();
//        switch (weapon)
//        {
//            case "Gun":
//                cibledWeapon = GameObject.FindObjectOfType<Gun>();
//                break;
//            case "Shogun":
//                cibledWeapon = GameObject.FindObjectOfType<Gun>();
//                break;
//        }
//        Debug.Log(cibledWeapon);
//        GameObject newGO = new GameObject();
//        Text myText = newGO.AddComponent<Text>();
//        myText.name = "WeaponMunitions";
//        myText.text = cibledWeapon.energy + "/" + cibledWeapon.Maxenergy;
//        myText.font = Arialfont;
//        myText.color = Color.green;
//        myText.fontSize = ParentEnergy.GetComponent<Text>().fontSize + 10;
//        myText.fontStyle = FontStyle.Bold;
//        myText.transform.position = new Vector3(ParentEnergy.transform.position.x + 150, ParentEnergy.transform.position.y - 25, ParentEnergy.transform.position.z);
//        newGO.GetComponent<RectTransform>().SetParent(ParentEnergy.transform);
//    }

//    void LoseEnergy(string weapon)
//    {
//        Transform lastChild = getEnergyListChild()[getEnergyListChild().Length - 1];
//        Destroy(GameObject.Find(lastChild.name));
//        getWeaponEnergy(weapon);
//    }
}
