using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rb;
    public float speed = 1;
    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Debug.Log(moveHorizontal);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        _rb.velocity = movement * speed;


    }
}
