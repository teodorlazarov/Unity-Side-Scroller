using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

    public int damageToGive;
    public GameObject hurtParticle;

    private Animator animator;
	// Use this for initialization
	void Start () {
        animator = FindObjectOfType<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetTrigger("PlayerHurt");
            Instantiate(hurtParticle, other.gameObject.transform.position, other.gameObject.transform.rotation);
            HealthManager.HurtPlayer(damageToGive);

            var player = other.GetComponent<PlayerController>();
            player.knockbackCount = player.knockbacklength;
            if(other.transform.position.x < transform.position.x)
            {
                player.knockFromRight = true;
            }
            else
            {
                player.knockFromRight = false;
            }
        }  
    }
}

