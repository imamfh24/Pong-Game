using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField] GameObject []itemsArray;
    [SerializeField] float delaySpawn = 5f;
    [SerializeField] float offsetSpawn = 10f;

    private float timerSpawn = 0f;
    private bool isSpawn = true;

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount <= 2 && isSpawn)
        {
            TimerSpawnItem();
        }
    }

    public bool IsSpawn
    {
        set { isSpawn = value; }
        get { return isSpawn; }
    }

    private void TimerSpawnItem()
    {
        timerSpawn += Time.deltaTime;
        if (timerSpawn > delaySpawn)
        {
            SpawnGameItem();
            timerSpawn = 0f;
        }
    }

    public void ResetSpawnTimer()
    {
        timerSpawn = 0f;
    }

    void SpawnGameItem()
    {
        int indexRandom = Random.Range(0, itemsArray.Length);
        Vector2 positionRandom = new Vector2(Random.Range(-offsetSpawn, offsetSpawn), Random.Range(-offsetSpawn, offsetSpawn));

        GameObject itemSpawn = Instantiate(itemsArray[indexRandom], positionRandom, Quaternion.identity);

        itemSpawn.transform.parent = transform;
    }

    public void DestroyChildSpawn()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        ResetSpawnTimer();
    }
}
