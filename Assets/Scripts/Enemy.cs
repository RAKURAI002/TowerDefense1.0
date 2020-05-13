using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    public Color color= Color.blue;

    public int hp;

    

    private float speed = 2f;
    private float slowSpeedMul = 0.5f;

    private bool fearInitial = false;
    private bool fearStatus = false;

    private int damageColor = 255;

    private Transform walkTarget;

    [SerializeField]
    private int pathIndex = 0;

    private GameObject bananaShooted;

    Animator animator;

    private SpriteRenderer sr;

    private void Awake()
    {
        walkTarget = GameManager.walkPaths[pathIndex].transform;
    }
    void Start()
    {

        sr = gameObject.GetComponent<SpriteRenderer>();
        color = Color.blue;
        hp = 3;
       
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

            if (Vector3.Distance(transform.position, walkTarget.position) <= 0.1f || fearInitial == true)
            {
                //Debug.Log(pathIndex + " / " + (GameManager.walkPath.Length-1).ToString());
                if (pathIndex == GameManager.walkPaths.Length - 1)
                {
                    GameManager.gm.EnemyReachEnd();
                    Destroy(gameObject);
                    return;
                }
                if (!fearStatus)
                {
                    pathIndex++;
                    

                }
                else
                {
                     if(pathIndex>0)
                         pathIndex--;
                     fearInitial = false;
                }
                SetWalkpath();
            }
            if(hp == 0)
              {
                  // Debug.Log("Gold ++");
                   GameManager.gm.PlusGold();
                    Destroy(gameObject);
                    return;
                    
              }
          


    }
    public void FearEnemy()
    {

        
        StartCoroutine(Fearing());
    }
    IEnumerator Fearing()
    {
        animator.enabled = true;
        fearStatus = true;
        fearInitial = true;
        animator.SetBool("fear", true);
        yield return new WaitForSeconds(3f);
        animator.SetBool("fear", false);
        fearStatus = false;
        animator.enabled = false;
        sr.color = new Color(1f, damageColor / 255f, damageColor / 255f, 1);
    }
    public void SlowEnemy()
    {
        bananaShooted = transform.Find("BananaShooted").gameObject;
        bananaShooted.SetActive(true);
        StartCoroutine(Slowing());
    }

    IEnumerator Slowing()
    {
        speed *= slowSpeedMul;
        yield return new WaitForSeconds(4f);
        speed *= 1/slowSpeedMul;
        bananaShooted.SetActive(false);
    }
    public void EnemyDamage()
    {
        
        if(hp>=1)
        {
            hp --;
            animator.SetInteger("hp", hp);

            if(damageColor >= 120)
                damageColor -= 120;
            //Debug.Log(damageColor);
            color = new Color(1, damageColor / 255, damageColor / 255, 1); 
            sr.color = new Color(1f, damageColor / 255f, damageColor / 255f, 1);
        }

    }

    public void SetWalkpath()
    {
        walkTarget = GameManager.walkPaths[pathIndex].transform;
    }

    IEnumerator EnemyDestroyed()
    {
        animator.Play("EnemyDestroyed");
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

}
