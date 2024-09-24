using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPlayer : MonoBehaviour
{
    private Vector3 direction;
    public float gravity = -9.8f;
    public float jumpForce=5f;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        InvokeRepeating(nameof(AnimtionS), 0.15f,.15f);
    }
    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)||Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up*jumpForce;
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * jumpForce;
            }
        }
        direction.y += gravity * Time.deltaTime;
        transform.position += direction*Time.deltaTime;
    }
    private void AnimtionS() {
        spriteIndex++;
        if (spriteIndex >= sprites.Length)
        {
            spriteIndex =0;

        }
        spriteRenderer.sprite = sprites[spriteIndex];
    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obs")
        {
            FindObjectOfType<GameManager>().GameOver();
        }
        else if(collision.gameObject.tag == "Score")
        {
            FindObjectOfType<GameManager>().IncreaseScore();
        }
    }
}
