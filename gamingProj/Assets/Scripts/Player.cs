using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float translationSpeed;
    [SerializeField]
    public int healthPoint = 3;
    Rigidbody rigidbody;
    GameObject hand;
    //public GameObject resetButton;

    private float TimeSinceLastHit = 999;
    GameObject ui;

    [SerializeField]
    private float InvinsibleDuration;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        ui = GameObject.Find("Interface");
        hand = GameObject.Find("Hand");
    }

    // Update is called once per frame
    void Update()
    {
        TimeSinceLastHit += Time.deltaTime;
        if (Input.GetButton("ChangeWeapon1")) 
        {
            if(hand.transform.childCount >= 1)
            {
                ui.SendMessage("SetOverlayedWeapon",value:0) ;
                hand.SendMessage("ChangeWeapon", hand.transform.GetChild(0).name);
            }
        }

        if (Input.GetButton("ChangeWeapon2"))
        {
            if (hand.transform.childCount >= 2)
            {
                ui.SendMessage("SetOverlayedWeapon", value: 1);
                hand.SendMessage("ChangeWeapon", hand.transform.GetChild(1).name);
            }
        }

        if (Input.GetButton("ChangeWeapon3"))
        {
            if (hand.transform.childCount >= 3)
            {
                ui.SendMessage("SetOverlayedWeapon", 2);
                hand.SendMessage("ChangeWeapon", hand.transform.GetChild(2).name);
            }
        }

        if (Input.GetButton("ChangeWeapon4"))
        {
            if (hand.transform.childCount >= 4)
            {
                ui.SendMessage("SetOverlayedWeapon", 3);
                hand.SendMessage("ChangeWeapon", hand.transform.GetChild(3).name);
            }
        }
    }
    void FixedUpdate()
    {
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");
        Vector3 dir = Vector3.ClampMagnitude(new Vector3(hInput, 0, vInput), 1);

        if (healthPoint > 0)
        {
            rigidbody.MovePosition(transform.position + dir * translationSpeed * Time.fixedDeltaTime);
        }

    }

    void HealthDown()
    {
        healthPoint--;
        GameObject.Find("Interface").SendMessage("LooseHeart");
        if (healthPoint <= 0)
        {
            GameObject.Find("Interface").SendMessage("RestartButton");
        }
    }
}
