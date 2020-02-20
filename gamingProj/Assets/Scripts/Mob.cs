using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mob : MonoBehaviour
{
    [SerializeField]
    private float mobSpeed;

    private Player player;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.frameCount % 20 == 0)
        {
            agent.speed = !isTranslating ? 5F : 0.5F;
            agent.SetDestination(player.transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Player") && !isTranslating)
        {
            player.SendMessage("HealthDown");
            StartCoroutine(TranslationCoroutine());
        }
    }

    bool isTranslating = false;

    IEnumerator TranslationCoroutine() 
    {
        float elapsedTime = 0;
        //isTranslating = true;

        //Vector3 direction = (transform.position - player.transform.position).normalized;

        //GetComponent<Rigidbody>().velocity = direction * 100;

        while (elapsedTime < 0.5)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isTranslating = false;
    }
}
