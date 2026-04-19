using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyData", menuName = "Scriptable Objects/Difficulty Data")]
public class DifficultyData : ScriptableObject
{
    public int Id;
    public string Name;
    public Color NameColor;
}
