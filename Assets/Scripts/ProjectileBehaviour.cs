using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ProjectileBehaviour : NetworkBehaviour
{
    public float destroyAfter = 2;
    public float force = 1000;

    public float Speed = 4.5f;
    public Rigidbody2D _rigidBody;

    public override void OnStartServer()
    {
        Invoke(nameof(DestroySelf), destroyAfter);
    }

    void Start()
    {
        _rigidBody.AddForce(transform.forward * force);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.right * Time.deltaTime * Speed;
    }

    [Server]
    void DestroySelf()
    {
        NetworkServer.Destroy(gameObject);
    }

    // ServerCallback because we don't want a warning
    // if OnTriggerEnter is called on the client
    [ServerCallback]
    void OnTriggerEnter(Collider co) => DestroySelf();

    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
    */
}
