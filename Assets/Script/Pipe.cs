using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField]
    private Bird bird;
    [SerializeField]
    private float speed = 1;


    // Update is called once per frame
    void Update()
    {
        //Melakukan pengecekan jika burung belum mati
        if (!bird.IsDead())
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }    
    }

    //Membuat bird mati ketika bersentuhan dan menjatuhkannya ke ground jika mengenai collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bird bird = collision.gameObject.GetComponent<Bird>();

        //Pengecekan null value 
        if (bird)
        {
            //Mendapatkan komponen Collider pada game object
            Collider2D collider = GetComponent<Collider2D>();

            //Melakukan pengecekan null atau tidak
            if (collider)
            {
                //Menonaktifkan collider
                collider.enabled = false;
            }

            //Burung mati
            bird.Dead();
        }
    }
}
