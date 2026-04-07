using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class HazardSpawner : MonoBehaviour
{
    public static event Action onTimerHit;
    [SerializeField]private GameObject laserPrefab;
    [SerializeField]private List<GameObject> spawnPoints = new List<GameObject>();
    private float velocity;
    private bool scoreCheck;
    [SerializeField]private float timer;
    [SerializeField]private int randomizedTime;
    [SerializeField] private int i;

    public static event Action onHazardWarning;
    [SerializeField] private float warningTimer = 0f;
    private float spawnTimer = 0f;
    private GameObject spawnObject;
    private SpriteRenderer spriteRenderer;
    private bool visibility = false;


    void Start()
    {
        Score.onGetScore += CheckScoreThreshold;
        HazardSpawner.onHazardWarning += SpawnLaser;
        HazardSpawner.onTimerHit += WarningEffect;

        randomizedTime = UnityEngine.Random.Range(5, 20);


        
        spawnObject = spawnPoints[i];

        spriteRenderer = spawnObject.GetComponent<SpriteRenderer>();
    }

    private void OnDisable()
    {
        Score.onGetScore -= CheckScoreThreshold;
        HazardSpawner.onHazardWarning -= SpawnLaser;
        HazardSpawner.onTimerHit -= WarningEffect;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) {
            SpawnLaser();
        }

        if (visibility == true)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
            spriteRenderer.enabled = true;
        }

        RandomTimer();

    }
    private void CheckScoreThreshold(Vector2 _ , int __, int score) {
        if (score >= 70000) { 
            scoreCheck = true;
        }
    
    }

    private void RandomTimer()
    {
        //if (scoreCheck == false) return;
        timer += Time.deltaTime;
        if (timer > randomizedTime) {
            onTimerHit?.Invoke();
        }

    }

    private void WarningEffect()
    {
        warningTimer += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        

        spawnObject = spawnPoints[i];

        spriteRenderer = spawnObject.GetComponent<SpriteRenderer>();

        if (spawnTimer > 4f)
        {
            onHazardWarning?.Invoke();
            spawnTimer = 0f;
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

        i = UnityEngine.Random.Range(0, spawnPoints.Count);

        timer = 0f;
        randomizedTime = UnityEngine.Random.Range(5, 20);
    }
}
