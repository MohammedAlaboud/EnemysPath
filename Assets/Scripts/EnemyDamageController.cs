using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageController : MonoBehaviour
{
    [SerializeField] int enemyHitPoints = 10; //enemy starts with 10 HP
    [SerializeField] ParticleSystem hitVFX; //emitted when enemy is hit (prefab)
    [SerializeField] ParticleSystem destructionVFX; //emitted when enemy is destroyed (prefab)
    AudioSource audioSource; //store audio source component to to be able to use multiple times
    [SerializeField] AudioClip hitSound; //sound effect when enemy is hit (needs to be assigned in editor)
    [SerializeField] AudioClip destroyedSound; //sound effect when enemy is destroyed (needs to be assigned in editor)

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); //get the audio source component
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnParticleCollision(GameObject other) //check when something (bullet) collides with enemy (enemy damange and kill processes)
    {
        DamageEnemy(); 
        if(enemyHitPoints <= 0) //if enemy's HP is delpeted 
        {
            ParticleSystem instantiatedDestructionVFX = Instantiate(destructionVFX, transform.position, Quaternion.identity); //instantiate death visual effect separatly from the object as to not destroy it as well, and store it to modify it later
            float destructionDelay = instantiatedDestructionVFX.main.duration; //get access to the duration of the particle system
            instantiatedDestructionVFX.Play(); //play the effect
            Destroy(instantiatedDestructionVFX.gameObject, destructionDelay); //destroy the destruction visual effect after it is done and to ensure it does not stay in the hierarchy
            AudioSource.PlayClipAtPoint(destroyedSound, Camera.main.transform.position); //instantiate the sound effect on the enemy when destroyed as to not destroy sound component as well
            //using playClipAtPoint requires that the sound be placed at a vector 3 position 
            Destroy(gameObject); //then destroy the enemy
        }
        
    }

    void DamageEnemy() //process damage when enemy is hit
    {
        enemyHitPoints--; //decrease enemy HP by 1 when it is hit
        hitVFX.Play(); //play hit particle effect
        audioSource.PlayOneShot(hitSound);
    }
    
}
