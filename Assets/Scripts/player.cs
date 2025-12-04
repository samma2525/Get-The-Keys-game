using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float health =100f;
    public Animator anim;
    public keyManager km;
    public string sceneName;
    public string nextSceneName;
    public Image healthBar;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float x;
    private float y;
    private bool moving;
    private Vector2 input;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        GetInput();
        Animate();
        healthBar.fillAmount = health / 100f;
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
        
    }
    private void GetInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        input = new Vector2(x,y);
        input.Normalize();
    }
    private void Animate()
    {
        if(input.magnitude > 0.1f || input.magnitude < -0.1f)
        {
            moving = true;
        }
        else{
            moving = false;
        }
        if(moving)
        {
            anim.SetFloat("X",x);
            anim.SetFloat("Y", y);
        }
        anim.SetBool("Moving", moving);
}
void OnTriggerEnter2D(Collider2D other)
{
    if(other.gameObject.CompareTag("key"))
    {
        Destroy(other.gameObject);
        km.keyCount += 1;
    }
    if (other.gameObject.CompareTag("checkpoint"))
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }
}
void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Damage"))
        {
            health -=25f;
            StartCoroutine(BlinkRed());
            
        }
        if (health <= 0)
        {
            Die();
        }
    }
private IEnumerator BlinkRed()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }
private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    
}}