using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

    public GameObject fruitPrefab;
    public Transform[] spawnPoints;

    public float minDelay = .1f;
    public float maxDelay = 1f;

    public float bpm;
    float lastTime, deltaTime, timer;

	// Use this for initialization
	void Start () {
        //StartCoroutine(SpawnFruit());
        lastTime = 0;
        deltaTime = 0;
        timer = 0;
	}

    /*IEnumerator SpawnFruit()
    {
        while (true)
        {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(1f);

            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnIndex];

            Instantiate(fruitPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }*/

    void Update()
    {
        int rand = Random.Range(0, 4);
        deltaTime = GetComponent<AudioSource>().time - lastTime;
        timer += deltaTime;

        if (timer >= (60 / bpm))
        {
            ((GameObject)Instantiate(fruitPrefab, spawnPoints[rand].transform.position, spawnPoints[rand].transform.rotation)).GetComponent<Transform>().parent = GetComponent<Transform>();
            timer -= (60 / bpm);
        }

        lastTime = GetComponent<AudioSource>().time;
    }
}
