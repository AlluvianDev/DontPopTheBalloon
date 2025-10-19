using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public float offset = 8.5f;
    public GameObject cloud;
    public float spawnRate = 3f;
    public float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnCloud();
    }

    void SpawnCloud()
    {
        float rightmost = transform.position.x + offset;
        float leftmost = transform.position.x - offset;
        Vector3 location = new Vector3(Random.Range(leftmost,rightmost),transform.position.y,0);

        if (timer >= spawnRate)
        {
            Instantiate(cloud, location, transform.rotation);
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }

    }
}
