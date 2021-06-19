using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] Animator _playeranimator;
    [SerializeField] float jump;
    private Rigidbody2D rigidbody;
    [SerializeField] bool isgrounded;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            _playeranimator.SetBool("Idle", false);
        }

        if (Input.GetKey(KeyCode.D)) {
            _playeranimator.SetBool("Move", true);
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            _playeranimator.SetBool("Move", true);
            transform.position += Vector3.right * -moveSpeed * Time.deltaTime;
            spriteRenderer.flipX = true;
        }
        else
        {
            _playeranimator.SetBool("Move", false);
        }
        if (Input.GetKeyDown(KeyCode.Space)  || Input.GetKeyDown(KeyCode.W))
        {
            if (isgrounded) {
                _playeranimator.SetBool("Jump", true);
                rigidbody.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
                isgrounded = false;

            }
               
            

        }
      
        


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isgrounded = true;
            _playeranimator.SetBool("Jump", false);
        }

    }
 
}
