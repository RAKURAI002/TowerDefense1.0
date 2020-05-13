using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
   

    private float speed = 2f;
    private Transform walkTarget;

    private int pathIndex = 0;

    Animator animator;


    private void Awake()
    {
        walkTarget = GameManager.walkPath[pathIndex].transform;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        Vector3 Distance = walkTarget.position - transform.position;
        transform.Translate(Distance.normalized * speed * Time.deltaTime, Space.World);
        // walkTarget = GameManager.walkPath[pathIndex+1].transform;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 Distance = walkTarget.position - transform.position;
        transform.Translate(Distance.normalized * speed * Time.deltaTime, Space.World);
     
        if (Vector3.Distance(transform.position, walkTarget.position) <= 0.1f)
        {
            //Debug.Log(pathIndex + " / " + (GameManager.walkPath.Length-1).ToString());
            if (pathIndex == GameManager.walkPath.Length-1)
            {
                Destroy(gameObject);
                return;
            }
            pathIndex++;
            setWalkpath();

        }
    }

    public void setWalkpath()
    {
        walkTarget = GameManager.walkPath[pathIndex].transform;
    }

    public void EnemyDestroyed()
    {
        StartCoroutine("EnemyDestroyedAnimated");
    }
    IEnumerator EnemyDestroyedAnimated()
    {
        animator.Play("EnemyDestoryed");
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
