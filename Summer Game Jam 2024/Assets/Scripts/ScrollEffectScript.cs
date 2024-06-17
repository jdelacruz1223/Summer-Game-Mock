using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollEffectScript : MonoBehaviour
{
    public float scrollSpeed = 5f; // Speed at which the background scrolls
    public GameObject roadSegmentPrefab; // Prefab for road segments
    public float segmentLength = 10f; // Length of each road segment
    public int initialSegmentCount = 5; // Initial number of road segments

    private Queue<GameObject> roadSegments;
    private Camera mainCamera;
    private float cameraWidth;

    private static ScrollEffectScript instance;

    public static ScrollEffectScript GetInstance() { return instance; }

    private void Awake()
    {
        if (instance != null) Debug.LogWarning("Another instance of the ScrollEffectScript is running.");
        instance = this;
    }

    void Start()
    {
        roadSegments = new Queue<GameObject>();
        roadSegmentPrefab.SetActive(false);

        mainCamera = Camera.main;
        float halfFov = mainCamera.fieldOfView * 0.5f;
        float cameraHeight = mainCamera.transform.position.y * Mathf.Tan(Mathf.Deg2Rad * halfFov);
        cameraWidth = cameraHeight * mainCamera.aspect;

        int initialSegmentCount = Mathf.CeilToInt(cameraWidth / segmentLength) + 2;

        for (int i = 0; i < initialSegmentCount; i++)
        {
            Vector3 position = new Vector3(10 + i * segmentLength * 10, transform.position.y, transform.position.z);
            GameObject segment = Instantiate(roadSegmentPrefab, position, Quaternion.identity, transform);
            
            segment.SetActive(true);
            roadSegments.Enqueue(segment);
        }
    }

    void Update()
    {
        if (RandomEncounterManager.GetInstance().currentlyInEncounter) return;

        foreach (GameObject segment in roadSegments)
        {
            segment.transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        }
    }

    public void RepositionRoadSegment()
    {
        if (roadSegments.Count > 0)
        {
            GameObject firstSegment = roadSegments.Peek();

            roadSegments.Dequeue();
            GameObject lastSegment = roadSegments.ToArray()[roadSegments.Count - 1]; // Get the last segment
            Vector3 newPosition = new Vector3(lastSegment.transform.position.x + 10 * segmentLength, transform.position.y, transform.position.z);
            firstSegment.transform.position = newPosition;
            roadSegments.Enqueue(firstSegment);

            Debug.Log("Repositioned segment to: " + newPosition);
        }
    }
}
