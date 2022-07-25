using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 20f;
    Rigidbody2D rigidbody;
    PlayerMovement player;
    float xSpeed = 0f;

    void Start(){
        rigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }
    void Update(){
        rigidbody.velocity = new Vector2(xSpeed,0f);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy"){
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject);
    }

}
