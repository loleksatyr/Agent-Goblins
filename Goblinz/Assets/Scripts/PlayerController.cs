using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] Animator _playeranimator;
    [SerializeField] float jump;
    private Rigidbody2D rigidbody;
    [SerializeField] bool isgrounded;
    [SerializeField] int coins = 0;
    [SerializeField] Text _coinsText;
    [SerializeField] bool _enemynear;
    [SerializeField] bool damage;
    [SerializeField] int Health;
    [SerializeField] GameObject attackleft;
    [SerializeField] GameObject attackright;
    [SerializeField] GameObject[] Healthtab;
    private bool isdead = false;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        attackleft.SetActive(false);
        attackright.SetActive(false);
        Health = Healthtab.Length-1;
        Debug.Log(Health);
    }
    private void Update()
    {
        if (isdead == false)
        {
            if (Input.anyKeyDown)
            {
                _playeranimator.SetBool("Idle", false);
            }

            if (Input.GetKey(KeyCode.D))
            {
                _playeranimator.SetBool("Move", true);
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                spriteRenderer.flipX = false;
                _playeranimator.SetBool("Attack", false);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                _playeranimator.SetBool("Move", true);
                transform.position += Vector3.right * -moveSpeed * Time.deltaTime;
                spriteRenderer.flipX = true;
                _playeranimator.SetBool("Attack", false);
            }
            else
            {
                _playeranimator.SetBool("Move", false);
                _playeranimator.SetBool("Attack", false);
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
            {
                if (isgrounded)
                {
                    _playeranimator.SetBool("Jump", true);
                    rigidbody.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
                    isgrounded = false;

                }



            }
            if (Input.GetKey(KeyCode.P))
            {
                _playeranimator.SetBool("Attack", true);
                if (spriteRenderer.flipX == true)
                {
                    attackleft.SetActive(true);
                    attackright.SetActive(false);
                }
                else
                {
                    attackright.SetActive(true);
                    attackleft.SetActive(false);
                }
            }
            else
            {
                attackleft.SetActive(false);
                attackright.SetActive(false);
            }
        }
            _coinsText.text = coins.ToString();
        if (Health < 0) {
            Die();
        }
      
        


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isgrounded = true;
            _playeranimator.SetBool("Jump", false);
        }
        if (collision.gameObject.tag == "Coin")
        {
            Destroy(collision.gameObject);
            coins++;

        }
        if (collision.gameObject.tag == "Enemy") {
            if (spriteRenderer.flipX == true)
            {
                rigidbody.AddForce(Vector2.right * jump, ForceMode2D.Impulse);
            }
            else if (spriteRenderer.flipX == false) {
                rigidbody.AddForce(Vector2.left * jump, ForceMode2D.Impulse);
            }
            Healthtab[Health].GetComponent<Animator>().SetBool("Healtdown",true);
            Health--;

        }
    }
    private void Die() {
        isdead = true;
        _playeranimator.SetBool("Dead", true);
    }
 
}
