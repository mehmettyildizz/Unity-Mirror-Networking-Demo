using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Character2DController : NetworkBehaviour
{
    private Rigidbody2D _rigidbody;

    public float MovementSpeed = 5f;
    public float JumpForce = 3f;

    public KeyCode holaKey = KeyCode.H;

    [Header("Firing")]
    public KeyCode shootKey = KeyCode.X;
    public GameObject ProjectilePrefab;
    public Transform LaunchOffset;
    

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

    }


    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            var movement = Input.GetAxis("Horizontal");
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

            //Flip character
            if (!Mathf.Approximately(0, movement))
            {
                transform.rotation = movement > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            }

            if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
            {
                _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            }

            //shoot
            /*
            if (Input.GetKeyDown(shootKey))
            {
                Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
            }
            */
            if (Input.GetKeyDown(shootKey))
            {
                CmdFire();
            }

            if (Input.GetKeyDown(holaKey))
            {
                Hola();
            }


        }
    }

    // this is called on the server
    [Command]
    void CmdFire()
    {
        GameObject projectile = Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
        NetworkServer.Spawn(projectile);
        RpcOnFire();
    }

    // this is called on the server
    [Command]
    void Hola()
    {
        Debug.Log("Received hola from client!");
        ReplyHola();
    }

    [TargetRpc]
    void ReplyHola()
    {
        Debug.Log("Received hola from server!");
    }

    [ClientRpc]
    void RpcOnFire()
    {
        //transform.position += -transform.right * Time.deltaTime * ProjectileSpeed;
    }
}
