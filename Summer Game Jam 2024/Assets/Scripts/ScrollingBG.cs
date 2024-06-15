using UnityEngine;
using System.Collections.Generic;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed = 5f; // Speed at which the background scrolls
    public GameObject roadSegmentPrefab; // Prefab for road segments
    public float segmentLength = 10f; // Length of each road segment
    public int initialSegmentCount = 5; // Initial number of road segments

    private Queue<GameObject> roadSegments = new Queue<GameObject>();
    private Camera mainCamera;
    private float cameraWidth;

    void Start()
    {
        // Get the main camera and calculate the width of the view at the camera's position
        mainCamera = Camera.main;
        float halfFov = mainCamera.fieldOfView * 0.5f;
        float cameraHeight = mainCamera.transform.position.y * Mathf.Tan(Mathf.Deg2Rad * halfFov);
        cameraWidth = cameraHeight * mainCamera.aspect;

        // Calculate the number of segments needed to fill the camera view width
        int initialSegmentCount = Mathf.CeilToInt(cameraWidth / segmentLength) + 2; // +2 for smooth transition

        // Create initial road segments and place them end-to-end
        for (int i = 0; i < initialSegmentCount; i++)
        {
            Vector3 position = new Vector3(i * segmentLength, transform.position.y, transform.position.z);
            GameObject segment = Instantiate(roadSegmentPrefab, position, Quaternion.identity, transform);
            roadSegments.Enqueue(segment);
        }
    }

    void Update()
    {
        // Move each road segment to the left
        foreach (GameObject segment in roadSegments)
        {
            segment.transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        }

        // Check if the first segment is out of view and reposition it
        if (roadSegments.Count > 0)
        {
            GameObject firstSegment = roadSegments.Peek();
            if (firstSegment.transform.position.x < -segmentLength)
            {
                roadSegments.Dequeue();
                GameObject lastSegment = roadSegments.ToArray()[roadSegments.Count - 1]; // Get the last segment
                Vector3 newPosition = new Vector3(lastSegment.transform.position.x + segmentLength, transform.position.y, transform.position.z);
                firstSegment.transform.position = newPosition;
                roadSegments.Enqueue(firstSegment);

                Debug.Log("Repositioned segment to: " + newPosition);
            }
        }

        Debug.Log("Queue count: " + roadSegments.Count);
    }
}
