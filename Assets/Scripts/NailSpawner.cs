using System.Threading;
using UnityEngine;

public class NailSpawner : MonoBehaviour
{
    public GameObject nail;
    public float timer = 0;
    public float spawnRate = 3;
    public GameObject balloon;
    public float spawnOffset = 8f;
    public LogicScript LogicScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LogicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        Instantiate(nail, transform.position, transform.rotation);      
    }

    // Update is called once per frame
    void Update()
    {
        if (!LogicScript.gameEnded)
        {
            SpawnNail();
        }
    }

    void SpawnNail()
{
    if (timer < spawnRate)
    {
        timer += Time.deltaTime;
    }
    else
    {
        float balloonX = balloon.transform.position.x;
        float left = balloonX - spawnOffset;
        float right = balloonX + spawnOffset;

        float spawnX;

        if (Random.value < 0.5f)
        {
            // 50% chance: spawn at the balloon's x position
            spawnX = balloonX;
        }
        else
        {
            // 50% chance: spawn at a random position within the offset range
            spawnX = Random.Range(left, right);
        }

        Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, 0f);
        Instantiate(nail, spawnPosition, transform.rotation);
        timer = 0;
    }
}
}
