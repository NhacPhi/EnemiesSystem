using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    public float perceptionRadius;
    
    private List<GameObject> enemies;

    void Start()
    {
        enemies = new List<GameObject>();
        SpawnEnemies();
    }

    void Update()
    {
        UpdatePositionEnemy();
    }

    void SpawnEnemies()
    {
        for(int i = 0; i < 35; i++)
        {
            float x = Random.Range(-30, 30);
            float y = Random.Range(-30, 30);

            Vector3 pos = new Vector3(x, y, 1);

            GameObject obj = ObjectPooler.Instance.GetObjectFromPool("Green_Monster_Lv0");
            obj.transform.position = pos;

            enemies.Add(obj);
        }
    }

    private void UpdatePositionEnemy()
    {
        foreach (GameObject obj in enemies) 
        {
            Vector3 direcition = player.transform.position - obj.transform.position;
            obj.GetComponent<Enemy>().SetVelocityOfEnemy(direcition.normalized, player.transform.position);
            //obj.GetComponent<Enemy>().Alignment(enemies);
            obj.GetComponent<Enemy>().Separation(enemies);
        }
    }

    private Vector3 Separation(Vector3 direcition, GameObject obj)
    {
        Vector3 steering = new Vector3(0, 0, 0);

        int total = 0;

        foreach (GameObject other in enemies)
        {
            Vector3 d = obj.transform.position - other.transform.position;

            float distance = d.magnitude;

            if (other != obj && distance < perceptionRadius)
            {
                Vector3 diff = gameObject.transform.position - other.transform.position;
                diff /= distance;
                steering += diff;
                total++;
            }
        }

        if (total > 0)
        {
            steering /= total;
            steering -= direcition;
            return steering;
        }
        return direcition;
    }

    private Vector3 Alignment(Vector3 direcition, GameObject obj)
    {
        Vector3 steering = new Vector3(0, 0, 0);

        int total = 0;

        foreach (GameObject other in enemies)
        {
            Vector3 d = obj.transform.position - other.transform.position;

            float distance = d.magnitude;

            if (other != obj && distance < perceptionRadius)
            {
                Vector3 diff = gameObject.transform.position - other.transform.position;
                steering += diff;
                total++;
            }
        }

        if (total > 0)
        {
            steering /= total;
            steering -= direcition;
            return steering;
        }
        else
        {
            return direcition;
        }
    }
}
