using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUIMio : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button ServerBtn;
    [SerializeField] private Button HostBtn;
    [SerializeField] private Button ClientBtn;
    [SerializeField] private Button DayBtn;

    private void Awake(){
        ServerBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartServer();
        });
        ClientBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
        });
        HostBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
        });
        DayBtn.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
        });
    }

}
