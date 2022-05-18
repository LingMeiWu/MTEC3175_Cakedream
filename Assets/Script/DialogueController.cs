using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DialogueObject;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour
{

    [SerializeField] TextAsset twineText;
    Dialogue curDialogue;
    Node curNode;

    public bool jarM = false;
    public bool pearl = false;
    public bool sand = false;
    public bool banana = false;


    public delegate void NodeEnteredHandler(Node node);
    public event NodeEnteredHandler onEnteredNode;

    public Node GetCurrentNode()
    {
        return curNode;
    }

    public void InitializeDialogue()
    {
        curDialogue = new Dialogue(twineText);
        curNode = curDialogue.GetStartNode();
        onEnteredNode(curNode);
    }

    public List<Response> GetCurrentResponses()
    {
        return curNode.responses;
    }

    public void ChooseResponse(int responseIndex)
    {
        string nextNodeID = curNode.responses[responseIndex].destinationNode;
        Node nextNode = null;
        if (nextNodeID == "Start baking!" && (!jarM || !pearl || !sand || !banana))
        {
            nextNode = curDialogue.GetNode("Bad ending");

        }
        else if (nextNodeID == "Take the pouch")
        {
            pearl = true;
            nextNode = curDialogue.GetNode(nextNodeID);
        }
        else if (nextNodeID == "Sit down and take a break" || nextNodeID == "Exit the room")
        {
            jarM = true;
            nextNode = curDialogue.GetNode(nextNodeID);
        }
        else if (nextNodeID == "correct answer")
        {
            sand = true;
            nextNode = curDialogue.GetNode(nextNodeID);
        }
        else if (nextNodeID == "Do not approach the monkeys")
        {
            banana = true;
            nextNode = curDialogue.GetNode(nextNodeID);
        }
        else if (nextNodeID == "have you played the game yet?")
        {
            SceneManager.LoadScene("StartMenu");
        }


        else
        {
            nextNode = curDialogue.GetNode(nextNodeID);
        }

        curNode = nextNode;
        onEnteredNode(nextNode);
    }
}