using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSine : MonoBehaviour
{
    [SerializeField] Vector2 m_ViewportStartPos;
    [SerializeField] Vector2 m_ViewportEndPos;
    [SerializeField] float m_TranslationSpeed;
    [SerializeField] float m_SineAmplitude;
    [SerializeField] float m_SinePulsation;

    [SerializeField] bool m_OrientateTowardsEnd;

    Vector3 m_SineDir;
    Vector3 m_StartPos;
    Vector3 m_EndPos;
    Vector3 m_LinearPos;

    Vector3 WorldPos(Vector2 viewportPos)
    {
        return Camera.main.ViewportToWorldPoint(new Vector3(viewportPos.x, viewportPos.y, -Camera.main.transform.position.z));
    }
    // Start is called before the first frame update
    void Start()
    {
        m_StartPos = WorldPos(m_ViewportStartPos);
        m_EndPos = WorldPos(m_ViewportEndPos);
        m_SineDir = Vector3.Cross((m_EndPos - m_StartPos).normalized,Vector3.forward);
        m_LinearPos = m_StartPos;
        transform.position = m_LinearPos;
        if (m_OrientateTowardsEnd)
            transform.LookAt(m_EndPos);
    }

    // Update is called once per frame
    void Update()
    {
        m_LinearPos = Vector3.MoveTowards(m_LinearPos, m_EndPos, m_TranslationSpeed * Time.deltaTime);
        transform.position = m_LinearPos+ m_SineDir * Mathf.Sin(m_SinePulsation * Time.time) * m_SineAmplitude;
        if (Mathf.Approximately(Vector3.Distance(m_LinearPos, m_EndPos), 0))
            m_LinearPos = m_StartPos;
    }
}
