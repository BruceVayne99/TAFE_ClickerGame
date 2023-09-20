using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
public class SpawnShape : MonoBehaviour
{
    #region Variables
    public GameObject[] shapesTemplate; //Prefab
    public TMP_Text spawnCountText; //defines Cost Text
    private int spawnCount = 0;//Keeps track of how many times we pressed the button.
    public TMP_Text spawnMultiplierText;//defines multiplier text
    public int spawnMultiplier = 1; //starts the multiplier at 1
    public TMP_Text costMultiplierText; //defines cost until next upgrade
    public List<GameObject> spawnedObjects; //creates a list that keeps track of all spawned objects
    public Rigidbody2D rb; //defines the rigidbodies of spawned objects
    public int launchForce = 3; //sets the force at which objects are bounced


    #endregion

    #region Unity Event Functions
    public void Start()
    {
        UpdateUI(); //refreshes the UI
        spawnedObjects = new List<GameObject>(); //defines the spawn list
    }
    #endregion

    #region Functions
    public void Spawn()
    {
        //loops from 0 to the value of spawn multiplier
        for (int loop = 0; loop < spawnMultiplier; loop++)
        {
            spawnCount++; //Increase counter by 1
            UpdateUI();
            float randomX = Random.Range(-0.1f, 0.1f); //Find a random number for the X axis
            int randomIndex = Random.Range(0, shapesTemplate.Length); //sets the length of the array for random shapes
            GameObject randomTemplate = shapesTemplate[randomIndex]; //chooses a random shape from the array
            GameObject spawnedNewGameObject = Instantiate(randomTemplate, new Vector2(randomX, 6f), Quaternion.identity) as GameObject; //spawns the object in
            spawnedObjects.Add(spawnedNewGameObject); //adds spawned object to list

        }
    }
    public void DeleteObjects()
    {
        //Loop backwards when deleting from an array or list
        for (int loop = spawnedObjects.Count - 1; loop >= 0; loop--)
        {
            GameObject deleteGameObject = spawnedObjects[loop];
            Destroy(deleteGameObject); //deletes all objects in list
            spawnedObjects.RemoveAt(loop); //remove objects from list
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Trampoline"))
        {
            var rnd = Random.insideUnitCircle; //chooses a random direction within 360 degrees
            rb.velocity = new Vector2(rnd.x, rnd.y) * launchForce; //adds a force to the object in the chosen direction 
        }
    }

    public void Upgrade()
    {
        if (spawnCount >= 10 * spawnMultiplier)
        {
            spawnCount -= 10 * spawnMultiplier; //minuses upgrade cost from amount spawned
            int randomY = Random.Range(1, 5); //decides on a random number between 1 and 5 to increase the spawnmultiplier by
            spawnMultiplier = spawnMultiplier + randomY; //increases spawn  multiplier by random number
            launchForce++; //increases bounciness (don't think this actually works)
            UpdateUI();
            DeleteObjects(); //calls the delete function
        }

    }
    public void UpdateUI()
    {
        spawnCountText.text = "Money: $" + spawnCount.ToString(); //changes text
        spawnMultiplierText.text = "Spawn Multiplier: " + spawnMultiplier; //changes text
        costMultiplierText.text = "Next Upgrade: $" + (10 * spawnMultiplier); //changes text
    }

}
    #endregion