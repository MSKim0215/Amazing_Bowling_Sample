using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGenerator : MonoBehaviour
{
    private GameObject[] propPrefabs;
    private BoxCollider area;
    private readonly int count = 100;

    private List<GameObject> props;

    private void Awake()
    {
        SetGenerator();
    }

    private void SetGenerator()
    {
        propPrefabs = Resources.LoadAll<GameObject>("Prefabs/Prop");
        area = GetComponent<BoxCollider>();
        props = new List<GameObject>();

        for(int i = 0; i < count; i++)
        {
            Spawn();
        }

        area.enabled = false;
    }   // ½ºÆù ¼¼ÆÃ ÇÔ¼ö , ±è¹Î¼·_220617

    private void Spawn()
    {
        int selection = Random.Range(0, propPrefabs.Length);
        GameObject selectedPrefab = propPrefabs[selection];
        Vector3 spawnPos = GetRandomPosition();
        GameObject instance = Instantiate(selectedPrefab, spawnPos, Quaternion.identity);
        instance.transform.SetParent(transform);
        props.Add(instance);
    }   // ½ºÆù ÇÔ¼ö , ±è¹Î¼·_220617

    private Vector3 GetRandomPosition()
    {
        Vector3 basePosition = transform.position;
        Vector3 size = area.size;

        float posX = GetRandomPosition(basePosition.x, size.x);
        float posY = GetRandomPosition(basePosition.y, size.y);
        float posZ = GetRandomPosition(basePosition.z, size.z);

        return new Vector3(posX, posY, posZ);
    }   // ·£´ý À§Ä¡ »ý¼º ÇÔ¼ö , ±è¹Î¼·_220617

    private float GetRandomPosition(float _pos, float _size) => _pos + Random.Range(-_size / 2f, _size / 2f);
    // ·£´ý ÁÂÇ¥ ¼¼ÆÃ ÇÔ¼ö , ±è¹Î¼·_220617

    public void Reset()
    {
        for(int i = 0; i < props.Count; i++)
        {
            props[i].transform.position = GetRandomPosition();
            props[i].SetActive(true);
        }
    }
}
