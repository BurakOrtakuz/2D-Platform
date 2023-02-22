using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    [SerializeField] float speed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbingSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f,10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField]public int coins = 0;
    float gravityScaleAtStart;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D   myFeetCollider;
    bool isAlive = true;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive) {return;}
        Run();
        FlipSprite();
        Climb();
        Die();
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    void OnJump(InputValue value)
    {
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if(value.isPressed)
        {
            myRigidBody.velocity += new Vector2(0f,jumpSpeed);
        }
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * speed,myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x)> Mathf.Epsilon;
        myAnimator.SetBool("isRunning",playerHasHorizontalSpeed);
    }
    void OnFire(InputValue value)
    {
        if(!isAlive){return;}
        Instantiate(bullet,gun.position,transform.rotation);
    }
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x)> Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x),1f);
        }
    }
    void Climb()
    {
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x,moveInput.y * climbingSpeed);
        if(!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))){
            myRigidBody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing",false);
            return;
        }
        myAnimator.SetBool("isClimbing",Mathf.Abs(climbVelocity.y)> Mathf.Epsilon);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0f;
    }
    void Die()
    {
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidBody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessOfDeath();
        }
    }
}
