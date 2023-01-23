using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ServerBrain : NetworkBehaviour
{

    public NetworkObject Zombo;
    int day = 1;
    // Start is called before the first frame update
    void DayStart(){
        for(int i = 0; i < day; i++)
        {
            Zombo.Spawn();
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
