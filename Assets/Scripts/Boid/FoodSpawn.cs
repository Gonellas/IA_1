using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour
{
    public GameObject foodPrefab;
    public Transform spawnArea;
    public int maxFood = 5;
    private List<Transform> foodList = new List<Transform>();
    private float timer = 0f;
    public float spawnInterval = 1.5f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && foodList.Count < maxFood)
        {
            SpawnFood();
            timer = 0f;
        }
    }
    
    private void SpawnFood()
    {
        Vector3 randomPos = RandomFood();
        GameObject newFood = Instantiate(foodPrefab, randomPos, Quaternion.identity);
        foodList.Add(newFood.transform);
    }

    Vector3 RandomFood()
    {
        Vector3 areaPos = spawnArea.position;
        Vector3 areaScale = spawnArea.localScale;

        float randomX = Random.Range(areaPos.x - areaScale.x / 2, areaPos.x + areaScale.x / 2);
        float randomZ = Random.Range(areaPos.z - areaScale.z / 2, areaPos.z + areaScale.z / 2);

        Vector3 randomFood = new Vector3(randomX, 1f, randomZ);

        return randomFood;
    }
}
