using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Damage Effect")]
    public float invulnerabilityTime = 4f; // Invulnerability time in seconds
    public float pushForce = 5f; // Force to push cats away
    public AudioClip hitSound; // Sound effect for hit
    public Color damageColor = Color.red; // Color when damaged

    private bool isInvulnerable = false; // Flag for invulnerability
    private SpriteRenderer spriteRenderer; // Reference to the sprite renderer
    
    [Header("Movement")]
    [SerializeField] private float speed = 3f;
    private Rigidbody2D _rigidbody2D;
    private Vector3 movementVector;

    [Header("Animation")]
    private Animator animator;

    [Header("Health UI")]
    public Image Corazon;
    public int CantDeCorazon;
    public RectTransform PosicionPrimerCorazon;
    public Canvas MyCanvas;
    public int OffSet;
    
    private List<Image> hearts = new List<Image>();
    
    //[Header("UI")]
    //public GameObject panelPausa;
    
    public delegate void GameOverEventHandler();
    public event GameOverEventHandler OnGameOver;
    private bool estaVivo = true;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the sprite renderer
    }

    private void Start()
    {
        InitializeHealthUI();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision);
    }

    private void HandleMovement()
    {
        if (estaVivo)
        {
            movementVector.x = Input.GetAxis("Horizontal");
            movementVector.y = Input.GetAxis("Vertical");

            movementVector *= speed;


            _rigidbody2D.velocity = movementVector;
        }
        else
        {
            _rigidbody2D.velocity = Vector2.zero;
        } 
        //_rigidbody2D.velocity = movementVector;

        animator.SetFloat("Horizontal", Mathf.Abs(movementVector.x));
    }
    
    private void InitializeHealthUI()
    {
        //panelPausa.SetActive(false);
        Transform PosCorazon = PosicionPrimerCorazon;

        for (int i = 0; i < CantDeCorazon; i++)
        {
            Image NewCorazon = Instantiate(Corazon, PosCorazon.position, Quaternion.identity);
            NewCorazon.transform.SetParent(MyCanvas.transform);
            hearts.Add(NewCorazon); // Add to the list
            PosCorazon.position = new Vector2(PosCorazon.position.x + OffSet, PosCorazon.position.y);
        }
    }

    private void HandleCollision(Collision2D collision)
    {
        if (collision.gameObject.tag == "gatos" && estaVivo && !isInvulnerable)
        {
            Destroy(hearts[CantDeCorazon - 1].gameObject); // Destroy from the list
            hearts.RemoveAt(CantDeCorazon - 1); // Remove from the list
            CantDeCorazon -= 1;
            
            StartCoroutine(DamageEffect());

            if (CantDeCorazon == 0)
            {
                estaVivo = false;
                //Die();
            }
        }
    }
    
    private IEnumerator DamageEffect()
    {
        // Set invulnerable
        isInvulnerable = true;

        // Change color to red
        spriteRenderer.color = damageColor;

        // Play hit sound
        AudioSource.PlayClipAtPoint(hitSound, transform.position);

        // Push cats away
        Collider2D[] cats = Physics2D.OverlapCircleAll(transform.position, pushForce);
        foreach (Collider2D cat in cats)
        {
            if (cat.CompareTag("gatos"))
            {
                Vector2 pushDirection = (cat.transform.position - transform.position).normalized;
                cat.GetComponent<Rigidbody2D>().AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }
        }

        // Wait for invulnerability time
        yield return new WaitForSeconds(invulnerabilityTime);

        // Revert color
        spriteRenderer.color = Color.white;

        // Reset invulnerable
        isInvulnerable = false;
    }
    
    // public void Die()
    // {
    //     panelPausa.SetActive(true);
    //     if (OnGameOver != null)
    //     {
    //         OnGameOver(); 
    //     }
    // }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene("GameScene");
    }
}