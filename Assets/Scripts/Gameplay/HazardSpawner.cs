using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HazardSpawner : MonoBehaviour
{
    public static event Action onTimerHit;
    public static event Action onHazardWarning;

    private float velocity;
    private bool scoreCheck;
    private float spawnTimer = 0f;
    private bool visibility = false;
    private bool warningActive = false;
    private List<SpriteRenderer> spriteRenderers = new List<SpriteRenderer>();

    [SerializeField]private GameObject laserPrefab;
    [SerializeField]private List<GameObject> spawnPoints = new List<GameObject>();
    private float timer;
    private int randomizedTime;
    private int i;    
    private float warningTimer = 0f;
   
    

    void Start()
    {
        Score.onGetScore += CheckScoreThreshold;
       
        randomizedTime = UnityEngine.Random.Range(3, 6);


        foreach (var p in spawnPoints) {
 

            spriteRenderers.Add(p.GetComponent<SpriteRenderer>());

            p.GetComponent<SpriteRenderer>().enabled = false;
        }

        i = UnityEngine.Random.Range(0, spawnPoints.Count);

    }


    private void OnDisable()
    {
        Score.onGetScore -= CheckScoreThreshold;
    }

   

    void Update()
    {

        if (warningActive == true)
        {
            if (visibility == true)
            {
                spriteRenderers[i].enabled = false;
            }
            else
            {
                spriteRenderers[i].enabled = true;
            }
        }

        RandomTimer();

    }
    private void CheckScoreThreshold(Vector2 _ , int __, int score) {
        if (score >= 10000) { 
            scoreCheck = true;
        }
    
    }

    private void RandomTimer()
    {
        if (scoreCheck == false) return;
        timer += Time.deltaTime;
        if (timer > randomizedTime) {
            onTimerHit?.Invoke();   // wordt gebruikt in sound script

            WarningEffect();                     
        }

    }

    private void WarningEffect()
    {
        warningTimer += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        warningActive = true;
               

        if (spawnTimer > 2f)
        {
            onHazardWarning?.Invoke();//ook voor sound
            SpawnLaser();
            spawnTimer = 0f;
            warningActive = false;
        }

        if (warningTimer < 0.5) return;
        visibility = !visibility;
        warningTimer = 0;
    }

    private void SpawnLaser() {


        GameObject laser = Instantiate(laserPrefab);

        float vel = 0f;
        

        Vector3 position = spawnPoints[i].transform.position;

        vel = (position.x < 0) ? 17f : -17f;
        laser.transform.position = position;

        laser.GetComponent<HazardObject>().Velocity = vel;

      

        timer = 0f;
        randomizedTime = UnityEngine.Random.Range(3, 6);
        
        spriteRenderers[i].enabled = false;
        i = UnityEngine.Random.Range(0, spawnPoints.Count);
    }
}
