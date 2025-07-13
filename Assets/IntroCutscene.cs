using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroCutscene : MonoBehaviour
{
    public Image backgroundFade;
    public Image backgroundPanel;
    public Sprite[] backgroundSprites; // 인덱스로 배경 설정
    public TextAsset introCSV;
    public DialogueCSVLoader csvLoader;
    public DialogueSystem dialogueSystem;
    public PlayerMovement playerMovement;
    [SerializeField] GameObject objectToDestroy;
    void Start()
    {
        StartCoroutine(RunIntro());
    }


    IEnumerator FadeIn()
    {
        // 검정 화면 페이드 인
        float duration = 1f; // 1초 동안 페이드 인
        float elapsed = 0f;
        Color color = backgroundFade.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / duration);
            backgroundFade.color = color;
            yield return null;
        }

        // 페이드 인 완료 후 투명하게 설정
        color.a = 0f;
        backgroundFade.color = color;
    }
    IEnumerator RunIntro()
    {
        // 1. 플레이어 조작 막기
        playerMovement.enabled = false;

        // 2. 페이드 인
        yield return StartCoroutine(FadeIn());

        // 3. 대화 시작
        var lines = csvLoader.LoadDialogue(introCSV);
        dialogueSystem.StartDialogue(lines);

        // 4. 대사 종료 기다리기
        yield return new WaitUntil(() => dialogueSystem.IsFinished);

        // 5. 배경 제거
        backgroundPanel.sprite = null;

        // 6. 조작 다시 허용
        playerMovement.enabled = true;

        // 7. 오브젝트 제거
        Destroy(objectToDestroy);
    }

    // 호출 예시: DialogueSystem 쪽에서 function1 처리 시 이 함수 호출
    public void ChangeBackground(string keyword)
    {
        switch (keyword)
        {
            case "검은화면":
                backgroundPanel.sprite = backgroundSprites[0];
                break;
            case "배경전환(불타는 배경)":
                backgroundPanel.sprite = backgroundSprites[1];
                break;
            case "익숙한천장":
                backgroundPanel.sprite = backgroundSprites[2];
                break;
            case "가게 앞":
                backgroundPanel.sprite = backgroundSprites[3];
                break;
            case "TV 장면":
                backgroundPanel.sprite = backgroundSprites[4];
                break;
            default:
                Debug.Log($"알 수 없는 배경 키워드: {keyword}");
                break;
        }
    }
}
