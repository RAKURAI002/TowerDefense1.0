using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Shell : MonoBehaviour
{
    Animator animator;

    Enemy enemy;

    public Transform firepos;

    private Transform target;
    private float shellSpeed = 10f;
    private float explosionRadius = 1.5f;

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
        }

        else if (gameObject.name == "RocketShell(Clone)")
        {
            RocketFire();
        }
        else if (gameObject.name == "FlameShell(Clone)")
        {
            FlameFire();
        }
        else if (gameObject.name == "BananaShell(Clone)")
        {
            BananaFire();
        }
        else if (gameObject.name == "CSharpShell(Clone)")
        {
            CSharpFire();
        }



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
           // Debug.Log("Cannon Shooting to " + target.position);
            if (Vector3.Distance(transform.position, target.position) <= 0.3)
            {
                Destroy(gameObject);
                enemy.EnemyDamage();
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
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
                //Debug.Log("CoillderS : " + colliders.Length);  
                foreach (Collider2D collider in colliders)
                {
                    Debug.Log("Coillder : " + collider.gameObject.name);
                    if (collider.gameObject.tag == "Enemy")
                    {
                        //Debug.Log("Explosion !!! " + target.position);
                        Enemy enemies = collider.gameObject.GetComponent<Enemy>();
                        enemy.EnemyDamage();
                    }
                }
                //Destroy(gameObject);
                StartCoroutine(ExplosionAnimated());


            }
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    public void FlameFire()
    {
        if (target != null)
        {
/*
            Vector3 distance = target.transform.position - transform.position;

            distance.Normalize();

            Quaternion lookRotation = Quaternion.LookRotation(distance);

            float rotation = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, rotation - 90);*/

            if (Vector3.Distance(transform.position, target.position) <= 0.3)
            {
                enemy.EnemyDamage();
            }
            StartCoroutine(FlameDuration());
        }
        else
        {
            Destroy(gameObject);
        }
    }
    IEnumerator FlameDuration()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    public void CSharpFire()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;

            transform.Translate(direction.normalized * Time.deltaTime * shellSpeed, Space.World);

            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, rotation - 90);
           
            if (Vector3.Distance(transform.position, target.position) <= 0.3)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

                foreach (Collider2D collider in colliders)
                {
                   
                    if (collider.gameObject.tag == "Enemy")
                    {
                        //Debug.Log("Explosion !!! " + target.position);
                        Enemy enemies = collider.gameObject.GetComponent<Enemy>();
                        enemy.FearEnemy();
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
    IEnumerator CSharpFear()
    {
        yield return new WaitForSeconds(5f);

    }
    public void BananaFire()
    {

        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            transform.Translate(direction.normalized * Time.deltaTime * shellSpeed, Space.World);
            // Debug.Log("Cannon Shooting to " + target.position);
            if (Vector3.Distance(transform.position, target.position) <= 0.3)
            {
                Destroy(gameObject);
                enemy.SlowEnemy();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    IEnumerator ExplosionAnimated()
    {
        animator.Play("RocketExplosion");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
