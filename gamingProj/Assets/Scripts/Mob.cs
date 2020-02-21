using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mob : MonoBehaviour
{
    [SerializeField]
    int maxLifePoint;

    int lifePoint;
    private Player player;

    [SerializeField]
    private NavMeshAgent agent;

    bool isTranslating = false;
    Quaternion startRot;

    int randomLooted = 0;
    int randomLoot = 0;
    [SerializeField]
    GameObject[] possibleLoots;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        startRot = transform.rotation;
        lifePoint = maxLifePoint;
        
        //loots = new Transform[3]{ GameObject.<Life>().transform, GameObject.FindObjectOfType<Gun>().transform, GameObject.FindObjectOfType<Shotgun>().transform };

    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = startRot;
        transform.position = new Vector3(transform.position.x,0,transform.position.z);
        if(lifePoint <= 0)
        {
            System.Random rnd = new System.Random();
            randomLooted = rnd.Next(1, 3);
            //Debug.Log(randomLooted);
            if(randomLooted == 2)
            {
                randomLoot = rnd.Next(0, 2);
                GameObject lootGO = GameObject.Instantiate(possibleLoots[randomLoot]);
                lootGO.transform.position = transform.position;
            }
            Destroy(gameObject);
        }
        if(Time.frameCount % 1 == 0)
        {
            agent.speed = !isTranslating ? 5 : 1;
            agent.SetDestination(player.transform.position);
        }
    }

    private void OnCollisionStay(Collision collision)
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
