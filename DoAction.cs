using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//=====================================================================================
// This script processes the player's input and the AI's output
//=====================================================================================
public class DoAction : MonoBehaviour
{
    public Button doAction;
    public InputField playerInput;
    public Text aIText;
    public List<String> messageHistory = new List<String>();
    public bool action;
    public static float currentActionM = 0.0f;
    public static float actionWeighting = 0.3f;
    public static String currentAction;

    public Bunny bunny;
    
    private String previousAction;
    private String previousPreviousAction;
    private bool boring;


    // Start is called before the first frame update
    void Start()
    {
        doAction.onClick.AddListener(TaskOnClick);
        bunny = gameObject.AddComponent<Bunny>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TaskOnClick();
        }
        
    }

    private bool bored()
    {
        //if the actions become repetitive it is perceived as negative.
        if (previousAction == currentAction && previousPreviousAction == currentAction)
        {
            aIText.text = aIText.text + "\r\n" + "The bunny yawns. Don't repeat yourself, it's annoying.";
            currentActionM = -0.1f;
            return true;
        }
        else
        {
            return false;
        }
    }

    void TaskOnClick()
    {
        if (playerInput)
        {
            messageHistory.Add(playerInput.text);
            aIText.text = aIText.text + "\r\n>" + playerInput.text;
            recogniseAction();

            if (action && bunny.allowed() && bored())
            {
                bunny.processAction(currentActionM, actionWeighting);
                bunny.calculateOutput();

                if (Bunny.numOfActions >= 3)
                {
                    previousPreviousAction = previousAction;
                    previousAction = currentAction;
                }
            }
            else if (action && bunny.allowed())
            {
                actionData();
                bunny.processAction(currentActionM, actionWeighting);
                bunny.calculateOutput();
                aIText.text = aIText.text + "\r\n" + bunny.responseOptions();

                if(Bunny.numOfActions >= 3)
                {
                    previousPreviousAction = previousAction;
                    previousAction = currentAction;
                }
                
                bunny.wasOutcomePositive();
                bunny.remember();
            }
            else if (playerInput.text == "c")
            {
                aIText.text = aIText.text + "\r\n-" + "feed, pet, clean, approach fast, approach slow, smile, sing, talk, kick, yell, frown, insult";
            }
            else if (action)
            {
                actionData();
                aIText.text = aIText.text + "\r\n" + "The bunny dodged away from you. Looks like he doesn't trust you enough for that yet.";
                bunny.wasOutcomePositive();
                bunny.remember();
            }
            else
            {
                aIText.text = aIText.text + "\r\n" + "The bunny looks confused. If you could read his mind, he might be thinking you should type \"c\" to see available commands.";
            }
            bunny.behaviourSprite();
            playerInput.text = "";
            playerInput.ActivateInputField();
            
        }
    }

    private void recogniseAction()
    {
        if ((playerInput.text == "c"))
        {
            currentActionM = 0.0f;
        }
        else if ((playerInput.text == "feed")|| (playerInput.text == "pet") || (playerInput.text == "clean") || (playerInput.text == "approach fast") || 
            (playerInput.text == "approach slow") || (playerInput.text == "smile") || (playerInput.text == "sing") || (playerInput.text == "talk") ||
            (playerInput.text == "kick") || (playerInput.text == "yell") || (playerInput.text == "frown") || (playerInput.text == "insult"))
        {
            action = true;
            Debug.Log("Action recognised");
            currentAction = playerInput.text;
        }
        else
        {
            action = false;
        }
    }

    private static void actionData()
    {
        switch (currentAction)
        {
            case "feed":
                currentActionM = 1.0f;
                break;

            case "pet":
                currentActionM = 0.9f;
                break;

            case "clean":
                currentActionM = 0.8f;
                break;

            case "approach fast":
                currentActionM = -0.3f;
                break;

            case "approach slow":
                currentActionM = 0.3f;
                break;

            case "smile":
                currentActionM = 0.6f;
                break;

            case "sing":
                currentActionM = 0.5f;
                break;

            case "talk":
                currentActionM = 0.7f;
                break;

            case "kick":
                currentActionM = -1.0f;
                break;

            case "yell":
                currentActionM = -0.8f;
                break;

            case "frown":
                currentActionM = -0.6f;
                break;

            case "insult":
                currentActionM = -0.7f;
                break;


        }
    }

}
