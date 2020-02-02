using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthHandler : MonoBehaviour
{
    [SerializeField] int playerHealth = 10; //player health, dependent on the player base, and decreases by a specified amount if an enemy reaches the player base
    [SerializeField] Text healthText; //need to assign the UI's relavent text field that will display the player HP
    [SerializeField] AudioClip baseDamageSound; //sound effect when enemy reaches base and player is damaged (needs to be assigned in editor)
    SceneLoader sc;

    // Start is called before the first frame update
    void Start()
    {
        healthText.text = "HP: " + playerHealth.ToString(); //the HP text displayed in the UI is the playerHealth value here
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other) //if an enemy (other collider) enters this object's collider
    {
        if(playerHealth > 0)
        {
            playerHealth = playerHealth - 1; //the player health is decreases by 1;
            healthText.text = "HP: " + playerHealth.ToString(); //update the UI HP text when it is changed here
            GetComponent<AudioSource>().PlayOneShot(baseDamageSound); //sound played when player base is hit
        }
        else
        {
            returnToMenu(); //if the player HP reaches zero, then return to the main menu
        }
    }

    public void returnToMenu() //called to return to the main menu
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); //laods the main menu scene
    }


}
