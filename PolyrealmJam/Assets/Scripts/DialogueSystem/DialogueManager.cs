using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class DialogueManager : MonoBehaviour
{
    #region Constants
    public const bool DIALOGUE_ENABLED = true;
    public const bool DIALOGUE_DISABLED = false;
    #endregion

    #region Statics

    private static DialogueManager activeManager = null;

    public static DialogueManager ActiveManager
    {
        get
        {
            if (activeManager == null)
                activeManager = FindObjectOfType<DialogueManager>();

            if (activeManager == null)
                Debug.LogErrorFormat("Cannot find DialogueManager in the game. Please make sure there is initialized DialogueManager.");

            return activeManager;
        }

        set => activeManager = value;
    }

    #endregion



    public event Action OnDialogueStart;
    public event Action OnDialogueEnd;

    public Queue<string> sentences;

    [Header("Manager Fields and Settings")]
    public CanvasGroup dialoguePanel;
    public Text speakerName;
    public Text dialogueSentece;
    [Range(0f, 1f)] public float typeDelay;

    [Header("Debug")]
    [SerializeField] bool isEnabled;
    [SerializeField] bool isTyping;
    [SerializeField] string currentSentence;

    private Coroutine typeCoroutine;

    private void Awake()
    {
        
    }

    private void OnDestroy()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextSentence();
        }
    }

    private void Start()
    {
        sentences = new Queue<string>();
    }

    #region Main Functions

    public void StartConversation(Dialogue _dialogues, string _speakerName)
    {
        OnDialogueStart?.Invoke();

        isEnabled = DIALOGUE_ENABLED;
        speakerName.text = _speakerName;

        sentences.Clear();
        foreach (var dialogue in _dialogues.sentences)
        {
            sentences.Enqueue(dialogue);
        }

        VisualizeTextBox();
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        if (isTyping)
        {
            if (typeCoroutine != null)
            {
                StopCoroutine(typeCoroutine);
                isTyping = false;
                dialogueSentece.text = currentSentence;
            }
            return;
        }
        currentSentence = sentences.Dequeue();
        typeCoroutine = StartCoroutine(TypeSentence(currentSentence));
    }

    public void EndDialogue()
    {
        OnDialogueEnd?.Invoke();
        isEnabled = DIALOGUE_DISABLED;

        VisualizeTextBox();
    }

    IEnumerator TypeSentence(string _sentence)
    {
        dialogueSentece.text = "";
        int currIndx;

        isTyping = true;
        for (int i = 0; i < _sentence.Length; i++)
        {
            dialogueSentece.text += _sentence[i];
            currIndx = i;
            yield return new WaitForSeconds(typeDelay);
            if (currIndx >= _sentence.Length -1)
            {
                isTyping = false;
            }
        }
    }
    #endregion

    #region Assiting Function
    private void VisualizeTextBox()
    {
        if (isEnabled)
        {
            dialoguePanel.alpha = 1;
            dialoguePanel.blocksRaycasts = DIALOGUE_ENABLED;
        }
        else
        {
            dialoguePanel.alpha = 0;
            dialoguePanel.blocksRaycasts = DIALOGUE_DISABLED;
        }
    }
    #endregion
}
