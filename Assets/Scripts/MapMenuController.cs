using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMenuController : MonoBehaviour
{
    public void SelectMap(string sceneName)
    {
        // Save the map choice to our static manager
        MapSelectionManager.SelectedMapScene = sceneName;
        
        // Move to the Gun Selection scene
        SceneManager.LoadScene("Gun Menu"); // This is your gun selection scene
    }
}