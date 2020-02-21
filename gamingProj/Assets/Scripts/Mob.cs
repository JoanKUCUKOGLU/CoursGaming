using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mob : MonoBehaviour
{
    [SerializeField]
    private float mobSpeed;
    [SerializeField]
    int maxLifePoint;

    int lifePoint;
    private Player player;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private NavMeshAgent agent;

    bool isTranslating = false;
    Quaternion startRot;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        startRot = transform.rotation;
        lifePoint = maxLifePoint;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = startRot;
        if(lifePoint <= 0)
        {
            Destroy(gameObject);
        }
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

    void Hitted(int damage)
    {
        lifePoint -= damage;
    }
}
