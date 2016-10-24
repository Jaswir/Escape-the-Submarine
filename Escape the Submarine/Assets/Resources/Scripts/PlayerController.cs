using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 1;
    public AudioSource walking;
    public bool isColliding;
    public bool Playing = true;

    private Rigidbody _rb;
    private bool stoppedMoving;
    private CapsuleCollider _capCol;
    private float collisionTimer;

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _capCol = GetComponent<CapsuleCollider>();
    }


    /// <summary>
    /// Activated when door is closed
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag != "Ground" && !GameManager.Instance.capturedFlag)
        {
            isColliding = true;
            AudioManager.Instance.Play(walking, "Collision", false, true);
        }
    }

    void HandleCollisionAudioManagement()
    {
        bool isMoving = _rb.velocity.sqrMagnitude > 0;

        if (isColliding)
        {
            collisionTimer += Time.deltaTime;
            if (collisionTimer > walking.clip.length)
            {
                isColliding = false;
                collisionTimer = 0;
            }

        }

        else
        {
            if (isMoving && !walking.isPlaying)
            {
                walking.Play();
            }
            if (!isMoving && walking.isPlaying || isColliding)
            {
                walking.Stop();
            }
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (!GameManager.Instance.capturedFlag && !GameManager.Instance.drown)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            _rb.velocity = movement * speed;
            HandleCollisionAudioManagement();
        }



    }




}
