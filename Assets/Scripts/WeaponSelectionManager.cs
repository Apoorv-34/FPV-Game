using UnityEngine;
using UnityEngine.SceneManagement;

public class WeaponSelectionManager : MonoBehaviour
{
    // This static variable stores the data so it carries over to the Map Scene
    public static WeaponData SelectedWeapon; 

    [Header("All Available Weapons")]
    public WeaponData shotgunData;
    public WeaponData uziData;
    public WeaponData sniperData;
    public WeaponData arData;

    // Original Button Functions
    public void SelectShotgun()
    {
        SelectedWeapon = shotgunData;
        Debug.Log("Shotgun Selected");
        StartGame(); // Automatically starts game after click
    }

    public void SelectUzi()
    {
        SelectedWeapon = uziData;
        Debug.Log("Uzi Selected");
        StartGame();
    }

    public void SelectSniper()
    {
        SelectedWeapon = sniperData;
        Debug.Log("Sniper Selected");
        StartGame();
    }

    public void SelectAR()
    {
        SelectedWeapon = arData;
        Debug.Log("AR Selected");
        StartGame();
    }

    private void StartGame()
    {
        // Make sure "Map Scene" matches your scene name in Build Settings exactly
        SceneManager.LoadScene("Map Scene"); 
    }
}