using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    private Button startButton;
    private Button loadButton;
    private Button optionsButton;
    private Button exitButton;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        startButton = root.Q<Button>("StartButton");
        loadButton = root.Q<Button>("LoadButton");
        optionsButton = root.Q<Button>("OptionsButton");
        exitButton = root.Q<Button>("ExitButton");

        startButton.clicked += OnStartClicked;
        loadButton.clicked += OnLoadClicked;
        optionsButton.clicked += OnOptionsClicked;
        exitButton.clicked += OnExitClicked;
    }

    private void OnStartClicked()
    {
        SceneManager.LoadScene("MainGameScene");
    }
    private void OnLoadClicked()
    {
        SceneManager.LoadScene("LoadGameScene");
    }

    private void OnOptionsClicked()
    {
        SceneManager.LoadScene("OptionsScene");
    }

    private void OnExitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
