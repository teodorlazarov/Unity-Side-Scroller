using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenController : MonoBehaviour {

    public float speed;
    public PlayerController player;
    public float rotationSpeed;
    public int damageToInflict;
    public GameObject impactParticle;
    public GameObject hurtParticle;


    private Rigidbody2D rb;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();

        if(player.transform.localScale.x < 0)
        {
            speed = -speed;
            rotationSpeed = -rotationSpeed;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Instantiate(impactParticle, gameObject.transform.position, gameObject.transform.rotation);
            collision.GetComponent<Animator>().SetTrigger("WasHit");
            Instantiate(hurtParticle, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            collision.GetComponent<EnemyHealthManager>().GiveDamage(damageToInflict);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Ground")
        {
            Instantiate(impactParticle, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
        rb.velocity = new Vector2(speed, rb.velocity.y);
        rb.angularVelocity = rotationSpeed;
	}
}
