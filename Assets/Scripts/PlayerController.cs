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
    public GameObject waterBombPrefab; // Reference to the water bomb prefab
    public float waterBombRange = 5f; // Range of the water bomb explosion
    public int waterBombDrops = 12; // Number of drops in the water bomb
    public float invulnerabilityTime = 4f; // Invulnerability time in seconds
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

    public delegate void GameOverEventHandler();
    public event GameOverEventHandler OnGameOver;
    private bool estaVivo = true;

    private CharacterPhrases characterPhrases; // Reference to the CharacterPhrases script

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the sprite renderer
        characterPhrases = GetComponent<CharacterPhrases>(); // Get the CharacterPhrases script
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
        animator.SetFloat("Horizontal", Mathf.Abs(movementVector.x));
    }

    private void InitializeHealthUI()
    {
        Transform PosCorazon = PosicionPrimerCorazon;
        for (int i = 0; i < CantDeCorazon; i++)
        {
            Image NewCorazon = Instantiate(Corazon, PosCorazon.position, Quaternion.identity);
            NewCorazon.transform.SetParent(MyCanvas.transform);
            hearts.Add(NewCorazon);
            PosCorazon.position = new Vector2(PosCorazon.position.x + OffSet, PosCorazon.position.y);
        }
    }

    private void HandleCollision(Collision2D collision)
    {
        if (collision.gameObject.tag == "gatos" && estaVivo && !isInvulnerable)
        {
            Destroy(hearts[CantDeCorazon - 1].gameObject);
            hearts.RemoveAt(CantDeCorazon - 1);
            CantDeCorazon -= 1;

            StartCoroutine(DamageEffect());

            if (CantDeCorazon == 0)
            {
                estaVivo = false;
            }
        }
    }

    private IEnumerator DamageEffect()
    {
        isInvulnerable = true;
        spriteRenderer.color = damageColor;

        // Pause any ongoing character phrase and play the hit sound
        characterPhrases.PauseAndPlayHitSound();

        // Duration of the water bomb effect
        float waterBombDuration = 1f; // 1 second
        float timeElapsed = 0f;

        while (timeElapsed < waterBombDuration)
        {
            // Instantiate a single water drop
            float angle = UnityEngine.Random.Range(0f, 360f);
            Vector3 direction = Quaternion.Euler(0, 0, angle) * Vector3.right;
            GameObject waterDrop = Instantiate(waterBombPrefab, transform.position, Quaternion.identity);
            WaterDrop waterDropScript = waterDrop.GetComponent<WaterDrop>();
            waterDropScript.Initialize(direction, waterBombRange);

            timeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        yield return new WaitForSeconds(invulnerabilityTime - waterBombDuration);

        spriteRenderer.color = Color.white;
        isInvulnerable = false;
    }
}

