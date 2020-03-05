using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    //Global Variables
    [SerializeField]
    private Bird bird;
    [SerializeField]
    private Pipe pipeUp, pipeDown;
    [SerializeField]
    private float spawnInterval = 1.5f;
    [SerializeField]
    public float holeSize;
    [SerializeField]
    private float maxMinOffset = 1;
    [SerializeField]
    private Point point;

    //varible penampung coroutine yang sedang berjalan
    private Coroutine CR_Spawn;

    void Start()
    {
        StartSpawn();
    }

    void StartSpawn()
    {
        

        //Menjalankan fungsi coroutine IeSpawn()
        if (CR_Spawn == null)
        {
            CR_Spawn = StartCoroutine(IeSpawn());
        }

    }

    void SpawnPipe()
    {
        //random untuk lubang pipa
        holeSize = Random.Range(1f, 2f);

        //Menduplikasi game object pipeUp dan menempatkan posisinya sama dengan game object ini tetapi dirotasi 180 derajat
        Pipe newPipeUp = Instantiate(pipeUp, transform.position, Quaternion.Euler(0, 0, 180));

        //Mengaktifkan gameobject newPipeUp
        newPipeUp.gameObject.SetActive(true);

        //Menduplikasi gameobject pipeDown dan menempatkan posisi nya sama dengan game object
        Pipe newPipeDown = Instantiate(pipeDown, transform.position, Quaternion.identity);

        //Mengaktidkan gameobject newPipeDown
        newPipeDown.gameObject.SetActive(true);

        //Menempatkan posisi dari pipa yang sudah terbentuk agar memiliki lubang ditengah
        newPipeUp.transform.position += Vector3.up * (holeSize / 2);
        newPipeDown.transform.position += Vector3.down * (holeSize / 2);

        //Menempatkan posisi pipa yang telah dibentuk agar posisinya menyesuaikan dengan fungsi Sin 
        float y = maxMinOffset * Mathf.Sin(Time.time);
        newPipeUp.transform.position += Vector3.up * y;
        newPipeDown.transform.position += Vector3.up * y;

        Point newPoint = Instantiate(point, transform.position, Quaternion.identity);
        newPoint.gameObject.SetActive(true);
        newPoint.SetSize(holeSize);
        newPoint.transform.position += Vector3.up * y;
    }

    IEnumerator IeSpawn()
    {
        while (true)
        {
            //Jika burung mati maka akan menghentikan pembuatan pipa baru
            if (bird.IsDead())
            {
                StopSpawn();
            }

            //Membuat pipa baru
            SpawnPipe();

            //Menunggu beberapa detik sesuai dengan spawn interval
            yield return new WaitForSeconds(spawnInterval);
        
        }
    }

    void StopSpawn()
    {
        //Menghentikan Coroutine IeSpawn jika sebelumnya sudah dijalankan 
        if (CR_Spawn != null)
        {
            StopCoroutine(CR_Spawn);
        }
    }
}
