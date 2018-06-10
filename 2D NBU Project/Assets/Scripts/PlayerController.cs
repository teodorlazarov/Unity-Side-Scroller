using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float movementSpeed;
    private float moveVelocity;
    public bool facingRight = true;
    public float jumpHeight;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;

    public Transform firePoint;
    public GameObject shuriken;

    public float knockback;
    public float knockbacklength;
    public float knockbackCount;
    public bool knockFromRight;

    public float timeBetweenMelee;
    public float timeBetweenShurikens;

    public AudioSource meleeAttackSound;
    public Collider2D weaponA;
    public Collider2D weaponB;
    //private int meleeAttacksCounter = 0;

    private Rigidbody2D rb;
    private Animator animator;
    private bool doubleJumped;
    private float meleeCounter;
    private float shurikenCounter;

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void Update()
    {

        moveVelocity = movementSpeed * Input.GetAxisRaw("Horizontal");

        if (knockbackCount <= 0)
        {
            rb.velocity = new Vector2(moveVelocity, rb.velocity.y);
        }
        else
        {
            if (knockFromRight)
            {
                rb.velocity = new Vector2(-knockback, rb.velocity.y);
            }
            if (!knockFromRight)
            {
                rb.velocity = new Vector2(knockback, rb.velocity.y);
            }
            knockbackCount -= Time.deltaTime;
        }
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (rb.velocity.x > 0 && !facingRight)
        {
            FlipMichael();
        }
        else if (rb.velocity.x < 0 && facingRight)
        {
            FlipMichael();
        }

        if (grounded)
        {
            doubleJumped = false;
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }
        if (Input.GetButtonDown("Jump") && !doubleJumped && !grounded)
        {
            Jump();
            doubleJumped = true;
        }

        /*float horMove = Input.GetAxisRaw("Horizontal");
        Vector2 vect = rb.velocity;
        if (knockbackCount <= 0)
        {
            rb.velocity = new Vector2(horMove * movementSpeed, vect.y);
        }
       else
        {
            if (knockFromRight)
            {
                GetComponent<Rigidbody2D>().AddForce(transform.right * -knockback);
            }
            if(!knockFromRight)
            {
                GetComponent<Rigidbody2D>().AddForce(transform.right * knockback);   
            }
            knockbackCount -= Time.deltaTime;
        }

        if (grounded == true)
        {
            rb.velocity = new Vector2(horMove * movementSpeed, vect.y);
            doubleJumped = false;
        }
        animator.SetFloat("Speed", Mathf.Abs(horMove));

        if (horMove > 0 && !facingRight)
        {
            FlipMichael();
        }
        else if (horMove < 0 && facingRight)
        {
            FlipMichael();
        }



        if (Input.GetButtonDown("Jump") && grounded)
        {
            Jump();
        }

        if (Input.GetButtonDown("Jump") && !doubleJumped && !grounded)
        {
            Jump();
            doubleJumped = true;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }*/

        shurikenCounter -= Time.deltaTime;
        if (Input.GetButtonDown("Fire2") && shurikenCounter < 0)
        {
            Instantiate(shuriken, firePoint.position, firePoint.rotation);
            animator.SetTrigger("ShurikenThrow");
            shurikenCounter = timeBetweenShurikens;
        }

        meleeCounter -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && meleeCounter < 0)
        {
            

                EnableWeapons();
                animator.SetTrigger("AttackLeftHand");
                MeleeAttack.damageToInflict = 30;
                //MeleeAttack.maxAttacks = 1;
                StartCoroutine(DisableWeaponCollider(0.25f));

           /* else
            {
                EnableWeapons();
                animator.SetTrigger("AttackBothHands");

                MeleeAttack.damageToInflict = 30;
                MeleeAttack.maxAttacks = 1;

                meleeAttacksCounter = 0;
                StartCoroutine(DisableWeaponCollider(0.4f));

            }*/
            meleeAttackSound.Play();
            meleeCounter = timeBetweenMelee;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FlipMichael()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

    private void EnableWeapons()
    {
        weaponA.enabled = true;
        weaponB.enabled = true;
    }

    private IEnumerator DisableWeaponCollider(float time)
    {

        yield return new WaitForSeconds(time);
        weaponA.enabled = false;
        weaponB.enabled = false;
    }
}
