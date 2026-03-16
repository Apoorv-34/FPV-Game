using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadoutMenu : MonoBehaviour
{
    public WeaponData ar, sniper, uzi, shotgun;

    public void SelectWeapon(string type)
    {
        if (type == "AR") WeaponSelectionManager.SelectedWeapon = ar;
        else if (type == "Sniper") WeaponSelectionManager.SelectedWeapon = sniper;
        else if (type == "Uzi") WeaponSelectionManager.SelectedWeapon = uzi;
        else if (type == "Shotgun") WeaponSelectionManager.SelectedWeapon = shotgun;

        StartGame();
    }

    void StartGame()
    {
        string mapToLoad = MapSelectionManager.SelectedMapScene;

        if (!string.IsNullOrEmpty(mapToLoad))
        {
            Debug.Log("Loading Map: " + mapToLoad);
            SceneManager.LoadScene(mapToLoad);
        }
        else
        {
            // Error handling if the player didn't come from the Map Scene
            Debug.LogError("FATAL ERROR: No map data found in MapSelectionManager! Returning to Start.");
            SceneManager.LoadScene("StartScene");
        }
    }
}