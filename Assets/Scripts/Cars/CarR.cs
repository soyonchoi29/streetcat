using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CarR : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    public float moveSpeed;
    public float roundingDistance;
    public Transform[] carRoute;
    public int currentPoint;
    public Transform currentGoal;
    private Animator animator;
    private Vector3 change;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentPoint = 1;
        currentGoal = carRoute[1];
        moveSpeed = 15;
        change = Vector3.zero;
    }

    void FixedUpdate()
    {
        change = Vector3.zero;
        MoveCar();
        UpdateAnimation();
    }

    private void MoveCar()
    {
        if (Vector3.Distance(transform.position, carRoute[4].position) <= roundingDistance)
        {
            Destroy(this.gameObject);
        }
        else if (Vector3.Distance(transform.position, currentGoal.position) > roundingDistance)
        {
            Vector3 carMovement = Vector3.MoveTowards(transform.position, currentGoal.position, moveSpeed * Time.deltaTime);
            myRigidBody.MovePosition(carMovement);

            change = currentGoal.position - transform.position;
        }
        else
        {
            ChangeGoal();
        }
    }

    private void ChangeGoal()
    {
        if (currentPoint == carRoute.Length - 1)
        {
            Destroy(this.gameObject);
        }
        else if (currentPoint == carRoute.Length - 3)
        {
            currentPoint += Random.Range(1, 3);
            currentGoal = carRoute[currentPoint];
        }
        else
        {
                currentPoint++;
                currentGoal = carRoute[currentPoint];
        }
    }

    private void UpdateAnimation()
    {
        animator.SetFloat("moveX", change.x);
        animator.SetFloat("moveY", change.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {

        }
    }
}
