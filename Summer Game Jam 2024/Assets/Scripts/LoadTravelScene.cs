using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadTravelScene : MonoBehaviour
{
    // Function to load the travel scene by build index
    public void LoadTravel()
    {
        int travelSceneIndex = 4; // Replace with the actual build index of your travel scene

        SceneManager.LoadScene(travelSceneIndex);
    }
}
