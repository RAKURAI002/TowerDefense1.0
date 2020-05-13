using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{


    public float fireRange = 3f;
    private const string enemyTag = "Enemy";

    public float rotateSpeed = 20f;

    public GameObject target = null;
    public GameObject Shell;

    public Transform RotatePart;
    public Transform firePos;

    private float fireRate = 2f;
    private float fireCooldown;

    // Start is called before the first frame update
    void Start()
    {
        fireCooldown = 1f / fireRate;
        InvokeRepeating("EnemyTrack", 1f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
            return;
       
        Vector3 distance = target.transform.position - transform.position;

        distance.Normalize();

        Quaternion lookRotation = Quaternion.LookRotation(distance);

        //Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;

        //cannonPart.rotation = Quaternion.Euler(0, 0, rotation.z);
        //cannonPart.rotation = Quaternion.Euler(0, 0, lookRotation.z);

        float rotation = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg;

        RotatePart.rotation = Quaternion.Euler(0f, 0f, rotation - 90);

        //cannonPart.rotation = Quaternion.Euler(0, 0, Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, rotation - 90), Time.deltaTime).z);

       // cannonPart.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, rotation + 180), rotateSpeed * Time.deltaTime);

        //Destroy(target.gameObject);



        //Debug.Log("Try to Rotate . . ." + rotation.z);

        if (fireCooldown <= 0)
        {
            shoot();
            fireCooldown = 1f / fireRate;
        }
        fireCooldown -= Time.deltaTime;
    }
    void shoot()
    {
        GameObject bullet = Instantiate(Shell, firePos.position, firePos.rotation);
        //Debug.Log("Shoot :  " + firePos.position);
        Shell shell = bullet.GetComponent<Shell>();
        shell.setTarget(target);
    }
    void EnemyTrack()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject nearestEnemy = null;
        float nearestEnemyDist = Mathf.Infinity;


        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if(distance<nearestEnemyDist)
            {
                nearestEnemy = enemy;
                nearestEnemyDist = distance;
            }
            
        }
        if(nearestEnemy != null && nearestEnemyDist <= fireRange)
        {
            target = nearestEnemy;
        }
        else
        {
            target = null;
        }


    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireRange);
    }

}
