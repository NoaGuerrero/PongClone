using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float initialVelocity = 4f;
    [SerializeField] private float velocityMultiplier = 1.1f;
    [SerializeField] private AudioClip bounceSound;  

    private Rigidbody2D ballRb;
    private AudioSource audioSource;                

    void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();   
        Launch();
    }

    private void Launch()
    {
        float xVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        float yVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        ballRb.velocity = new Vector2(xVelocity, yVelocity) * initialVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (audioSource != null && bounceSound != null)
        {
            audioSource.PlayOneShot(bounceSound);
        }

       
        if (collision.gameObject.CompareTag("Paddle"))
        {
            ballRb.velocity *= velocityMultiplier;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal1"))
        {
            GameM.Instance.Paddle2Scored();
            GameM.Instance.Restart();
            Launch();
        }
        else if (collision.gameObject.CompareTag("Goal2"))
        {
            GameM.Instance.Paddle1Scored();
            GameM.Instance.Restart();
            Launch();
        }
    }
}
