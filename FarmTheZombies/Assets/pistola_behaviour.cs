using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class pistola_behaviour : NetworkBehaviour
{
    //public Transform tr;
    public Rigidbody2D rb;
    public Transform tr;
    public Camera mainCamera;
    Vector2 mousePos;
    public GameObject bulletPrefab;
    public Transform firepoint;
    public float rotate_speed;
    public float bulletForce = 200f;
    public Transform PlayerOwnerTr;
    //public ulong clientId;
    
    // Start is called before the first frame update
    void Start()
    {

        if (!IsOwner)
        {
            return;
        }
        Debug.Log("ahahahhahahahahahahahahahahhahahhahahaha");
        Debug.Log(OwnerClientId);
        /*
        if (!IsOwner)
        {
            return;
        }
        */
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        PlayerOwnerTr= transform.parent;
        //PlayerOwnerTr = gameObject.GetComponentInParent(System.Type Transform);

        //mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            ShootServerRpc();
        }
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
    
    private void FixedUpdate()
    {
        if (!IsOwner)
        {
            return;
        }
        rb.position = new Vector3(PlayerOwnerTr.position.x - 5, PlayerOwnerTr.position.y + 10, 0);
        Vector2 localPos = rb.position;
        Vector2 Direction = mousePos - localPos;
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        //tr.Rotate (0,0,angle,Space.Self);

        /*if((angle <= -90 && angle >= -180) || (angle <= 180 && angle >= 90))
        {
            if (angle < 0)
            {
                Quaternion q = Quaternion.Euler(new Vector3(0, 180, angle));
                tr.localRotation = Quaternion.Slerp(tr.localRotation, q, rotate_speed);
            }
            else
            {
                Quaternion q = Quaternion.Euler(new Vector3(0, 180, angle));
                tr.localRotation = Quaternion.Slerp(tr.localRotation, q, rotate_speed);
            }
            
        }
        else
        {
            Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
            tr.localRotation = Quaternion.Slerp(tr.localRotation, q, rotate_speed);
        }

        if (angle < -90)
        {
            Quaternion q = Quaternion.Euler(new Vector3(0, 180, angle));
            tr.localRotation = Quaternion.Slerp(tr.localRotation, q, rotate_speed);
        }
        else
        {
            if(angle < 90)
            {
                Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
                tr.localRotation = Quaternion.Slerp(tr.localRotation, q, rotate_speed);
            }
        }*/
        rb.rotation = angle;
        //Debug.Log(angle);


    }
    [ServerRpc]
    public void ShootServerRpc()
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, tr.rotation);
        bullet.GetComponent<NetworkObject>().Spawn();
        Destroy(bullet, 10f);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        //Debug.Log(tr.up);
        bulletRb.AddForce(tr.right * bulletForce, ForceMode2D.Impulse);
    }
}
