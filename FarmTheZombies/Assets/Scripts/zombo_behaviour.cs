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
    public float speed = 3f;
    public float rotate_speed = 0.01f;
    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            tr = GetComponent<Transform>();
            print("ZOMBOO");
            tr.position = new Vector3(400, 400);
            rb = GetComponent<Rigidbody2D>();
        }
    }
    void Start()
    {
        
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
        }
    }
    public void findTarget()
    {
        target = GameObject.FindGameObjectsWithTag("Player");
    }
    public void rotateTowardsTarget()
    {
        Vector2 targetDirection = target_tr.position - tr.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        tr.localRotation = Quaternion.Slerp(transform.localRotation, q, rotate_speed);
        Debug.Log("ZIOPERA");
        Debug.Log(transform.localRotation);
        Debug.Log(q);

    }
}
