using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
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
    public float bulletForce = 0.1f;
    public float fireRate = 0.4f;
    public float reloadSpeed = 0.8f;
    public int magCapacity = 9;
    private int magBulletsLeft = 9;
    private float TimeShot;
    public Animator anim;
    public TextMeshProUGUI magText;
    public float reloadTimeAccumulator=0f;

    public Transform PlayerOwnerTr;
    //public ulong clientId;
    
    // Start is called before the first frame update
    void Start()
    {
        magText.text = magBulletsLeft.ToString()+" / "+ magCapacity.ToString();
        PlayerOwnerTr = transform.parent;
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
        if (Input.GetButtonDown("Fire1") && Time.time>=TimeShot+fireRate+reloadTimeAccumulator)
        {
            if (magBulletsLeft == 0)
            {
                //play animazione ricarica 
                anim.SetTrigger("reloadTrigger");
                //Reload
                magBulletsLeft = magCapacity;
                
                reloadTimeAccumulator = reloadSpeed;
                magText.text = magBulletsLeft.ToString() + " / " + magCapacity.ToString();
            }
            else {
                magText.text = magBulletsLeft.ToString() + " / " + magCapacity.ToString();
                ShootServerRpc();
                magBulletsLeft = magBulletsLeft - 1;
                magText.text = magBulletsLeft.ToString() + " / " + magCapacity.ToString();
                TimeShot = Time.time;
                reloadTimeAccumulator = 0f;
            }
        }

        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }
    
    private void FixedUpdate()
    {
        if (!IsOwner)
        {
            return;
        }
        Debug.Log("dioporcoelamadonna");
        RotatePistolServerRpc(OwnerClientId,mousePos);
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
    [ServerRpc]
    public void RotatePistolServerRpc(ulong clientID,Vector2 mousePos)
    {
        rb.position = new Vector3(PlayerOwnerTr.position.x - 5, PlayerOwnerTr.position.y + 10, 0);
        Vector2 localPos = rb.position;
        Vector2 Direction = mousePos - localPos;
        if (clientID == 1)
        {
            Debug.Log("Direction:");
            Debug.Log(Direction);
            Debug.Log(mousePos);
            Debug.Log(localPos);
        }
        
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }
}
