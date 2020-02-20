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
    [SerializeField]
    private GameObject restartButtonGO;
    private Player player;
    public GameObject ParentLives;
    //public GameObject ParentWeapons;
    public GameObject roundText;

    public GameObject weaponEmplacement1;
    public GameObject weaponEmplacement2;
    public GameObject weaponEmplacement3;
    public GameObject weaponEmplacement4;

    private GameObject[] weaponEmplacements;
    int overlayedWeaponIndex = 0;
    private Font Arialfont;
    // Start is called before the first frame update
    void Start()
    {
        ParentLives = GameObject.Find("LivesText");
        roundText = GameObject.Find("WeaponMunitionsText");
        player = GameObject.FindObjectOfType<Player>();
        Arialfont = Resources.GetBuiltinResource<Font>("Arial.ttf");
        weaponEmplacement1 = GameObject.Find("WeaponEmplacement1");
        weaponEmplacement2 = GameObject.Find("WeaponEmplacement2");
        weaponEmplacement3 = GameObject.Find("WeaponEmplacement3");
        weaponEmplacement4 = GameObject.Find("WeaponEmplacement4");

        weaponEmplacements = new GameObject[4]{ weaponEmplacement1, weaponEmplacement2, weaponEmplacement3, weaponEmplacement4};
        PrintHearts();
        GetWeapons(0);
    }
    
    private Transform GetLastLive()
    {
        return ParentLives.transform.GetChild(ParentLives.transform.childCount - 1);
    }

    private void PrintHearts()
    {
        for (var i = 1; i <= player.healthPoint; i++)
        {
            CreateHeart();
        }
    }
    private void CreateHeart()
    {
        GameObject NewObj = Instantiate(new GameObject());
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
    void AddLife(int lifeToUp)
    {
        for (int i = 0; i < lifeToUp; i++)
        { 
            CreateHeart();
        }
    }

    void UpdadateRounds(Rounds rounds)
    {
        roundText.GetComponent<Text>().text = rounds.ActualRound + " / " + rounds.MaxRound;
    }

    void GetWeapons(int overlayedIndex)
    {
        overlayedWeaponIndex = overlayedIndex;
        List<string> childs = new List<string>();
        for(int i = 0; i < GameObject.Find("Hand").transform.childCount;i++)
        {
            childs.Add(GameObject.Find("Hand").transform.GetChild(i).name);
        }
        PrintWeapons(childs.ToArray());
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        foreach (GameObject weaponEmplacement in weaponEmplacements)
        {
            for (int i = 0; i < weaponEmplacement.transform.childCount; i++)
            {
                Transform img = weaponEmplacement.transform.GetChild(i);
                Destroy(img.gameObject);
                Destroy(weaponEmplacement.transform.GetChild(i));
            }
        }
    }
    private void LateUpdate() {
        GetWeapons(overlayedWeaponIndex);
    }
    void PrintWeapons(string[] weapons)
    {
        
        //Debug.Break();
        for(int i = 0; i< weapons.Length;i++)
        {
            Sprite sprite = GameObject.Find(weapons[i]).GetComponent<SpriteRenderer>().sprite;
            GameObject weaponArt = Instantiate(new GameObject(),weaponEmplacements[i].transform);
            Image weaponImg = weaponArt.AddComponent<Image>();
            weaponImg.sprite = sprite;
            if(i == overlayedWeaponIndex)
            {
                weaponImg.color = Color.red;
            }
        }
    }

    void RestartButton()
    {
        Instantiate(restartButtonGO);
    }
}
