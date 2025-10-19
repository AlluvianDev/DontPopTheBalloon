using UnityEngine;

public class NailMovement : MonoBehaviour
{
    public float moveSpeed = 4;
    public float deadZone = -3;
    public LogicScript LogicScript;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < deadZone)
        {
            Destroy(gameObject);
        }
    }

    public void setNailSpeed(float speed)
    {
        this.moveSpeed = speed;
    }
    
}
