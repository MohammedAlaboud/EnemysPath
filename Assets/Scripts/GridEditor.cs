using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class Act as a helper for level editing and used for building world in a consistent way. Should not affect the game. 
[ExecuteInEditMode] //To control how editor is used
[SelectionBase] //to avoid selecting parts of the cube
[RequireComponent(typeof(Cell))] // automatically adds the cell script component as a dependency
public class GridEditor : MonoBehaviour
{

    Cell cell; //stores the cell component

    private void Awake()
    {
        cell = GetComponent<Cell>(); //to have a reference to the cell component of this object, and add it if its not there
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        LockToGrid(); //used as a snapping tool in the editor to lock translations to positions in the grid, and asks the cell clas where it should lock its position(s) to
        //position coordinate relies on the origin of the world in the editor
        UpdateObjectAndTextMeshLabels(); //changes the name of the object and the text mesh label on the object to the position coordinate in the grid
    }

    private void LockToGrid() 
    {
        int gridCellSize = cell.getGridCellSize(); //get the grid size from the cell
        transform.position = new Vector3(cell.getCellPositionInGrid().x * gridCellSize, 0f, cell.getCellPositionInGrid().y * gridCellSize); // update positions in x and z direction. cubes that make up the ground cannot move in Y plane (for now)
        //reason it is y here is that Vector2's first value is obtained using .x and the second value is obtain using .y (y corresponds to z)
    }

    private void UpdateObjectAndTextMeshLabels()
    {
        TextMesh textMeshGridCellPositionLabel = GetComponentInChildren<TextMesh>(); // variable to store textMesh component. This acts as a label for the coordinate position in the grid; itgets the component from a child (top part of the cube) of this object 
        string gridCellPositionLabel = (cell.getCellPositionInGrid().x) + "," + (cell.getCellPositionInGrid().y); //store the label using the grid positions from the Cell as coordinates 
        //^(dividing by gridCellSize to get coordinates postions in whole numbers rather than increments of 10)
        gameObject.name = gridCellPositionLabel; // change the object's name when duplicating to better see in the hierarchy what each object refers to in the scene
        textMeshGridCellPositionLabel.text = gridCellPositionLabel; //update the text mesh with the label 
    }
}
