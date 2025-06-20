using System.Collections.Generic;
using UnityEngine;

public class DialogueLoader : MonoBehaviour
{
    public TextAsset csvFile;
    public List<DialogueData> dialogueList = new List<DialogueData>();

    void Awake()
    {
        LoadCSV();
    }

    void LoadCSV()
    {
        string[] lines = csvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++) // 헤더 제외
        {
            string[] values = lines[i].Split(',');

            if (values.Length < 3 || string.IsNullOrWhiteSpace(values[0]))
                continue;

            DialogueData data = new DialogueData();
            data.id = int.Parse(values[0]);
            data.characterName = values[1];
            data.dialogue = values[2];
            data.backgroundName = values[3];

            if (values.Length >= 8)
            {
                data.choice1Text = values[4];
                int.TryParse(values[5], out data.choice1NextID);
                data.choice2Text = values[6];
                int.TryParse(values[7], out data.choice2NextID);
            }

            dialogueList.Add(data);
        }

        Debug.Log("CSV 로딩 완료: " + dialogueList.Count + "줄");
    }
}
