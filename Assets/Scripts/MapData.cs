using UnityEngine;

[CreateAssetMenu(fileName = "NewMap", menuName = "FPS/MapData")]
public class MapData : ScriptableObject
{
    public string mapName;        // Name shown on the button
    public string sceneName;      // EXACT name of the Unity Scene file
    public Sprite mapThumbnail;   // (Optional) For the UI later
}