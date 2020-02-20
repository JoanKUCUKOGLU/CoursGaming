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

    bool isTranslating = false;

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
            agent.speed = !isTranslating ? 10 : 1;
            agent.SetDestination(player.transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Player") && !isTranslating)
        {
            if (player != null)
            {
                player.SendMessage("HealthDown");
                StartCoroutine(TranslationCoroutine());
            }
        }
    }


    IEnumerator TranslationCoroutine() 
    {
        float elapsedTime = 0;
        isTranslating = true;

        while (elapsedTime < 1)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isTranslating = false;
    }
}
