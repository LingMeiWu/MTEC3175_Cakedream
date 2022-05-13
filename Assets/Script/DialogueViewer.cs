using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DialogueObject;
using UnityEngine.Events;
using System;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class DialogueViewer : MonoBehaviour
{
    [SerializeField] Transform parentOfResponses;
    [SerializeField] Button prefab_btnResponse;
    [SerializeField] Text txtMessage;
    DialogueController controller;
    private WaitForSeconds charDelay;

    public bool isTyping;
    private bool skipVoice;
    public Sprite[] backgrounds;
    public Image backgroundObject;
  

    float characterSpeed = 0.1f;
    [DllImport("__Internal")]
    private static extern void openPage(string url);

    private void Start()
    {
        controller = GetComponent<DialogueController>();
        controller.onEnteredNode += OnNodeEntered;
        controller.InitializeDialogue();

        charDelay = new WaitForSeconds(characterSpeed);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            charDelay = new WaitForSeconds(0.01f);
        }

        
    }

    public static void KillAllChildren(UnityEngine.Transform parent)
    {
        UnityEngine.Assertions.Assert.IsNotNull(parent);
        for (int childIndex = parent.childCount - 1; childIndex >= 0; childIndex--)
        {
            UnityEngine.Object.Destroy(parent.GetChild(childIndex).gameObject);
        }
    }

    private void OnNodeSelected(int indexChosen)
    {
        Debug.Log("Chose: " + indexChosen);
        controller.ChooseResponse(indexChosen);
    }
    private void OnNodeEntered(Node newNode)
    {
        if (newNode == null) return;
        changeBackground(newNode);
        KillAllChildren(parentOfResponses);
        StartCoroutine(DisplayText(newNode.text,newNode));
  
    }
    
    public void changeBackground (Node node)
    {
        if (node == null) return;  
        List<string> tags = node.tags;
        foreach (string tag in tags ) 
        {
            if (tag == "bedroom")
            {
                backgroundObject.sprite = backgrounds[0];
            }
            else if (tag == "hut")
            {
                backgroundObject.sprite = backgrounds[1];
            }
            else if (tag == "hutIN")
            {
                backgroundObject.sprite = backgrounds[2];
            }
            else if (tag == "monkeyBeach")
            {
                backgroundObject.sprite = backgrounds[3];
            }
            else if (tag == "monkeyBeach1")
            {
                backgroundObject.sprite = backgrounds[4];
            }
            else if (tag == "monkeyPoop")
            {
                backgroundObject.sprite = backgrounds[5];
            }
            else if (tag == "monkey")
            {
                backgroundObject.sprite = backgrounds[6];
            }
            else if (tag == "trees")
            {
                backgroundObject.sprite = backgrounds[7];
            }
            else if (tag == "beach")
            {
                backgroundObject.sprite = backgrounds[8];
            }
            else if (tag == "banana")
            {
                backgroundObject.sprite = backgrounds[9];
            }
            else if (tag == "waterfallClear")
            {
                backgroundObject.sprite = backgrounds[10];

            }
            else if (tag == "waterfallMilk")
            {
                backgroundObject.sprite = backgrounds[11];
            }
            else if (tag == "lab")
            {
                backgroundObject.sprite = backgrounds[12];
            }
            else if (tag == "kitchen")
            {
                backgroundObject.sprite = backgrounds[13];
            }
            else if (tag == "cave")
            {
                backgroundObject.sprite = backgrounds[14];
            }
            else if (tag == "umm")
            {
                backgroundObject.sprite = backgrounds[15];
            }
            else if (tag == "cumber")
            {
                backgroundObject.sprite = backgrounds[16];
            }
            else if (tag == "kit")
            {
                backgroundObject.sprite = backgrounds[17];
            }
            else if (tag == "pink")
            {
                backgroundObject.sprite = backgrounds[18];
            }
            else if (tag == "egg")
            {
                backgroundObject.sprite = backgrounds[19];
            }
            else if (tag == "jar")
            {
                backgroundObject.sprite = backgrounds[20];
            }
            else if (tag == "jarM")
            {
                backgroundObject.sprite = backgrounds[21];
            }
            else if (tag == "list")
            {
                backgroundObject.sprite = backgrounds[22];
            }
            else if (tag == "pearl")
            {
                backgroundObject.sprite = backgrounds[23];
            }
            else if (tag == "note")
            {
                backgroundObject.sprite = backgrounds[24];
            }
            else if (tag == "sand")
            {
                backgroundObject.sprite = backgrounds[25];
            }
            else if (tag == "mermaid")
            {
                backgroundObject.sprite = backgrounds[26];
            }
            else if (tag == "unicorn")
            {
                backgroundObject.sprite = backgrounds[27];
            }
            else if (tag == "whaleW")
            {
                backgroundObject.sprite = backgrounds[28];
            }
            else if (tag == "whaleC")
            {
                backgroundObject.sprite = backgrounds[29];
            }
            else if (tag == "whaleHa")
            {
                backgroundObject.sprite = backgrounds[30];
            }
            else if (tag == "cloverT")
            {
                backgroundObject.sprite = backgrounds[31];
            }
            else if (tag == "m3pNL")
            {
                backgroundObject.sprite = backgrounds[32];
            }
            else if (tag == "m3pTL")
            {
                backgroundObject.sprite = backgrounds[33];
            }
            else if (tag == "m3pT")
            {
                backgroundObject.sprite = backgrounds[34];
            }
            else if (tag == "clovC")
            {
                backgroundObject.sprite = backgrounds[35];
            }
            else if (tag == "clovE")
            {
                backgroundObject.sprite = backgrounds[36];
            }
            else if (tag == "CloverN")
            {
                backgroundObject.sprite = backgrounds[37];
            }
            else if (tag == "black")
            {
                backgroundObject.sprite = backgrounds[38];
            }
            else if (tag == "badEnding1")
            {
                backgroundObject.sprite = backgrounds[39];
            }
            else if (tag == "goodEnding1")
            {
                backgroundObject.sprite = backgrounds[40];
            }
        }
    }

    private IEnumerator DisplayText(string text, Node newNode)
    {   
        isTyping = true;
        var chars = text.ToCharArray();
        string t = "";

        // int limit = Mathf.Min(chars.Length, dialogueCharacterLimit);
        int limit = (chars.Length);
 
        for (int i = 0; i < limit; i++)
        {
            t += chars[i];
            txtMessage.text = t;

            yield return charDelay;
        }
        isTyping = false;
        charDelay = new WaitForSeconds(characterSpeed);

        KillAllChildren(parentOfResponses);

        for (int i = newNode.responses.Count - 1; i >= 0; i--)
        {
            int currentChoiceIndex = i;
            var response = newNode.responses[i];
            var responceButton = Instantiate(prefab_btnResponse, parentOfResponses);
            responceButton.GetComponentInChildren<Text>().text = response.displayText;
            responceButton.onClick.AddListener(delegate { OnNodeSelected(currentChoiceIndex); });
        }

    }
}