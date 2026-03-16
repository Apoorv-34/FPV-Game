using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "FPS/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float damage;
    public float range;
    public float fireRate;
    public int magSize;
    public float zoomFOV; // <--- Add this line
    public GameObject gunPrefab; 
    // Add this line to your WeaponData script
public GameObject muzzleFlashPrefab;
}