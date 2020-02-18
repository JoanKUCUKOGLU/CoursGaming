using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    float m_TranslationSpeed;

    Rigidbody m_RigidBody;

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
        
    }
    void FixedUpdate()
    {
        float vInput = Input.GetAxis("Vertical");
        float hInput = Input.GetAxis("Horizontal");
        Vector3 dir = Vector3.ClampMagnitude(new Vector3(hInput, 0, vInput),1);
        
        //Vector3 vectForward = transform.forward * m_TranslationSpeed * Time.fixedDeltaTime * vInput;
        //Vector3 vectSide = transform.right * m_TranslationSpeed * Time.fixedDeltaTime * hInput;
        m_RigidBody.MovePosition(transform.position + dir * m_TranslationSpeed * Time.fixedDeltaTime);
    }
}
