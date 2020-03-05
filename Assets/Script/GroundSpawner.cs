using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class GroundSpawner : MonoBehaviour
{
    //Menampung referensi ground yang ingin dibuat
    [SerializeField]
    private Ground groundRef;

    //Menampung ground sebelumnya
    [SerializeField]
    private Ground prevGround;
    
    //Method ini akan membuat Ground game object baru
    private void SpawnGround()
    {
    
        //Pengecekan null variable
        if (prevGround != null)
        {
            //Menduplikat Groundref
            Ground newGround = Instantiate(groundRef);

            //Mengaktifkan game object
            newGround.gameObject.SetActive(true);

            //Menepatkan new ground dengan posisi dengan posisi nextground dari prevground agar posisinya sejajar dengan ground sebelumnya
            prevGround.SetNextGround(newGround.gameObject);
        }
    }

    //Method ini akan dipanggil ketika terdapat game object lain yang memiliki komponen collider keluar dari area collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        //Mencari komponen ground dari object yang keluar dari area trigger
        Ground ground = collision.GetComponent<Ground>();

        //Pengecekan null variable
        if (ground)
        {
            //Mengisi variable prevGround
            prevGround = ground;

            //Membuat ground baru
            SpawnGround();
        }
    }
}
