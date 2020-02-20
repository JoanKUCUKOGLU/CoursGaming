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
    public GameObject resetButton;

    private float TimeSinceLastHit = 999;

    [SerializeField]
    private float InvinsibleDuration;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeSinceLastHit += Time.deltaTime;
        if(healthPoint <= 0)
        {
            GameObject.Find("Interface").SendMessage("RestartButton");
        }
    }
    void FixedUpdate()
    {
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");
        Vector3 dir = Vector3.ClampMagnitude(new Vector3(hInput, 0, vInput), 1);

        //Vector3 vectForward = transform.forward * m_TranslationSpeed * Time.fixedDeltaTime * vInput;
        //Vector3 vectSide = transform.right * m_TranslationSpeed * Time.fixedDeltaTime * hInput;
        if (healthPoint > 0)
        {
            rigidbody.MovePosition(transform.position + dir * translationSpeed * Time.fixedDeltaTime);
            //rigidBody.velocity = Vector3.zero;
        }

    }

    void HealthDown()
    {
        healthPoint--;
        Debug.Log(healthPoint);
    }
}
