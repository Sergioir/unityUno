using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject playerGoal;

    // Start is called before the first frame update
    void Start()
    {
        speed = GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManagerX>().speedEn;
        enemyRb = GetComponent<Rigidbody>();
        playerGoal=GameObject.FindWithTag("PlayerGoal");
        
    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
             GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
                       for (int i = 0; i < enemigos.Length; i++)
                       {
                           Destroy(enemigos[i]);
                       }
                        GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManagerX>().SpawnEnemyWave(GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManagerX>().waveCount);
//Destroy(gameObject);
        } 
        else if (other.gameObject.name == "Player Goal")
        {
           // Destroy(gameObject);
           GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
           for (int i = 0; i < enemigos.Length; i++)
           {
               Destroy(enemigos[i]);
           }
            GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManagerX>().SpawnEnemyWave(GameObject.FindWithTag("SpawnManager").GetComponent<SpawnManagerX>().waveCount);

        }

    }

}
