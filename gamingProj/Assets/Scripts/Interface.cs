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

    public GameObject weaponEmplacement1;
    public GameObject weaponEmplacement2;
    public GameObject weaponEmplacement3;
    public GameObject weaponEmplacement4;

    private GameObject[] weaponEnplacements;
    private Font Arialfont;
    // Start is called before the first frame update
    void Start()
    {
        ParentLives = GameObject.Find("LivesText");
        //ParentWeapons = GameObject.Find("WeaponsText");
        roundText = GameObject.Find("WeaponMunitionsText");
        player = GameObject.FindObjectOfType<Player>();
        Arialfont = Resources.GetBuiltinResource<Font>("Arial.ttf");
        weaponEmplacement1 = GameObject.Find("WeaponEmplacement1");
        weaponEmplacement2 = GameObject.Find("WeaponEmplacement2");
        weaponEmplacement3 = GameObject.Find("WeaponEmplacement3");
        weaponEmplacement4 = GameObject.Find("WeaponEmplacement4");

        weaponEnplacements = new GameObject[4]{ weaponEmplacement1, weaponEmplacement2, weaponEmplacement3, weaponEmplacement4};
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

    void GetWeapons()
    {
        List<string> childs = new List<string>();
        for(int i = 0; i < GameObject.Find("Hand").transform.childCount;i++)
        {
            childs.Add(GameObject.Find("Hand").transform.GetChild(i).name);
        }
        PrintWeapons(childs.ToArray());
    }

    void PrintWeapons(string[] weapons)
    {
        for(int i = 0; i< weapons.Length;i++)
        {
            Sprite sprite = GameObject.Find(weapons[i]).GetComponent<SpriteRenderer>().sprite;
            GameObject weaponArt = Instantiate(new GameObject(),weaponEnplacements[i].transform);
            Image waponImg = weaponArt.AddComponent<Image>();
            waponImg.sprite = sprite;
        }
    }


}
