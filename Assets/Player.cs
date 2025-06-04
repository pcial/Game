using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 7f;
    public float jump = 7f;
    private bool Grounded=false;
    Rigidbody rb;

    public float grappleSpeed = 20f;

    private bool isGrapplingLeft = false;
    private bool isGrapplingRight = false;

    private Vector3 grapplePointLeft;
    private Vector3 grapplePointRight;

    public LineRenderer lineLeft;
    public LineRenderer lineRight;



    void Start()
    {
        rb = GetComponent<Rigidbody>();

        GameObject leftLineObj = new GameObject("LeftGrappleLine");
        lineLeft = gameObject.AddComponent<LineRenderer>();
        lineLeft.startWidth = 0.05f;
        lineLeft.endWidth = 0.05f;
        lineLeft.material = new Material(Shader.Find("Sprites/Default"));
        lineLeft.startColor = Color.blue;
        lineLeft.endColor= Color.blue;
        lineLeft.positionCount = 0;


        GameObject RightLineObj = new GameObject("LeftGrappleLine");
        lineRight = gameObject.AddComponent<LineRenderer>();
        lineRight.startWidth = 0.05f;
        lineRight.endWidth = 0.05f;
        lineRight.material = new Material(Shader.Find("Sprites/Default"));
        lineRight.startColor = Color.blue;
        lineRight.endColor = Color.blue;
        lineRight.positionCount = 0;
    }

    void Update()
    {
        Jump();
        Grapple();
    }
    private void FixedUpdate()
    {
       if(!isGrappling)
        Move();
        else
            GrppleMove();  
    }
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = new Vector3(h, 0, v).normalized;

        rb.MovePosition(transform.position + moveDir * Speed * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            rb.AddForce(Vector3.up * jump, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Grounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Grounded=false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Speed++"))
        {
            Speed += 1f;
            Destroy(other.gameObject);
        }
    }
    void Grapple()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray,out RaycastHit hit, 100f))
            {
                grapplePoint= hit.point;
                isGrappling= true;


                line.positionCount = 2;
                line.SetPosition(0, transform.position);
                line.SetPosition(1, grapplePoint);
            }
        }
    }
    void GrppleMove()
    {
        Vector3 dir = (grapplePoint - transform.position).normalized;
        rb.MovePosition(transform.position + dir * grappleSpeed * Time.fixedDeltaTime);


        line.SetPosition(0, transform.position);

        if(Vector3.Distance(transform.position, grapplePoint) < 1f)
        {
            isGrappling = false;
            line.positionCount = 0;
        }
    }
    
}

