using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class zombo_behaviour : NetworkBehaviour
{
    Rigidbody2D rb;
    Transform tr;
    public GameObject[] target;
    public Transform target_tr;
    public float movement_speed = 100f;
    public float rotate_speed = 0.02f;
    private int randx;
    private int randy;
    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        
    }
    void Start()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            //Debug.Log(NetworkBehaviour.IsOwnedByServer);
            tr = GetComponent<Transform>();
            print("ZOMBOO");

            if (Random.Range(0, 2) == 0)
            {
                Debug.Log("first");
                randx = Random.Range(-500, -700);
                if (Random.Range(0, 2) == 0)
                {
                    Debug.Log("Second");
                    //top left
                    randy = Random.Range(1000, 1200);
                    tr.position = new Vector3(randx, randy, 0);
                }
                else
                {
                    //bottom left
                    randy = Random.Range(-400, -600);
                    tr.position = new Vector3(randx, randy, 0);
                }

                //tr.position = new Vector3(Random.Range(-300,-200), Random.Range(-200, 500), 0);

            }
            else
            {
                randx = Random.Range(1600, 1800);
                if (Random.Range(0, 2) == 0)
                {
                    //top right
                    randy = Random.Range(1000, 1200);
                    tr.position = new Vector3(randx, randy, 0);
                }
                else
                {
                    //bottom right
                    randy = Random.Range(-400, -600);
                    tr.position = new Vector3(randx, randy, 0);
                }
            }

            rb = GetComponent<Rigidbody2D>();

        }
    }
    private void FixedUpdate()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
        if (!target_tr)
        {
            findTarget();
        }
        else
        {
            rotateTowardsTarget();
            //go to target
            moveTowardsTarget();
        }
    }
    public void findTarget()
    {
        //Debug.Log("cercoTarget");
        target = GameObject.FindGameObjectsWithTag("Player");
        //scegli random target
        //Debug.Log(target.Length);
        target_tr = target[Random.Range(0,(target.Length))].GetComponent<Transform>();
       
    }
    public void moveTowardsTarget()
    {
        Vector2 targetDirection = target_tr.position - tr.position;
        targetDirection = targetDirection.normalized;
        
        //float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        //Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        //Debug.Log("muovendo");
        
        //rb.velocity = tr.up * movement_speed;
        //tr.position = new Vector3(500, 500);
        tr.position =  Vector3.MoveTowards(tr.position, target_tr.position, Time.deltaTime*movement_speed);
    }
    public void rotateTowardsTarget()
    {
        
        
        if(target_tr.position.x > tr.position.x)
        {
            //target sta a destra
            Quaternion q = Quaternion.Euler(new Vector3(0, 0, 0));
            tr.localRotation = Quaternion.Slerp(transform.localRotation, q, rotate_speed);
        }
        else
        {
            //target sta a sinistra
            Quaternion q = Quaternion.Euler(new Vector3(0, 180, 0));
            tr.localRotation = Quaternion.Slerp(transform.localRotation, q, rotate_speed);
        }
        
        //Debug.Log("ZIOPERA");
        //Debug.Log(transform.localRotation);
        //Debug.Log(q);

    }
}
