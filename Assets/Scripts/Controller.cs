using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Transform shotPoint;
    public Transform laser;
    public float moveSpeed;
    public float rotationSpeed;
    public float walkAnimationSpeed;
    public Vector3 velocity;

    private Rigidbody rigidbody;
    private Animator animator;

    public GameObject camera;

    public GameObject bullet;

    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") != 0.0f || Input.GetAxis("Horizontal") != 0.0f)
        {
            Move(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        Shoot();
    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Jump");

            Vector3 forward = laser.transform.position - transform.position;
            Vector3 upward = Vector3.up;

            Quaternion newRotation = Quaternion.LookRotation(forward, upward);
            newRotation.z = 0.0f;
            newRotation.x = 0.0f;
            transform.rotation = newRotation;
            Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        }
    }

    public void Move(float forward, float right)
    {
        Vector3 translation = Vector3.zero;

        translation = forward * camera.transform.forward;
        translation += right * camera.transform.right;
        translation.y = 0;

        if(translation.magnitude > 0.2f)
        {
            velocity = translation;
        }
        else
        {
            velocity = Vector3.zero;
        }

        rigidbody.velocity = new Vector3(velocity.normalized.x * moveSpeed, rigidbody.velocity.y, velocity.normalized.z * moveSpeed);

        if (velocity.magnitude > 0.2f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(velocity), Time.deltaTime * rotationSpeed);
        }

        animator.SetFloat("Velocity", velocity.magnitude * walkAnimationSpeed);
    }

    public void Jump()
    {
        rigidbody.AddForce(Vector3.up * jumpForce);
        animator.SetTrigger("Jump");
    }
}