using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Char1_movement : NetworkBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform Zombo1;
    public GameObject pistola;
    public GameObject[] clients;
    private NetworkVariable<int> currentDay = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    private NetworkVariable<bool> isDay = new NetworkVariable<bool>(true, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    //private NetworkVariable<int> numberOfReady = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    //private NetworkList<int> LobbyReadyList;
    //private NetworkVariable<bool> isReady ;
    //private NetworkVariable<int> day = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    Rigidbody2D rb;
    Transform tr;
    float horizontal;
    float vertical;
    public Animator animator;
    public float runSpeed = 10.0f;
    private Vector2 moveDirection;
    
    //private bool isReady;

    // Start is called before the first frame update
    void Start()
    {
        //LobbyReadyList = new NetworkList<int>();

        
        if (!IsOwner) return;
        
        //GameObject myPistola = Instantiate(pistola);
        
        PistolSpawnServerRpc(OwnerClientId);

        

        tr = GetComponent<Transform>();
        tr.position = new Vector3(400, 400);
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("OWNEROWNEROWNER DICOAEADASD");
        //isReady = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        
        //LobbyReadyList.Add((int)OwnerClientId);
        //LobbyReadyList.Add(0);

    }
    public override void OnNetworkSpawn()
    {
        gameObject.tag = "Player";
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(OwnerClientId + "; randomNumber: " + randomNumber.Value);
        if (!IsOwner) return;
        //randomNumber.Value= Random.Range(0,100);
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown("space"))
        {
            //isReady = new NetworkVariable<bool>(!isReady.Value, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
            //LobbyReadyList[(int)OwnerClientId] = isReady ? 1:0;

            //cambia qualcosa a schermo per dire che questo client è pronto

            print("space key was pressed " + OwnerClientId);
            ReadyServerRpc(true);

        }
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        moveDirection = new Vector2(horizontal, vertical).normalized;
        animator.SetFloat("horizontal", moveDirection.x);
        animator.SetFloat("vertical", moveDirection.y);
        animator.SetFloat("speed", moveDirection.sqrMagnitude);
        rb.velocity = new Vector2(moveDirection.x * runSpeed, moveDirection.y * runSpeed);
        
    }
    [ServerRpc]
    public void ReadyServerRpc(bool PlayerStatus)
    {
        //Debug.Log(NetworkManager.Singleton.IsServer);
        if (isDay.Value) {
            if (PlayerStatus == true)
            {

                //numberOfReady = new NetworkVariable<int>(numberOfReady.Value + 1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
                //controlla se tutti i player sono pronti
                /*
                for(int i = 0; i < LobbyReadyList.Count; i++)
                {
                    if (LobbyReadyList[i] == 0)
                    {
                        //almeno un player trovato che non è pronto
                        Debug.Log("Waiting For All Players To get ready");
                        return;
                    }
                }
                */
                /*
                print("ININININ");
                clients = GameObject.FindGameObjectsWithTag("Player");
                print(clients.Length);
                for (int i = 0; i < clients.Length; i++){
                    Char1_movement targetObjectScript = clients[i].GetComponent<Char1_movement>();
                    print("client i:");
                    print(i);
                    print("isReady?:");
                    print(targetObjectScript.isReady.Value);
                    //print(targetObjectScript.TEST);
                    if (targetObjectScript.isReady.Value == false)
                    {
                        return;
                    }
                }
                //se arrivo qui allora comincia la notte
                */

                for (int i = 0; i < currentDay.Value; i++)
                {
                    Transform spawnedObjectTransform = Instantiate(Zombo1);

                    spawnedObjectTransform.GetComponent<NetworkObject>().Spawn(true);
                }
                currentDay = new NetworkVariable<int>(currentDay.Value + 1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
            }
            

        }



    }
    [ServerRpc]
    public void PistolSpawnServerRpc(ulong clientid)
    {
        GameObject myPistola = Instantiate(pistola);
        myPistola.GetComponent<NetworkObject>().SpawnWithOwnership(clientid);
        myPistola.GetComponent<Transform>().parent = this.GetComponent<Transform>();

    }
    
    
       
}
    /*
    [ClientRpc]

    public void GetPlayerStatusClientRpc()
    {

        
    }
    */
