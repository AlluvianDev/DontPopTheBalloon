using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float moveSpeed = 8;
    public float deadZone;

    public GameObject despawner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        despawner = GameObject.FindWithTag("Despawner");
        deadZone = despawner.transform.position.y;
        print(deadZone);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y <= deadZone)
        {
            Destroy(gameObject);
            
        }
    }
}
