using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class DIfficultyNameController : MonoBehaviour
{
    private TMP_Text _text;

    void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _text.text = "";
    }

    void Start()
    {
        _text.text = CrossSceneManager.Instance.Difficulty.Name;
        _text.color = CrossSceneManager.Instance.Difficulty.NameColor;
    }
}
