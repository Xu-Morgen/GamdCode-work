using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignPaperController : MonoBehaviour
{
    Rigidbody2D SignPaperRigidBody2d;//
    public float FarMaxDistance;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.magnitude > FarMaxDistance)
        {
            Debug.Log("is destroyed");
            Destroy(gameObject);
        }
    }

    public void PlayerSignPaper(Vector2 direction, float force)
    {
        SignPaperRigidBody2d = GetComponent<Rigidbody2D>();//获取符咒刚体
        
        if (SignPaperRigidBody2d != null){
            SignPaperRigidBody2d.AddForce(direction * force);
        }
        else
        {
            Debug.Log(SignPaperRigidBody2d);
        }

    }

    void OnCollisionEnter2D(Collision2D other){
        Destroy(gameObject);
    }

}
