using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rigidbody;
    BoxCollider2D sideCollider;
    [SerializeField] float acceleration = 1.0f;
    
    void Start(){
        rigidbody = GetComponent<Rigidbody2D>();
        sideCollider = GetComponent<BoxCollider2D>();
    }

    void Update(){
        rigidbody.velocity = new Vector2(acceleration, 0f);
    }

    void OnTriggerExit2D(Collider2D other){
        acceleration = -acceleration;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing(){
        transform.localScale = new Vector2 (-(Mathf.Sign(rigidbody.velocity.x)), 1f);
    }
}
