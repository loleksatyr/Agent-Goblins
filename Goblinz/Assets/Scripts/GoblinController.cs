using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : MonoBehaviour
{
    [SerializeField] GameObject[] points;
    float moveSpeed = 3f;
    bool edge = false;
    [SerializeField] int health = 2;
    Animator animator;
    bool isdead = false;
    [SerializeField] GameObject _coinprefab;

    SpriteRenderer spriteRenderer;
    [SerializeField] bool _playernear = false;
    [SerializeField] GameObject leftTrigger;
    [SerializeField] GameObject rightTrigger;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (points.Length == 0 || points.Length == 1)
        {
            animator.SetBool("PlayerRange", false);
        }
        else
        {
            animator.SetBool("PlayerRange", true);
            if (isdead == false)
            {
                if (rightTrigger.GetComponent<GoblinTrigger>().isOn) {
                    transform.position += Vector3.right * moveSpeed/2 * Time.deltaTime;
                    spriteRenderer.flipX = false;
                    animator.SetBool("PlayerNear", true);
                    animator.SetBool("PlayerRange", false);
                }
                else if (leftTrigger.GetComponent<GoblinTrigger>().isOn)
                    {
                    spriteRenderer.flipX = true;
                    transform.position -= Vector3.right * moveSpeed/2 * Time.deltaTime;
                    animator.SetBool("PlayerNear", true);
                    animator.SetBool("PlayerRange", false);
                     }
                else
                {
                    animator.SetBool("PlayerRange", true);
                    animator.SetBool("PlayerNear", false);
                    if (edge)
                    {
                        transform.position -= Vector3.right * moveSpeed * Time.deltaTime;
                        spriteRenderer.flipX = true;
                    }
                    else
                    {
                        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                        spriteRenderer.flipX = false;
                    }
                }
            }
        }
        if (health == 0)
        {
            animator.SetBool("Die", true);
            isdead = true;
            Invoke("Disappear", 1f);
            health--;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "point")
        {
            edge = !edge;
        }


        if (collision.gameObject.tag == "Damage")
        {
            health--;

        }
    }
   
    private void Disappear()
    {
        gameObject.SetActive(false);
        Instantiate(_coinprefab, transform.position, Quaternion.identity);


    }
}

