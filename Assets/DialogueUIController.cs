using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DialogueUIController : MonoBehaviour
{
    public static DialogueUIController Instance { get; private set; }

    public GameObject dialogueUI;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 혹시 중복 생성 방지
            return;
        }

        Instance = this;
    }
    private void Start()
    {
        HideDialogue(); // 시작 시 대화 UI 숨김
    }
    public void ShowDialogue()
    {
        dialogueUI.SetActive(true);
    }

    public void HideDialogue()
    {
        dialogueUI.SetActive(false);
    }
}