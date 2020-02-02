using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 60f)][SerializeField] float timePerSpawn = 0.75f * 2; //how much time between each enemy spawn; also cannot set this to zero
    [SerializeField] EnemyController enemy; //the next enemy gameObject to spawn(which will be made from prefab) 
    [SerializeField] GameObject objectParent; //to make sure enemies are added under this parent in the hierarchy
    [SerializeField] Text scoreText; //need to assign the UI's relavent text field that will display the player score
    int playerScore = 0; //stores the player's score (based on number of enemies spawned)
    [SerializeField] AudioClip spawnSound; //sound effect when enemy is spawned (needs to be assigned in editor)

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
        scoreText.text = "Score: " + playerScore.ToString(); //the score text displayed in the UI is the playerScore value here
    }

    //corouting method
    IEnumerator SpawnEnemies()
    {
        while (true) //FOREVER
        {
            scoreText.text = "Score: " + playerScore.ToString(); //update the player score on UI
            yield return new WaitForSeconds(timePerSpawn); //delay between enemy spawns
            GetComponent<AudioSource>().PlayOneShot(spawnSound); //play spawn sound effect when enemy spawns
            var newEnemy = Instantiate(enemy, transform.position, Quaternion.identity); //make an enemy at the spawn position and do not rotate it and store it for modifying later
            newEnemy.transform.parent = objectParent.transform; //where the parent of the object is set
            playerScore = playerScore + 10; //increase the player score by 10 per enemy spawned
            scoreText.text = "Score: " + playerScore.ToString(); //update the player score on UI again just in case
        }

    }
        
    // Update is called once per frame
    void Update()
    {
    }
}
