using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public DialogueLoader loader;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Button nextButton;

    public GameObject choicePanel;
    public Button choice1Button;
    public Button choice2Button;

    private Dictionary<int, DialogueData> dialogueMap = new Dictionary<int, DialogueData>();
    private int currentID = 1;

    void Start()
    {
        foreach (var data in loader.dialogueList)
        {
            dialogueMap[data.id] = data;
        }

        nextButton.onClick.AddListener(ShowNextDialogue);
        ShowDialogue(currentID);
    }

    void ShowDialogue(int id)
    {
        if (!dialogueMap.ContainsKey(id))
        {
            Debug.Log("대화 끝!");
            dialogueText.text = "";
            nameText.text = "";
            return;
        }

        currentID = id;
        var line = dialogueMap[id];

        nameText.text = line.characterName;
        dialogueText.text = line.dialogue;

        if (!string.IsNullOrEmpty(line.choice1Text))
        {
            ShowChoices(line);
        }
        else
        {
            choicePanel.SetActive(false);
            nextButton.gameObject.SetActive(true);
        }
    }

    void ShowChoices(DialogueData line)
    {
        choicePanel.SetActive(true);
        nextButton.gameObject.SetActive(false);

        choice1Button.GetComponentInChildren<TextMeshProUGUI>().text = line.choice1Text;
        choice2Button.GetComponentInChildren<TextMeshProUGUI>().text = line.choice2Text;

        choice1Button.onClick.RemoveAllListeners();
        choice2Button.onClick.RemoveAllListeners();

        choice1Button.onClick.AddListener(() => {
            choicePanel.SetActive(false);
            ShowDialogue(line.choice1NextID);
        });

        choice2Button.onClick.AddListener(() => {
            choicePanel.SetActive(false);
            ShowDialogue(line.choice2NextID);
        });
    }

    void ShowNextDialogue()
    {
        ShowDialogue(currentID + 1);
    }
}
