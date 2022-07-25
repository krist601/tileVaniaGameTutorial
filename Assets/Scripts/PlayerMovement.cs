using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    Vector2 moveInput;
    Rigidbody2D rigidbody;
    [SerializeField] float acceleration = 1.0f;
    [SerializeField] float jumpSpeed = 5.0f;
    [SerializeField] float climbSpeed = 5.0f;
    [SerializeField] Vector2 deathKick = new Vector2(10f,10f);
    CapsuleCollider2D capsuleCollider;
    BoxCollider2D feetCollider;
    float gravityAtStart;
    bool isAlive = true;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    Animator myAnimator;

    void Start(){
        rigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityAtStart = rigidbody.gravityScale;
    }

    void Update(){
        if(!isAlive){ return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }
    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }
    void OnJump(InputValue value){
        if(!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        if(value.isPressed){
            rigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }
    void ClimbLadder(){
        if(!capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladders"))) { 
            rigidbody.gravityScale = gravityAtStart;
            return; 
        }
        rigidbody.gravityScale = 0f;
        Vector2 climbVelocity = new Vector2 (rigidbody.velocity.x, moveInput.y * climbSpeed);
        rigidbody.velocity = climbVelocity ;
        myAnimator.SetBool("isClimbing", Mathf.Abs(rigidbody.velocity.y) > Mathf.Epsilon);
    }
    void Run(){
        Vector2 playerVelocity = new Vector2 (moveInput.x * acceleration, rigidbody.velocity.y);
        rigidbody.velocity = playerVelocity ;
        myAnimator.SetBool("isRunning", Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon);
    }
    void FlipSprite(){
        if(Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon){
            transform.localScale = new Vector2 (Mathf.Sign(rigidbody.velocity.x), 1f);
        }
    }
    void Die(){
        if(capsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazards","Water"))){
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            rigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
    void OnFire(InputValue value){
        if(!isAlive){ return; }
        Instantiate(bullet, gun.position, transform.rotation);
    }
}
