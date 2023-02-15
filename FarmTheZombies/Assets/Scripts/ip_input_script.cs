using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ip_input_script : MonoBehaviour
{
    public string ipInput;
    
    //public unityTransport unityTransportInport; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    public void GetString(string str)
    {
        ipInput = str;
        Debug.Log(str);
        Debug.Log(ipInput);
        NetworkManager.Singleton.GetComponent<Unity.Netcode.Transports.UTP.UnityTransport>().SetConnectionData(
        ipInput,  // The IP address is a string
        (ushort)7777 // The port number is an unsigned short
        );
        //ConnectionData.Address = ipInput;
        //Unity.Netcode.Transports.UTP.UnityTransport.SetConnectionData(str,7777);
    }
    
}