//
// [RequireComponent(typeof(Rigidbody2D))]
// public class PlayerController : MonoBehaviour
// {
//     [Header("Damage Effect")]
//     public float invulnerabilityTime = 4f; // Invulnerability time in seconds
//     public AudioClip hitSound; // Sound effect for hit
//     public Color damageColor = Color.red; // Color when damaged
//     public float hitSoundVolume = 5f; // Volume of the hit sound
//     public GameObject waterBombPrefab; // Reference to the water bomb prefab
//     public float waterBombRange = 5f; // Range of the water bomb explosion
//     public int waterBombDrops = 12; // Number of drops in the water bomb
//
//     private bool isInvulnerable = false; // Flag for invulnerability
//     private SpriteRenderer spriteRenderer; 
//     
//     public float pushRadius = 10f; // Radius to detect cats
//
//     [Header("Movement")]
//     [SerializeField] private float speed = 3f;
//     private Rigidbody2D _rigidbody2D;
//     private Vector3 movementVector;
//
//     [Header("Animation")]
//     private Animator animator;
//
//     [Header("Health UI")]
//     public Image Corazon;
//     public int CantDeCorazon;
//     public RectTransform PosicionPrimerCorazon;
//     public Canvas MyCanvas;
//     public int OffSet;
//     
//     private List<Image> hearts = new List<Image>();
//     
//     //[Header("UI")]
//     //public GameObject panelPausa;
//     
//     public delegate void GameOverEventHandler();
//     public event GameOverEventHandler OnGameOver;
//     private bool estaVivo = true;
//
//     private void Awake()
//     {
//         _rigidbody2D = GetComponent<Rigidbody2D>();
//         animator = GetComponent<Animator>();
//         spriteRenderer = GetComponent<SpriteRenderer>(); // Get the sprite renderer
//     }
//
//     private void Start()
//     {
//         InitializeHealthUI();
//     }
//
//     private void Update()
//     {
//         HandleMovement();
//     }
//
//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         HandleCollision(collision);
//     }
//
//     private void HandleMovement()
//     {
//         if (estaVivo)
//         {
//             movementVector.x = Input.GetAxis("Horizontal");
//             movementVector.y = Input.GetAxis("Vertical");
//
//             movementVector *= speed;
//
//
//             _rigidbody2D.velocity = movementVector;
//         }
//         else
//         {
//             _rigidbody2D.velocity = Vector2.zero;
//         } 
//         //_rigidbody2D.velocity = movementVector;
//
//         animator.SetFloat("Horizontal", Mathf.Abs(movementVector.x));
//     }
//     
//     private void InitializeHealthUI()
//     {
//         //panelPausa.SetActive(false);
//         Transform PosCorazon = PosicionPrimerCorazon;
//
//         for (int i = 0; i < CantDeCorazon; i++)
//         {
//             Image NewCorazon = Instantiate(Corazon, PosCorazon.position, Quaternion.identity);
//             NewCorazon.transform.SetParent(MyCanvas.transform);
//             hearts.Add(NewCorazon); // Add to the list
//             PosCorazon.position = new Vector2(PosCorazon.position.x + OffSet, PosCorazon.position.y);
//         }
//     }
//     
//     private IEnumerator DamageEffect()
//     {
//         isInvulnerable = true;
//         spriteRenderer.color = damageColor;
//
//         AudioSource.PlayClipAtPoint(hitSound, transform.position, hitSoundVolume);
//
//         // Detect cats within the specified radius
//         Collider2D[] cats = Physics2D.OverlapCircleAll(transform.position, pushRadius);
//         Debug.Log("Number of cats detected: " + cats.Length); // Log the number of detected cats
//
//         foreach (Collider2D cat in cats)
//         {
//             if (cat.CompareTag("gatos"))
//             {
//                 Vector2 pushDirection = (cat.transform.position - transform.position).normalized;
//                 Rigidbody2D catRigidbody = cat.GetComponent<Rigidbody2D>();
//
//                 // Log the mass and drag values for debugging
//                 Debug.Log("Cat mass: " + catRigidbody.mass + ", Cat drag: " + catRigidbody.drag);
//
//                 catRigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Force); // Try using Force instead of Impulse
//             }
//         }
//
//
//         yield return new WaitForSeconds(invulnerabilityTime);
//
//         spriteRenderer.color = Color.white;
//         isInvulnerable = false;
//     }
//     
//     private void HandleCollision(Collision2D collision)
//     {
//         if (collision.gameObject.tag == "gatos" && estaVivo && !isInvulnerable)
//         {
//             Destroy(hearts[CantDeCorazon - 1].gameObject); // Destroy from the list
//             hearts.RemoveAt(CantDeCorazon - 1); // Remove from the list
//             CantDeCorazon -= 1;
//
//             StartCoroutine(DamageEffect());
//
//             if (CantDeCorazon == 0)
//             {
//                 estaVivo = false;
//                 //Die();
//             }
//         }
//     }
//
//     // private void HandleCollision(Collision2D collision)
//     // {
//     //     if (collision.gameObject.tag == "gatos" && estaVivo && !isInvulnerable)
//     //     {
//     //         Destroy(hearts[CantDeCorazon - 1].gameObject); // Destroy from the list
//     //         hearts.RemoveAt(CantDeCorazon - 1); // Remove from the list
//     //         CantDeCorazon -= 1;
//     //         
//     //         StartCoroutine(DamageEffect());
//     //
//     //         if (CantDeCorazon == 0)
//     //         {
//     //             estaVivo = false;
//     //             //Die();
//     //         }
//     //     }
//     // }
//     
//     
//     // public void Die()
//     // {
//     //     panelPausa.SetActive(true);
//     //     if (OnGameOver != null)
//     //     {
//     //         OnGameOver(); 
//     //     }
//     // }
//
//     public void ReiniciarNivel()
//     {
//         SceneManager.LoadScene("GameScene");
//     }
// }