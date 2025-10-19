using System.Threading;
using UnityEngine;
using System.Collections;
public class Balloon : MonoBehaviour
{
    public Rigidbody2D balloon;
    public float moveSpeed = 8;
    public float naturalMovementTimer = 0;
    public float naturalMovementRate = 1f;
    public bool balloonIsAlive = true;
    public bool tapeCollected = false;
    public LogicScript LogicScript;
    public float speedBoost = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LogicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if (balloonIsAlive)
        {
            float velocityX = 0;
            float velocityY = 0;

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                velocityX = moveSpeed;
                Rotate(-23);
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                velocityX = -moveSpeed;
                Rotate(23);   
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                velocityY = moveSpeed;
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                velocityY = -moveSpeed;
            }

            if (velocityX != 0 || velocityY != 0)
            {
                balloon.linearVelocity = new Vector3(velocityX * speedBoost, velocityY * speedBoost, 0);
            }
            else
            {
                Rotate(0);
                balloon.linearVelocity = Vector3.zero;
                NaturalMovement();
            }
        }
        else
        {
            balloon.linearVelocity = Vector3.zero;
        }
    }
    void NaturalMovement()
    {
        if (naturalMovementTimer < naturalMovementRate / 2)
        {
            balloon.linearVelocity = new Vector3(0, naturalMovementTimer * 0.3f, 0);
        }
        else if (naturalMovementTimer < naturalMovementRate)
        {
            balloon.linearVelocity = new Vector3(0, -naturalMovementTimer * 0.3f, 0);
        }
        naturalMovementTimer += Time.deltaTime;
        if (naturalMovementTimer >= naturalMovementRate)
        {
            naturalMovementTimer = 0;
        }
    }

    void Rotate(int angle)
    {
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);
    }
    IEnumerator BoostOnTapeAcquired()
    {
        float countdown = 3;
        speedBoost = 2.2f;

        while (countdown > 0)
        {
            countdown -= Time.deltaTime;
            yield return null;
        }
            speedBoost = 1f;

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            Debug.Log("Hit layer: " + collision.gameObject.layer + " — " + collision.gameObject.name);
            balloonIsAlive = false;
            LogicScript.gameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!tapeCollected && collision.CompareTag("Tape"))
        {
            tapeCollected = true;
            //collision.enabled = false;
            Destroy(collision.gameObject);
            LogicScript.addTape(1);
            StartCoroutine(BoostOnTapeAcquired());
            tapeCollected = false;
        }
    }
}
