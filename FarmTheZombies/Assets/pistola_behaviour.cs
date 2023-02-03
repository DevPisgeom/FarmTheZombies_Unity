using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class pistola_behaviour : NetworkBehaviour
{
    public Transform tr;
    public Camera mainCamera;
    Vector2 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner)
        {
            return;
        }
        tr = this.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
    private void FixedUpdate()
    {
        Vector2 playerPos = tr.position;
        Vector2 Direction = mousePos - playerPos;
    }
}
