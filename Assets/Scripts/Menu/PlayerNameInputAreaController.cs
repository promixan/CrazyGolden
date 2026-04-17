using TMPro;
using UnityEngine;

public class PlayerNameInputAreaController : MonoBehaviour
{

    private TMP_InputField inputField;

    void Awake()
    {
        inputField = gameObject.GetComponentInParent<TMP_InputField>();
        inputField.onEndEdit.AddListener(UpdatePlayerName);
    }

    void Start()
    {
        var name = CrossSceneManager.Instance.PlayerName;
        if (!CrossSceneManager.Instance.DefaultPlayerName.Equals(name))
        {
            inputField.text = name;
        }
    }

    void UpdatePlayerName(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            CrossSceneManager.Instance.PlayerName = value;
        }
    }
}
