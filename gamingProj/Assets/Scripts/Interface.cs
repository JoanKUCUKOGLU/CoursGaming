using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    private Player player;
    public GameObject Parent;
    // Start is called before the first frame update
    void Start()
    {
        Parent = GameObject.Find("LivesText");
        player = GameObject.FindObjectOfType<Player>();
        getHearts();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Transform[] getListChild()
    {
        List<Transform> childs = new List<Transform>();
        foreach (Transform child in Parent.transform)
        {
            childs.Add(child);
            //Debug.Log(child.transform.name);
        }
        return childs.ToArray();
    }

    private void getHearts()
    {
        Debug.Log("Healt : "+player.HealthPoint);
        for (var i = 1; i <= player.HealthPoint; i++)
        {
            Debug.Log("Healt : " + player.HealthPoint+"i : "+i);
            GameObject NewObj = new GameObject();
            Image NewImage = NewObj.AddComponent<Image>();
            NewImage.color = Color.red;
            NewImage.name = "Heart" + (i);
            if (i > 1)
            {
                Transform lastChild = getListChild()[getListChild().Length - 1];
                NewObj.transform.position = new Vector3(lastChild.position.x + 150, Parent.transform.position.y, Parent.transform.position.z);
            }
            else
            {
                NewObj.transform.position = new Vector3(Parent.transform.position.x + 150, Parent.transform.position.y, Parent.transform.position.z);
            }
            NewObj.GetComponent<RectTransform>().SetParent(Parent.transform);
        }
    }
}
