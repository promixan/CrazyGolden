using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas settingsCanvas;
    [SerializeField] private Canvas rankingCanvas;
    [SerializeField] private TMP_Text difficultyNameText;

    void Start()
    {
        OpenMenu();
    }

    public void LaunchGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenMenu()
    {
        menuCanvas.gameObject.SetActive(true);
        var difficultyController = difficultyNameText.GetComponent<DifficultyNameController>();
        difficultyController.UpdateName();
        settingsCanvas.gameObject.SetActive(false);
        rankingCanvas.gameObject.SetActive(false);
    }

    public void OpenSettings()
    {
        menuCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
        rankingCanvas.gameObject.SetActive(false);
    }

    public void OpenRanking()
    {
        menuCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(false);
        rankingCanvas.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        CrossSceneManager.Instance.SaveAllData();
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
