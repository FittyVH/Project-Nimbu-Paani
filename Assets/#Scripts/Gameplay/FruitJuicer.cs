using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitJuicer : MonoBehaviour
{
    // fuit-cubes
    [System.Serializable]
    public class FruitMapping
    {
        public string tag;       // The tag of the fruit
        public GameObject cube;  // The cube to spawn for this fruit
    }
    public float speed = 10f; // throw speed

    public List<FruitMapping> fruitMappings; // A list of tag-cube pairs
    public Transform spawnPos;

    // script references
    [Header ("script references")]
    [SerializeField] WireTop wireTop;

    private Dictionary<string, GameObject> fruitToCubeMap;

    void Start()
    {
        // Initialize the dictionary from the list
        fruitToCubeMap = new Dictionary<string, GameObject>();
        foreach (var mapping in fruitMappings)
        {
            if (!fruitToCubeMap.ContainsKey(mapping.tag))
            {
                fruitToCubeMap.Add(mapping.tag, mapping.cube);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (wireTop.isPlugged && fruitToCubeMap.TryGetValue(other.collider.tag, out GameObject cubeToSpawn))
        {
            SpawnJuice(other, cubeToSpawn);
        }
    }

    void SpawnJuice(Collision2D fruit, GameObject cubeToSpawn)
    {
        Destroy(fruit.gameObject);

        // Instantiate the corresponding cube
        GameObject spawnedCube = Instantiate(cubeToSpawn, spawnPos.position, spawnPos.rotation);

        Rigidbody2D rb = spawnedCube.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(-spawnedCube.transform.right * speed, ForceMode2D.Impulse);
        }
    }
}
