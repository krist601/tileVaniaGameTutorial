using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rigidbody;
    void Start(){
        rigidbody = GetComponent<Rigidbody2D>();
        
    }
    void Update(){
        rigidbody.velocity = new Vector2(1f,0f);
    }
}
