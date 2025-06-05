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
    [Header("script references")]
    [SerializeField] WireTop wireTop;

    // vibration parameters
    [Header("vibration parameters")]
    [SerializeField] float duration = 2f;
    [SerializeField] float magnitude = 0.2f;
    float elapsedTime = 0f;

    [SerializeField] GameObject juicer;
    Transform originalJuicerTransform;

    private Dictionary<string, GameObject> fruitToCubeMap;

    public bool isJuicing = false;

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
            // destroy fruit
            Destroy(other.gameObject);

            StartCoroutine(JuicingDelay(other, cubeToSpawn));
            StartCoroutine(Vibrate());
        }
    }

    void SpawnJuice(Collision2D fruit, GameObject cubeToSpawn)
    {
        // Instantiate the corresponding cube
        GameObject spawnedCube = Instantiate(cubeToSpawn, spawnPos.position, spawnPos.rotation);

        Rigidbody2D rb = spawnedCube.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(-spawnedCube.transform.right * speed, ForceMode2D.Impulse);
        }
    }

    IEnumerator JuicingDelay(Collision2D other, GameObject cubeToSpawn)
    {
        yield return new WaitForSeconds(2f);
        SpawnJuice(other, cubeToSpawn);
    }

    // vibration
    IEnumerator Vibrate()
    {
        // originalJuicerTransform.position = juicer.transform.position;
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;

            float x = Random.Range(-1f, 1f) * magnitude;

            yield return null;

            juicer.transform.position = new Vector2(juicer.transform.position.x + x, juicer.transform.position.y);
        }
        juicer.transform.localPosition = Vector3.zero;
    }
}
