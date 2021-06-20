using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject[] points;
    float moveSpeed = 3f;
    bool edge = false;
   [SerializeField] int health = 2;
    Animator animator;
    bool isdead = false;
    [SerializeField] GameObject _coinprefab;

    SpriteRenderer spriteRenderer;
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
            animator.SetBool("Idle", true);
        }
        else
        {
            if (isdead == false)
            {
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
        if (health == 0) {
            animator.SetBool("Die",true);

            isdead = true;
            Invoke("Disappear",1f);
            health--;
        }
       
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "point")
        {
            edge = !edge;
            Debug.Log(edge);
        }
        Debug.Log("trigger");

        if (collision.gameObject.tag == "Damage")
        {
            health--;
            animator.SetBool("Hit", true);
        }
        else {
            animator.SetBool("Hit", false);
        }
    }
    private void Disappear() {
        gameObject.SetActive(false);
        Instantiate(_coinprefab, transform.position, Quaternion.identity);
 

    }
}
