using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class DialogueCSVLoader : MonoBehaviour
{
    public List<DialogueLine> LoadDialogue(TextAsset csvFile)
    {
        var lines = csvFile.text.Split('\n');
        var dialogueList = new List<DialogueLine>();

        // 정규식: 쉼표를 구분자로 쓰되, 큰따옴표 안의 쉼표는 무시
        var pattern = ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))";

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            var cols = Regex.Split(lines[i], pattern);

            for (int j = 0; j < cols.Length; j++)
                cols[j] = cols[j].Trim().Trim('"'); // 양쪽 공백/따옴표 제거

            var line = new DialogueLine
            {
                index = int.TryParse(cols[0], out int id) ? id : -1,
                name = cols.Length > 1 ? cols[1] : "",
                text = cols.Length > 2 ? cols[2] : "",
                option1 = cols.Length > 3 ? cols[3] : "",
                option2 = cols.Length > 4 ? cols[4] : "",
                function1 = cols.Length > 5 ? cols[5] : "",
                function2 = cols.Length > 6 ? cols[6] : "",
                background = cols.Length > 7 ? cols[7] : ""
            };

            dialogueList.Add(line);
        }

        return dialogueList;
    }
}
