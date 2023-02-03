using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log(ipInput);
        //ConnectionData.Address = ipInput;
        //Unity.Netcode.Transports.UTP.UnityTransport.SetConnectionData(str,7777);
    }
}
