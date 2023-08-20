using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector3 movementVector;

    [Header("Animation")]
    private Animator animator;

    [SerializeField] private float speed = 3f;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    public delegate void GameOverEventHandler();
    public event GameOverEventHandler OnGameOver;

    public Image Corazon;
    public int CantDeCorazon;
    public RectTransform PosicionPrimerCorazon;
    public Canvas MyCanvas;
    public int OffSet;


    private bool estaVivo = true;

    public GameObject panelPausa;

    // Start is called before the first frame update
    void Start()
    {
        panelPausa.SetActive(false);
        Transform PosCorazon = PosicionPrimerCorazon;

        Debug.Log("Cantidad de vidas: " + CantDeCorazon);

        for (int i = 0; i < CantDeCorazon; i++)
        {
            Image NewCorazon = Instantiate(Corazon, PosCorazon.position, Quaternion.identity);
            NewCorazon.transform.parent = MyCanvas.transform;
            PosCorazon.position = new Vector2(PosCorazon.position.x + OffSet, PosCorazon.position.y);
        }
        
    }

    // Update is called once per frame
    void Update()
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

    private void  OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "gatos" && estaVivo)
        {
            Destroy(MyCanvas.transform.GetChild(CantDeCorazon + 1).gameObject);
            CantDeCorazon -= 1;

            if (CantDeCorazon == 0)
            {
                estaVivo = false;
                Die();
            }

            
        }
    }

    public void Die()
    {
        panelPausa.SetActive(true);
        if (OnGameOver != null)
        {
            OnGameOver(); 
        }
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene("GameScene");
    }
}
