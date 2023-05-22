using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Canvas dialogBox;

    public static DialogueSystem instance;

    public Queue<string> sentences;

    public delegate void FinishDialogue();
    public FinishDialogue onFinishDialogue;

    #region Singleton
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    [ContextMenu("Start Conversation")]
    public void StartDialogue(Dialogue dialogue)
    {
        //Debug.Log("Starting conversation with " + dialogue.name);

        dialogBox.enabled = true;

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    [ContextMenu("Next Sentence")]
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        //Debug.Log(sentence);
        //dialogueText.text = sentence;

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForEndOfFrame();
            //yield return null;
        }
    }

    void EndDialogue()
    {
        if (onFinishDialogue != null)
        {
            onFinishDialogue.Invoke();
            Debug.Log("Termine dialogo");
        }

        dialogBox.enabled = false;
        Debug.Log("End conversation");
        PlayerManager.instance.isTalking = false;
        CameraBehaviour.instance.LockOnConversation();
        PlayerMovement.instance.ChangeSpeed();
    }
}
