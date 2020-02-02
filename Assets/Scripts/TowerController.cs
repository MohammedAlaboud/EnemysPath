using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] Transform towerPartToRotate; //the top part of the tower that will pivot
    [SerializeField] float towerRange = 25f; //what distance the tower can target the enemy from (value obtained by experimenting)
    [SerializeField] ParticleSystem bullets; //for assigning its own particle system when the enemy is in range

    Transform target; //the enemy the tower is targetting (will be targeted depending on if it is in range and can change)
    public Cell placedOn; //the cell that this tower is placed on

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        lockOnTarget(); //find or set the enemy this will target. Tower will always choose nearest target (NOT first one in line)
        if (target) //only process firing if enemy exists 
        {
            towerPartToRotate.LookAt(target); //LookAt rotates the transform so that the forward vector points at the target
            TargetEnemy();
        }
        else //otherwise stop shooting
        {
            fireAtTarget(false);
        }
        
    }

    private void lockOnTarget()
    {
        var spawnedEnemies = FindObjectsOfType<EnemyDamageController>(); //find enemies in the scene
        if (spawnedEnemies.Length == 0) //if none are found 
        { return; } //cannot set target, so exit 

        Transform nearestEnemy = spawnedEnemies[0].transform; //assume the first enemy is nearest at the start

        foreach(EnemyDamageController targetToCheck in spawnedEnemies) //for every enemy in the scene
        {
            nearestEnemy = findNearestTarget(nearestEnemy, targetToCheck.transform); //find the closer one between the enemy being checked and the current nearest one
        }

        target = nearestEnemy; //set the target as the closest enemy
    }

    private Transform findNearestTarget(Transform first, Transform second) //compares two transforms to find out which of the two is the nearest to the tower
    {
        var distanceToFirstTarget = Vector3.Distance(transform.position, first.position); //work out the distance between the tower and the first transform
        var distanceToSecondTarget = Vector3.Distance(transform.position, second.position); //work out the distance between the tower and the second transform
        if (distanceToFirstTarget < distanceToSecondTarget) //if the first transform is closer then return it
        {
            return first;
        }
        else if (distanceToSecondTarget < distanceToFirstTarget) //if the second transform is closer then return it
        {

            return second; 
        }

        
        return first; //just in case distance is the same which shouldn't happen, return the first (first has priority) one to avoid error
    }

    private void TargetEnemy()
    {
        float distanceToTarget = Vector3.Distance(target.transform.position, gameObject.transform.position); //find out the distance between the tower and the enemy
        if(distanceToTarget <= towerRange) //if the enemy is in range then fire at it, otherwise don't
        {fireAtTarget(true);}
        else
        {fireAtTarget(false);}
    }

    private void fireAtTarget(bool fire)
    {
        var particleEmission = bullets.emission; //get a reference to the emission module of the bullets particle system
        particleEmission.enabled = fire; //activate particle emission if told to do so 
    }
}
