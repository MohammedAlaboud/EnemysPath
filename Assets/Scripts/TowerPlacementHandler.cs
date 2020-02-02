using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacementHandler : MonoBehaviour
{
    [SerializeField] TowerController tower; //the cells can have towers placed on them, and it should not be possible to place two towers on the same cell. Copies of the tower will come from the prefab (NOT instances)
    [SerializeField] int towerMaxLimit = 5; //maximum number of towers that can be added into the game
    [SerializeField] GameObject objectParent; //to make sure towers are added under this parent in the hierarchy

    //this acts as a ring buffer to recyle the towers once the max number of towers on the grid is reached
    Queue<TowerController> towerQ = new Queue<TowerController>(); //queue of towers which will be used to keep a maximum of towerMaxLimit towers on the grid and recycle the towers to be placed on new cells

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddTower(Cell cell) //adds a tower to the specified cell; this process recylces the towers rather than destroying and reinstantiating them (yes, efficiency)
    {
        print(towerQ.Count);
        int numberOfTowersFound = towerQ.Count; //should be the number of towers in the scene as the queue is modified
        Vector3 yIncrease = new Vector3(0f, 3f, 0f); //correction for placement

        if (numberOfTowersFound < towerMaxLimit) //only add towers if we have less than the set limit
        {
            TowerController newTower = Instantiate(tower, cell.transform.position + yIncrease, Quaternion.identity); //add a tower on that cell if possible, this tower is stored in a variable
                                                                                                                     //the tower is instantiated inside the block rather than on it, so it is instantiated just above it by adding the yIncrease vector

            newTower.transform.parent = objectParent.transform;//where the parent of the object is set

            newTower.placedOn = cell; //the tower will know what cell is has been placed on
            cell.available = false; //cannot place another tower on the same block

            towerQ.Enqueue(newTower); //enqueue the instantiated tower
        }
        else //otherwise move earliest placed existing tower to new chosen cell and update data
        {
            var earliestPlacedTower = towerQ.Dequeue(); //remove the tower (one that was placed earliest) off front the queue

            //when moving the oldest tower, we need to know what became available and what is not available anymore
            earliestPlacedTower.placedOn.available = true;
            cell.available = false;

            earliestPlacedTower.placedOn = cell; //update the cell that the moved tower is now placed on

            earliestPlacedTower.transform.position = cell.transform.position + yIncrease; //actually move the tower to the new cell

            towerQ.Enqueue(earliestPlacedTower); //readd the tower to the back of the queue (meaning this tower is the most recently placed one) 
        }
        
    }
}
