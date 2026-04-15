using UnityEngine;

public class ProcBackground : MonoBehaviour
{
    [SerializeField] private GameObject nebulaPrefab;
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private int maxCountNebula = 30;
    [SerializeField] private int maxCountStar = 20;
    [SerializeField] private Rect area;
    [SerializeField] private float maxScaleNebula = 3f;
    [SerializeField] private float minScaleNebula = 1f;
    [SerializeField] private float maxScaleStar = 0.5f;
    [SerializeField] private float minScaleStar = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < maxCountNebula; i++)
        {
            GameObject nebula = Instantiate(nebulaPrefab,new Vector3(Random.Range(-area.width/2, area.width/2), Random.Range(-area.height / 2, area.height / 2),0),Quaternion.Euler(0,0,Random.Range(0,360)));
            nebula.transform.localScale = new Vector3(Random.Range(minScaleNebula, maxScaleNebula), Random.Range(minScaleNebula, maxScaleNebula), 0);
        }
        for (int i = 0; i < maxCountStar; i++)
        {
            float scale = Random.Range(minScaleStar, maxScaleStar);
            GameObject star = Instantiate(starPrefab, new Vector3(Random.Range(-area.width / 2, area.width / 2), Random.Range(-area.height / 2, area.height / 2), 0), Quaternion.Euler(0, 0, Random.Range(0, 360)));
            star.transform.localScale = new Vector3(scale, scale, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
