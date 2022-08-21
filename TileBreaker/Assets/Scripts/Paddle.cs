using UnityEngine;

public class Paddle : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set; }
    public Vector2 direction { get; private set; }
    public float speed = 30f;
    public float maxBounceAngle = 75f;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PaddleMovement();
    }
    private void FixedUpdate()
    {
        if(this.direction != Vector2.zero) { 
            this.rigidbody.AddForce(this.direction * this.speed);
        }
    }

    private void PaddleMovement()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.direction = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.direction = Vector2.right;
        }
        else
        {
            this.direction = Vector2.zero;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) // collision with ball logic
    {
        Ball ball = collision.gameObject.GetComponent<Ball>(); // if collison is with 'ball'...

        if(ball !=null) // ball bounce off depending on which side of paddle it collides with
        {
            Vector3 paddlePosition = this.transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float offset = paddlePosition.x - contactPoint.x;
            float width = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.rigidbody.velocity); // angle of the ball 
            float bounceAngle = (offset / width) * this.maxBounceAngle;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -this.maxBounceAngle, this.maxBounceAngle);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.rigidbody.velocity = rotation * Vector2.up * ball.rigidbody.velocity.magnitude;

        }
    }
    
    public void ResetPaddle()
    {
        this.transform.position = new Vector2(0f,this.transform.position.y);
        this.rigidbody.velocity = Vector2.zero;
    }
}
