using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    public void returnToMenu() //called to return to the main menu when the button to return to menu is clicked
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); //laods the main menu scene
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
