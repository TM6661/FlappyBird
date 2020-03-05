using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    [SerializeField]
    private float upForce = 100;
    [SerializeField] 
    private float fireSpeed = 10;
    [SerializeField]
    private bool isDead;
    [SerializeField]
    private UnityEvent OnJump, OnDead;
    [SerializeField]
    private int score;
    [SerializeField] 
    private Rigidbody2D fireballRb;
    [SerializeField]
    UnityEvent OnAddPoint;
    [SerializeField] 
    UnityEvent OnAddStock;
    [SerializeField]
    private Text scoreText;
    [SerializeField] 
    private Text fbStock;
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private int fireballStock;
    // Init Variable
    void Start()
    {
        //Mendapatkan komponent ketika game baru berjalan
        rigidbody2d = GetComponent<Rigidbody2D>();
        //Mendapatkan komponent animator pada game object
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Melakukan pengecekan jika belum mati dan klik kiri pada mouse
        if (!isDead && Input.GetMouseButtonDown(0))
        {
            //Burung loncat
            Jump();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        //pake up biar sekali eksekusi pas tombol lepas
        if (Input.GetMouseButtonUp(1) && fireballStock > 0)
        {
            Fireball();
            fireballStock--;
            fbStock.text = fireballStock.ToString();
        }        
    }

    //Mengecek apakah sudah mati atau belum (bernilai true atau false)
    public bool IsDead()
    {
        return isDead;
    }

    //Membuat burung mati
    public void Dead()
    {
        //Pengecekan jika belum mati dan value onDead tidak sama dengan null
        if (!isDead && OnDead != null)
        {
            //Memanggil semua event pada OnDead
            OnDead.Invoke();
        }

        //Mengeset varible Dead menjadi true
        isDead = true;
    }

    public void Fireball()
    {
        Vector3 target = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Rigidbody2D fireballSpell = Instantiate(fireballRb, target, Quaternion.identity) as Rigidbody2D;
        fireballSpell.velocity = transform.TransformDirection(new Vector3(fireSpeed, 0f, 0f));
    }

    void Jump()
    {
        //Mengecek rigidbody null atau tidak 
        if (rigidbody2d)
        {
            //Menghentikan kecepatan burung ketika jatuh
            rigidbody2d.velocity = Vector2.zero;

            //Menambahkan gaya arah ke sumbu y agar burung meloncat
            rigidbody2d.AddForce(new Vector2(0, upForce));
        }

        //Pengecekan variable yang null
        if (OnJump != null)
        {
            //Menjalankan semua event OnJump event 
            OnJump.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Menghentikan animasi burung ketika bersentuhan dengan object lain
        animator.enabled = false;
    }

    public void AddScore(int value)
    {
        //Menambah score value
        score += value;

        //Pengecekan Null value
        if (OnAddPoint != null)
        {
            //Memanggil semua event pada OnAddPoint
            OnAddPoint.Invoke();
            scoreText.text = score.ToString();
        }

        if (score % 5 == 0)
        {
            fireballStock++;
            OnAddStock.Invoke();
            fbStock.text = fireballStock.ToString();
        }
    }

}
