using System.Collections.Generic;
using UnityEngine;

public class HazardSpawner : MonoBehaviour
{
    [SerializeField]private GameObject laserPrefab;
    [SerializeField]private List<Transform> spawnPoints = new List<Transform>();
    private float velocity;
    void Start()
    {
        Score.onGetScore += CheckScoreThreshold;
        //HazardObject.Velocity = velocity;
    }

    private void OnDisable()
    {
        Score.onGetScore -= CheckScoreThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        //hoogte van score nodig
        //als de score boven de x komt
        //start met na random tijd lasers spawnen
        //instantiate, tenary voor link of rechts
        //tenary checken met 0 nul punt , midden van het scherm
        //velocity hoort 17 of -17 te zijn

    }
    private void CheckScoreThreshold(Vector2 _ , int __, int score) {
        if (score == 70000) { 
            
        }
    
    }



    private void SpawnLaser() {
        
        GameObject laser = Instantiate(laserPrefab);
        laser.GetComponent<HazardObject>().Velocity = 100f;
    }
}
