using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject[] itemsToSpawn; // ������ ��������� ��� ������
    public float spawnIntervalLevel1; // �������� ����� ������� ���������
    public float spawnIntervalLevel2;
    public float spawnIntervalLevel3;
    public float spawnIntervalLevel4 ;
    public float spawnIntervalLevel5;
    float spawnInterval;
    public float spawnWidth = 8f; // ������ �������, � ������� ����� ���������� ��������

    private float timeSinceLastSpawn;
    private void Start()
    {
       
    }

    void Update()
    {
        switch (PlayerPrefs.GetInt("levelNumber"))
        {
            case 1:
                spawnInterval = spawnIntervalLevel1;
                break;
            case 2:
                spawnInterval = spawnIntervalLevel2;
                break;
            case 3:
                spawnInterval = spawnIntervalLevel3;
                break;
            case 4:
                spawnInterval = spawnIntervalLevel4;
                break;
            case 5:
                spawnInterval = spawnIntervalLevel5;
                break;
            default:
                // code block
                break;

        }
        // ��������� ����� � ���������� ������
        timeSinceLastSpawn += Time.deltaTime;

        // ���� ������ ���������� �������, ����� ���������� ����� �������
        if (timeSinceLastSpawn >= spawnInterval)
        {
            
            // ������� �������
            SpawnItem();

            // ���������� ������
            timeSinceLastSpawn = 0f;
        }

    }

    void SpawnItem()
    {
        // �������� ��������� ������� �� �������
        GameObject itemToSpawn = itemsToSpawn[Random.Range(0, itemsToSpawn.Length)];

        // ���������� ��������� ������� ��� ������ �������� � �������� ������ spawnWidth
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth / 2f, spawnWidth / 2f), transform.position.y, 0f);

        // ������� �������
        Instantiate(itemToSpawn, spawnPosition, Quaternion.identity);
    }
}
