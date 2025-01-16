using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject[] gameObjects; // Mảng chứa các đối tượng cần spawn
    private List<GameObject> spawnedObjects = new List<GameObject>(); // Danh sách lưu các đối tượng đã spawn
    public float fadeDuration = 5f; // Thời gian làm mờ

    void Start()
    {
        StartCoroutine(SpawnAndMoveObjects());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Work");
            foreach (GameObject obj in spawnedObjects)
            {
                StartCoroutine(FadeCube(obj));
            }
        }
    }

    IEnumerator SpawnAndMoveObjects()
    {
        // Spawn 3 đối tượng cùng lúc
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(2);
            float randomPosX = Random.Range(-5f, 5f);
            float randomPosY = Random.Range(-5f, 5f);
            float randomPosZ = Random.Range(-5f, 5f);
            Vector3 randomPos = new Vector3(randomPosX, randomPosY, randomPosZ);

            int randomIndex = Random.Range(0, gameObjects.Length);
            GameObject newObject = Instantiate(gameObjects[randomIndex], randomPos, Quaternion.identity);
            spawnedObjects.Add(newObject);
        }

        // Di chuyển từng đối tượng lần lượt, chờ 1 giây giữa mỗi lần di chuyển
        foreach (GameObject obj in spawnedObjects)
        {
            StartCoroutine(MoveObject(obj)); // Bắt đầu di chuyển đối tượng
            yield return new WaitForSeconds(1); // Chờ 1 giây trước khi di chuyển đối tượng tiếp theo
        }
    }

    IEnumerator MoveObject(GameObject obj)
    {
        float elapsedTime = 0f;
        float randomPosX = Random.Range(-5f, 5f);
        float randomPosY = Random.Range(-5f, 5f);
        float randomPosZ = Random.Range(-5f, 5f);
        Vector3 randomPos = new Vector3(randomPosX, randomPosY, randomPosZ);

        while (elapsedTime < 5)
        {
            obj.transform.position = Vector3.Lerp(obj.transform.position, randomPos, elapsedTime / 5);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeCube(GameObject cubeClone)
    {
        Renderer renderer = cubeClone.GetComponent<Renderer>();
        if (renderer == null || renderer.material == null)
        {
            Debug.LogWarning("Đối tượng không có Renderer hoặc Material!");
            yield break;
        }

        Color color = renderer.material.color;
        float startAlpha = color.a; // Alpha ban đầu
        float endAlpha = 0f; // Alpha sau khi làm mờ

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            // Tính alpha hiện tại dựa trên thời gian đã trôi qua
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);

            // Cập nhật giá trị alpha
            color.a = newAlpha;
            renderer.material.color = color;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Đảm bảo alpha đạt giá trị cuối cùng
        color.a = endAlpha;
        renderer.material.color = color;
    }
}
