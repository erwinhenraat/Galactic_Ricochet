using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class HazardSpawner : MonoBehaviour
{
    public static event Action onTimerHit;
    [SerializeField]private GameObject laserPrefab;
    [SerializeField]private List<Transform> spawnPoints = new List<Transform>();
    private float velocity;
    private bool scoreCheck;
    private float timer;
    private int randomizedTime;


    void Start()
    {
        Score.onGetScore += CheckScoreThreshold;
        HazardWarning.onHazardWarning += SpawnLaser;
        randomizedTime = UnityEngine.Random.Range(5, 20);
    }

    private void OnDisable()
    {
        Score.onGetScore -= CheckScoreThreshold;
        HazardWarning.onHazardWarning -= SpawnLaser;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space)) {
            SpawnLaser();
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



    private void SpawnLaser() {


        GameObject laser = Instantiate(laserPrefab);

        float vel = 0f;

        int i = UnityEngine.Random.Range(0, spawnPoints.Count);

        Vector3 position = spawnPoints[i].transform.position;

        vel = (position.x < 0) ? 17f : -17f;
        laser.transform.position = position;

        laser.GetComponent<HazardObject>().Velocity = vel;

        timer = 0f;
        randomizedTime = UnityEngine.Random.Range(5, 20);
    }
}
