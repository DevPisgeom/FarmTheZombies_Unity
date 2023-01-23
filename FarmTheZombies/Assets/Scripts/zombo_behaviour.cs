using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class zombo_behaviour : NetworkBehaviour
{
    Rigidbody2D rb;
    Transform tr;
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

    // Update is called once per frame
    void Update()
    {
       
    }
}
