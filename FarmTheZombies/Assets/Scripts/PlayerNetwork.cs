using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector2 moveDirection;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.W)){

        }
    }
    void FixedUpdate() {

    }
    void ProcessInputs(){
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY= Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX,moveY);
    }
    void Move(){
        rb.velocity = new Vector2();
    }
}
