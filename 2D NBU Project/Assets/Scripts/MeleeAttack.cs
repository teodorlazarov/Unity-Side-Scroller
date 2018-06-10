using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {


    public static int damageToInflict;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(WaitBetweenGettingHit(collision));
            Debug.Log(damageToInflict);
          
        }
    }

 
    private IEnumerator WaitBetweenGettingHit(Collider2D collision)
    {
        collision.GetComponent<Animator>().SetTrigger("WasHit");          
        collision.GetComponent<EnemyHealthManager>().GiveDamage(damageToInflict);
        yield return new WaitForSeconds(1f);
    }
}
