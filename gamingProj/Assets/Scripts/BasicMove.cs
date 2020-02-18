using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMove : MonoBehaviour
{
    [SerializeField]
    float _movSpeed;
    [SerializeField]
    float _rotSpeed;

    Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.MovePosition(transform.position + transform.forward * _movSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
        _rb.MoveRotation(transform.rotation * Quaternion.AngleAxis(Input.GetAxis("Horizontal") * Time.deltaTime * _rotSpeed, Vector3.up));
    }
}
