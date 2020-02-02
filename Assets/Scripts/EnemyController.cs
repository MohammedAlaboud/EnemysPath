using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float movementDelay = 0.5f * 2; //How long for an enemy to move between cells
    [SerializeField] ParticleSystem destructionVFX; //emitted when enemy is self-destructs (prefab)


    // Start is called before the first frame update
    void Start()
    {
        GridPathFinder gridPathFinder = FindObjectOfType<GridPathFinder>(); //get a reference to the grid path finder (by finding object of its type since there's only one)
        List<Cell> enemeyPath = gridPathFinder.getSpawnToPlayerBasePath(); //make sure the enemy has access to the path from grid path finder
        StartCoroutine(FollowCellsInPath(enemeyPath)); //coroutine allows for parallel execution and time delay (we specify where and for how long to delay in the routine method)
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator FollowCellsInPath(List<Cell> path) //return IEnumarator (something countable) which is required for coroutine); make enemy follow the path
    {
        print("Advancing...");
        foreach(Cell cell in path) //for every grid cell in the path list given
        {
            transform.position = cell.transform.position; //move the object to the position of the next cell in the path
            yield return new WaitForSeconds(movementDelay); //wait for given time before moving on (Unity executes others things as it delays this; sense of parallel execution)
        }

        destroySelf(); //self destruct when end of path is reached 
    }

    void destroySelf() //the enemy self desctructs when reading the player base (destination)
    {
        ParticleSystem instantiatedDestructionVFX = Instantiate(destructionVFX, transform.position, Quaternion.identity); //instantiate death visual effect separatly from the object as to not destroy it as well, and store it to modify it later
        float destructionDelay = instantiatedDestructionVFX.main.duration; //get access to the duration of the particle system
        instantiatedDestructionVFX.Play(); //play the effect
        Destroy(instantiatedDestructionVFX.gameObject, destructionDelay); //destroy the destruction visual effect after it is done and to ensure it does not stay in the hierarchy
        Destroy(gameObject); //then enemy destroys self
    }
}
