using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultiesDatabase", menuName = "Scriptable Objects/Difficulties Database")]
public class DifficultiesDatabase : ScriptableObject
{
    public List<DifficultyData> Difficulties;

    public DifficultyData GetById(int id)
    {
        return Difficulties.Find(difficulty => difficulty.Id == id);
    }

    void OnValidate()
    {
        if (Difficulties != null)
        {
            var duplicates = Difficulties.GroupBy(difficulty => difficulty.Id)
                                     .Where(group => group.Count() > 1)
                                     .Select(group => group.Key);
            foreach (var id in duplicates)
            {
                Debug.LogWarning($"Duplicate difficulty ID found: {id}");
            }
        }
    }
}
