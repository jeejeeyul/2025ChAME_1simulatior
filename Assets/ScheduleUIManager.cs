using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScheduleUIManager : MonoBehaviour
{
    public List<TMP_Dropdown> actionSlots; // Slot1~6 TMP_Dropdown 연결
    public Button confirmButton;

    public enum ActionType
    {
        None, Clean, Sell, Research, Train
    }

    public List<ActionType> plannedActions = new List<ActionType>();

    void Start()
    {
        confirmButton.onClick.AddListener(OnConfirm);
        InitDropdowns();
    }

    void InitDropdowns()
    {
        foreach (var dropdown in actionSlots)
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(new List<string> { "행동 없음", "청소", "주류 판매", "주류 연구", "운동" });
        }
    }

    void OnConfirm()
    {
        plannedActions.Clear();
        foreach (var dropdown in actionSlots)
        {
            plannedActions.Add((ActionType)dropdown.value);
        }

        Debug.Log("계획된 행동:");
        for (int i = 0; i < plannedActions.Count; i++)
        {
            Debug.Log($"{i + 1}시간차: {plannedActions[i]}");
        }

        // TODO: 행동 처리 시스템으로 전달
        this.gameObject.SetActive(false); // UI 닫기
    }
}
