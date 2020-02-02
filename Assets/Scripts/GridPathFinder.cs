using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPathFinder : MonoBehaviour
{ //the pathfinder is not designed to run more than once, I will look into this for future projects but not for this one due to time limitations
    Dictionary<Vector2Int, Cell> grid = new Dictionary<Vector2Int, Cell>(); //stores a list of key-value pairs, keys refer to the coordinates of a cell, and values refer to the cell itself
    Queue<Cell> q = new Queue<Cell>(); //a queue that will be used for the pathfinding Breadth First Search based algorithm

    bool isFindingPath = true; //bool to check if we are currently executing the path finding algorithm or not
    Cell currentCell; //keep track of current cell checking neighbours from

    //to specify where in the grid are the start and end points in the grid
    [SerializeField] Cell spawnCell; //where enemies spawn
    [SerializeField] Cell destinationCell; //player base

    private List<Cell> spawnToPlayerBasePath = new List<Cell>(); //list of cells that make up the path from the spawn point to the player base (not populated yet)

    //Array of directions to adjacent cells (diaganols don't count), directions rely on pair of values to specify movement in x and z directions
    Vector2Int[] neighbours = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left};

    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame
    void Update()
    {    
    }

    public List<Cell> getSpawnToPlayerBasePath() //process to find the path and make sure it is accessible to an enemy
    {
        //to avoid errors, this has become limited to work with finding one path only at the moment
        if (spawnToPlayerBasePath.Count == 0) //only do this if haven't already calculated a path as to not do the whole process again for another enemy
        {
            LoadCells();
            ColorSpawnAndDestinationCells();
            SearchForPath();
            SetPath();
        }
            return spawnToPlayerBasePath;
    }

    private void SetPath()
    {
        spawnToPlayerBasePath.Add(destinationCell); //we will construct the path backwards from destination to spawn with the help of 'checkedFrom', then reverse and reorganize it to be from spawn to distination
        destinationCell.available = false; //destination cell is not available for placing tower on
        Cell precedingCell = destinationCell.checkedFrom; //store the cell that the destination was reached from
        while (precedingCell != spawnCell) //while the preceding cell is not the spawn cell (while we haven't reached the start)
        {
            spawnToPlayerBasePath.Add(precedingCell); //continually add the preceding cells to make up the path
            precedingCell.available = false; //every cell in the path is not available for placing towers on
            precedingCell = precedingCell.checkedFrom; //and move backwards to the what this cell was checked from
        }
        spawnToPlayerBasePath.Add(spawnCell); //add start/spawn cell to the list after it has been populated with the correct cells (in order from destination to spawn)
        spawnCell.available = false; //spawn cell is not available for placing tower on
        spawnToPlayerBasePath.Reverse(); //reverse the list to set its elements up in the correct order (spawn to destination)
    }

    private void SearchForPath() //based on Breadth First Search pathfinding 
    {
        q.Enqueue(spawnCell); //add the spawn or starting position in the queue

        while(q.Count > 0 && isFindingPath) //while there's something in the queue and still finding a path, loop...
        {
            currentCell = q.Dequeue(); //check the adjacent cells of first cell in the queue
            //halt 
            if (currentCell == destinationCell) //if the cell we are searching from (checking its neighbours) is the destination cell
            {
                isFindingPath = false;
            }
            checkAdjacetCells(); //check adjacent cells of current cell
            currentCell.alreadyChecked = true; //mark a cell as checked once it has been taken out of the queue (used to not check the same cell again)
        }
        
    }

    private void checkAdjacetCells() //adjacent cells refers to cells that neighbour one another. Check adjacent cells of given parameter cell 
    {
        if (!isFindingPath) { return; }; //if we are not still finding a path, then no need to execute the rest of this method

        foreach(Vector2Int neighbour in neighbours)
        {
            Vector2Int adjacentPosition = currentCell.getCellPositionInGrid() + neighbour; //we combine or add up the two coordinates to get vector addition to traverse the grid
            if(grid.ContainsKey(adjacentPosition)) //if it exists in the grid, then queue it, otherwise don't bother
            {
                //queue new neighbours                
                if(grid[adjacentPosition].alreadyChecked || q.Contains(grid[adjacentPosition])) //look up the adjacent cell from the dictionary given the position and check if this cell has already been checked before or if it is already in the queue
                { 
                    //do  nothing
                }
                else { //otherwise put it in the queue
                    q.Enqueue(grid[adjacentPosition]); //and also push this smae cell into the queue (back of q) 
                    grid[adjacentPosition].checkedFrom = currentCell; //keep track of which cell checked the current cell for every cell
                }
                
            }
        }
    }

    private void ColorSpawnAndDestinationCells() //set the start and end point colors on the grid (cell colors) MAINLY FOR DEBUGGING PURPOSES
    {
        spawnCell.setCellColor(Color.green); //GREEN
        destinationCell.setCellColor(Color.red); //RED
    }

    private void LoadCells()
    {
        var cells = FindObjectsOfType<Cell>(); //get a list of all the cells in the grid (get active loaded objects of type Cell)
        foreach (Cell c in cells) //For every cell c found

            if (grid.ContainsKey(c.getCellPositionInGrid())) //if we found another cell c in the dictionary with the same position (overlapping cells) then log the warning and don't add it to the dictionary
            {
                print("Not adding overlapping block " + c);
            }
            else //otherwise added it to the grid dictionary 
            {
                grid.Add(c.getCellPositionInGrid(), c); //add it to the grid dictionary as a pair of cell coordinates and the cell itself
            }
    }
}
