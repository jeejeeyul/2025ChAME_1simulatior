using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public TextMeshProUGUI scriptText;
    public TextMeshProUGUI nameText;
    public GameObject dialogueUI;
    public Image backgroundImage;
    public Button select1Button;
    public Button select2Button;
    public TextMeshProUGUI select1Text;
    public TextMeshProUGUI select2Text;

    public float typingSpeed = 0.03f;
    private bool isTyping = false;

    private List<DialogueLine> lines;
    private int index = 0;
    private string lastName = "";

    public bool IsFinished { get; private set; } = false;

    public void StartDialogue(List<DialogueLine> dialogueLines)
    {
        lines = dialogueLines;
        index = 0;
        dialogueUI.SetActive(true);
        StartCoroutine(ShowLine());
    }

    void Update()
    {
        if (!dialogueUI.activeSelf) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                scriptText.text = lines[index].text;
                isTyping = false;
            }
            else
            {
                index++;
                if (index < lines.Count)
                    StartCoroutine(ShowLine());
                else
                    StartCoroutine(FadeOutAndClose());
            }
        }
    }

    IEnumerator ShowLine()
    {
        isTyping = true;
        var line = lines[index];

        // 선택지 버튼 항상 숨김
        select1Button.gameObject.SetActive(false);
        select2Button.gameObject.SetActive(false);

        // 이름 처리
        if (string.IsNullOrWhiteSpace(line.name))
        {
            nameText.text = lastName;
        }
        else if (line.name == "공백" || line.name == "독백")
        {
            nameText.text = "";
            lastName = "";
        }
        else
        {
            nameText.text = line.name;
            lastName = line.name;
        }

        scriptText.text = "";

        yield return HandleFunction(line.function1);
        yield return HandleFunction(line.function2);

        yield return HandleBackground(line.background);

        // 선택지 처리
        if (!string.IsNullOrEmpty(line.option1) || !string.IsNullOrEmpty(line.option2))
        {
            select1Button.gameObject.SetActive(true);
            select2Button.gameObject.SetActive(true);
            select1Text.text = line.option1;
            select2Text.text = line.option2;
            yield break;
        }

        foreach (char c in line.text)
        {
            scriptText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    IEnumerator FadeOutAndClose()
    {
        Color color = backgroundImage.color;
        color.a = 0f;
        backgroundImage.color = color;

        backgroundImage.gameObject.SetActive(true); // 암전 이미지 활성화

        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(0f, 1f, t / 1f); // 1초에 걸쳐 점점 검게
            backgroundImage.color = color;
            yield return null;
        }

        // 검정 화면 유지 후 UI 끄기
        yield return new WaitForSeconds(0.3f);

        dialogueUI.SetActive(false);
        color.a = 0f; // 투명하게 되돌리기
        backgroundImage.color = color;
        IsFinished = true; // ✅ 인트로 종료 신호
    }

    IEnumerator HandleFunction(string func)
    {
        if (string.IsNullOrWhiteSpace(func)) yield break;


        yield return null;
    }


    IEnumerator HandleBackground(string bg)
    {
        if (string.IsNullOrWhiteSpace(bg)) yield break;
        var cutscene = FindObjectOfType<IntroCutscene>();
        cutscene?.ChangeBackground(bg);

        // 실제 배경 처리 시 구현 필요
        yield return null;
    }
}
