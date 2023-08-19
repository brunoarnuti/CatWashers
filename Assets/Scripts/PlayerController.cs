using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector3 movementVector;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        
    }


    public Image Corazon;
    public int CantDeCorazon;
    public RectTransform PosicionPrimerCorazon;
    public Canvas MyCanvas;
    public int OffSet;
    // Start is called before the first frame update
    void Start()
    {

        Transform PosCorazon = PosicionPrimerCorazon; 

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
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");

        _rigidbody2D.velocity = movementVector;
    }

    private void  OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "X")
        {
            Destroy(MyCanvas.transform.GetChild(CantDeCorazon + 1).gameObject);
            CantDeCorazon -= 1;
            Destroy(collision.gameObject);
        }
    }
}
