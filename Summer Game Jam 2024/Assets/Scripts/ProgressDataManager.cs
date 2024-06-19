using UnityEngine;
using System.Collections.Generic;

public class ProgressDataManager : MonoBehaviour
{
    public static ProgressDataManager Instance { get; private set; }

    public float CurrentProgress { get; set; }
    public int LastSceneIndex { get; set; } = -1;
    public HashSet<int> VisitedCheckpoints { get; private set; } = new HashSet<int>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
