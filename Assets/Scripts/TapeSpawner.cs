using JetBrains.Annotations;
using UnityEngine;

public class TapeSpawner : MonoBehaviour
{
    public GameObject tape;
    public float offsetX = 8.2f;
    public float offsetY = 4.4f;
    public float timer = 0;
    public float spawnRate = 3; //x saniyede bir bant doğar.
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        spawnTape();
    }

    void spawnTape()
    {
        if (timer >= spawnRate)
        {
            float upMostY = transform.position.y + offsetY;
            float downMostY = transform.position.y - offsetY;
            float leftMostX = transform.position.x - offsetX;
            float RightMostX = transform.position.x + offsetX;

            Vector3 location = new Vector3(Random.Range(leftMostX, RightMostX), Random.Range(downMostY, upMostY), 0);
            Instantiate(tape, location, transform.rotation);
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
