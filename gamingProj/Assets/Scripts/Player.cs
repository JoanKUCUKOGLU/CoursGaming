    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float m_TranslationSpeed;

    private Rigidbody m_RigidBody;

    private int HealthPoint = 3;

    private float TimeSinceLastHit = 999;

    [SerializeField]
    private float InvinsibleDuration;

    void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeSinceLastHit += Time.deltaTime;
    }
    void FixedUpdate()
    {
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");
        Vector3 dir = Vector3.ClampMagnitude(new Vector3(hInput, 0, vInput),1);
        m_RigidBody.MovePosition(transform.position + dir * m_TranslationSpeed * Time.fixedDeltaTime);
        m_RigidBody.velocity = Vector3.zero;
    }

    void HealthDown()
    {
        if(TimeSinceLastHit >= InvinsibleDuration)
        {
            HealthPoint -= HealthPoint > 0 ? 1 : 0;
            TimeSinceLastHit = 0;
            Debug.Log(HealthPoint);
        } else
        {
            Debug.Log("Lol no");
        }
        TimeSinceLastHit += Time.deltaTime;
        Debug.Log(TimeSinceLastHit);

    }
}
