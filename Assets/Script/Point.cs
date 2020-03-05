using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Point : MonoBehaviour
{
    [SerializeField]
    private Bird bird;
    [SerializeField]
    private float speed = 1;

    void Update()
    {
        if (!bird.IsDead())
        {
            //Menggerakkan game object ke sebelah kiri dengan kecepatan tertentu
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
    }

    public void SetSize(float size)
    {
        //Mendapatkan komponen BoxCollider2d
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            //Mengubah ukuran collider sesuai dengan parameter
            collider.size = new Vector2(collider.size.x, size);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {

        //Mendapatkan komponen Bird
        Bird bird = collision.gameObject.GetComponent<Bird>();

        //Menambahkan score jika burung tidak null atau belum mati
        if(bird && !bird.IsDead())
        {
            bird.AddScore(1);
        }
    }
}
