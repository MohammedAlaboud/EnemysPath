using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour //can work out its grid position regardless of what the grid editor class is doing. Not dependent on GridEditor class.
{
    const int gridCellSize = 10; //Length of any side of a cell in a grid (change to adjust the movement of the object in units of this size) should be consistent in all scenes to avoid having different parameters for future implementation
    Vector2Int positionCoordinates; //A pair of int values to act as x and z coordinates for positions in the grid
    public bool alreadyChecked = false; //a public variable to freely check if the cell has been checked in the pathfinding algorithm or not
    public Cell checkedFrom; //public variable to keep track in each cell which other cell it was checked of discovered from 
    public bool available = true; //check to see if cell is blocked or in the enemy path to determine if something can be placed on this cell (no if its in path)


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public int getGridCellSize() //Getter method to return the gridCellSize to the editor. GridCellSize should not be modified (read only access)
    {
        return gridCellSize;
    }

    public Vector2Int getCellPositionInGrid() //work out the position of this cell in the grid
    {
        //round to whole number and lock to grid position in movements of gridCellSize units for x and z translations
        return new Vector2Int(Mathf.RoundToInt(transform.position.x / gridCellSize), Mathf.RoundToInt(transform.position.z / gridCellSize));//stored in coordinate pair int values
    }

    public void setCellColor(Color newColor) //color the cell's top (top not visible at the moment) MAINLY FOR DEBUGGING PURPOSES
    {
        MeshRenderer topSurfaceMR = transform.Find("Top").GetComponent<MeshRenderer>(); //find the child of this object with the given name and get its mesh renderer 
        topSurfaceMR.material.color = newColor; //change the color of that mesh renderer to the given color
    }

    void OnMouseOver() //player will have mouse control to place towers, this is used to check which cell is clicked on
    {
        Vector3 yIncrease = new Vector3(0f, 3f, 0f);

        if (Input.GetMouseButtonDown(0)) //if this object is left clicked
        {
            if (available) //and if the cell is availble to be placed on (not on enemy path)
            {
                FindObjectOfType<TowerPlacementHandler>().AddTower(this); //find the object of type TowerPlacementHandler and this should cause no issues as there should only be one of this in the scene
                //and call the add tower method from the TowerPlacementHandler given this object (cell) as a param
            }
            else
            {
                print(gameObject.name + " CANNOT PLACE TOWER HERE");
            }
        }
    }

}
