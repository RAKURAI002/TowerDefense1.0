using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameObject[] walkPath;

    public Transform SpawnPoint;

    public Enemy enemy;

    private float waveCooldown = 2f;
    private int waveAmount = 1;


    // Start is called before the first frame update
    void Awake()
    {
        walkPath = GameObject.FindGameObjectsWithTag("WalkPath");
        if(walkPath == null)
        {

            Debug.Log("Can't find Path . . ");
        }
    }

    private void Start()
    {
        //Instantiate(enemy, SpawnPoint.transform);
    }
    // Update is called once per frame
    void Update()
    {
        if(waveCooldown <= 0)
        {
            StartCoroutine("SpawnEnemy");
            waveCooldown = 5f;
        }
       // Debug.Log(waveCooldown);
        waveCooldown -= Time.deltaTime;
    }
    IEnumerator SpawnEnemy()
    {
        for(int i = 0; i < waveAmount; i++)
        {
            Instantiate(enemy, SpawnPoint.position, SpawnPoint.rotation);
            yield return new WaitForSeconds(1f);

        }
        


    }
}
