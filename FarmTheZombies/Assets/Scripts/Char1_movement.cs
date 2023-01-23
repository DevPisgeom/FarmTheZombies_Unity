using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Char1_movement : NetworkBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform Zombo1;
    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    //private NetworkVariable<int> day = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    Rigidbody2D rb;
    Transform tr;
    float horizontal;
    float vertical;

    public float runSpeed = 10.0f;
    private Vector2 moveDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner) return;
        tr = GetComponent<Transform>();
        tr.position = new Vector3(400, 400);
        rb = GetComponent<Rigidbody2D>();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(OwnerClientId + "; randomNumber: " + randomNumber.Value);
        if (!IsOwner) return;
        randomNumber.Value= Random.Range(0,100);
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed " + OwnerClientId);
            TestServerRpc();

        }
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        moveDirection = new Vector2(horizontal, vertical).normalized;
        rb.velocity = new Vector2(moveDirection.x * runSpeed, moveDirection.y * runSpeed);   
    }
    [ServerRpc]
    public void TestServerRpc()
    {
        Debug.Log(NetworkManager.Singleton.IsServer);
        Transform spawnedObjectTransform = Instantiate(Zombo1);
        spawnedObjectTransform.GetComponent<NetworkObject>().Spawn(true);
    }
}
