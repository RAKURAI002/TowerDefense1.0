using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Shell : MonoBehaviour
{
    Animator animator;

    Enemy enemy;

    private Transform target;
    private float shellSpeed = 10f;
    private float rocketRadius = 3f;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.name == "CannonShell(Clone)")
        {
            CannonFire();
            Debug.Log("Cannon Shooting ");
        }
           
        else if (gameObject.name == "RocketShell(Clone)")
            RocketFire();


    }
    public void setTarget(GameObject _target)
    {
        target = _target.transform;
        enemy = _target.GetComponent<Enemy>();
    }
    public void CannonFire()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction.normalized * Time.deltaTime * shellSpeed, Space.World);
            Debug.Log("Cannon Shooting to " + target.position);
            if (Vector3.Distance(transform.position, target.position) <= 0.3)
            {
                //animator.Play("EnemyDestroyed");
                Destroy(gameObject);
                enemy.EnemyDestroyed();
                //Destroy(target.gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void RocketFire()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;

            transform.Translate(direction.normalized * Time.deltaTime * shellSpeed, Space.World);
            
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, rotation - 90);
            //Debug.Log("Rocket Shooting to " + target.position);
            if (Vector3.Distance(transform.position, target.position) <= 0.3)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, rocketRadius);
                foreach (Collider collider in colliders)
                {
                    if(collider.gameObject.tag == "Enemy")
                    {
                        Debug.Log("Explosion !!! " + target.position);
                        Enemy enemies = collider.gameObject.GetComponent<Enemy>();
                        enemies.EnemyDestroyed();
                    }
                }
                Destroy(gameObject);
                //StartCoroutine(ExplosionAnimated());


            }
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    IEnumerator ExplosionAnimated()
    {
        //animator.Play("RocketEffect");
        yield return new WaitForSeconds(0.4f);
        Destroy(gameObject);
    }
}
