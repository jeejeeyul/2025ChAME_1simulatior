using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeetNpc : MonoBehaviour
{
    // Start is called before the first frame update
    public TextAsset dialogueCSV;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var csvLoader = FindObjectOfType<DialogueCSVLoader>();
            var dialogueSystem = FindObjectOfType<DialogueSystem>();

            List<DialogueLine> lines = csvLoader.LoadDialogue(dialogueCSV);
            dialogueSystem.StartDialogue(lines);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
