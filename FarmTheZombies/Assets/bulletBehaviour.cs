using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;

public class bulletBehaviour : NetworkBehaviour
{
    public GameObject hitEffect;
    // Start is called before the first frame update
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsServer)
        {
            return;
        }
        
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        effect.GetComponent<NetworkObject>().Spawn();
        Destroy(effect, 0.9f);
        Destroy(gameObject);
    }
}
