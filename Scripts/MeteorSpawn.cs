using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawn : MonoBehaviour
{

    [SerializeField] GameObject[] meteorList;

    private Astronaut astronaut;

    int randomX;
    int randomY;
    // Start is called before the first frame update
    void Start()
    {
        astronaut = GameObject.Find("Player").GetComponent<Astronaut>();
        StartCoroutine(SpawnMeteor());
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(SpawnMeteor());
    }

    private void StartSpawnRoutine()
    {
        
    }

    public IEnumerator SpawnMeteor()
    {
        //yield return new WaitForSeconds(5.0f);
        while (true)
        {
            //randomX = Random.Range(-1097, -1710);
            //randomY = Random.Range(1, 100);
            Instantiate(meteorList[0], new Vector3(Random.Range(-1800  ,-500), Random.Range(50, 300), -1006), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
        
        
    }
}
