using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//=====================================================================================
// This script is the main hub that controls the AI's reactions
//=====================================================================================

public class Bunny : MonoBehaviour
{
    

    public static int spriteInt = 3;

    public static float numOfActions = 1f;


    private static bool allowable;

    private static float currentHistoryM = 0.0f;

    public Bunny bunny;

    public static int responseSelector;
    public static float responseSelectorF;
    public static string responsePrint;

    // Start is called before the first frame update
    void Start()
    {

        behaviourSprite();

    }

    // Update is called once per frame
    void Update()
    {
    }

    public  void behaviourSprite()
    {

        if (DoAction.currentActionM >= 0.5f)
        {
            spriteInt = 1;
            Debug.Log("AI is very happy");
        }
        else if (DoAction.currentActionM > 0.0f)
        {
            spriteInt = 2;
            Debug.Log("AI is happy");
        }
        else if (DoAction.currentActionM <= 0.0f)
        {
            spriteInt = 3;
            Debug.Log("AI is sad");
        }
        else if (DoAction.currentActionM < -0.5f)
        {
            spriteInt = 4;
            Debug.Log("AI is very sad");
        }
    }

    


    public bool allowed()
    {
        // some activities are only allowed based on the amount of trust built up between AI and the user
        if (DoAction.currentAction == "feed")
        {
            if (currentHistoryM > 0.4)
            {
                allowable = true;
            }
            else
            {
                allowable = false;
            }
        }
        else if (DoAction.currentAction == "clean")
        {
            if (currentHistoryM > 0.3)
            {
                allowable = true;
            }
            else
            {
                allowable = false;
            }
        }
        else if (DoAction.currentAction == "pet")
        {
            if (currentHistoryM > 0.2)
            {
                allowable = true;
            }
            else
            {
                allowable = false;
            }
        }
        else if (DoAction.currentAction == "kick")
        {
            if (currentHistoryM > 0.4)
            {
                allowable = true;
            }
            else
            {
                allowable = false;
            }
        }
        else
        {
            allowable = true;
        }

        return allowable;
    }

    public  void calculateOutput()
    {
        //this is where the AI's response is determined based on the action performed and past experience
        
        Debug.Log(DoAction.currentActionM);
        DoAction.allActionM += DoAction.currentActionM;
        responseSelectorF = (DoAction.allActionM/numOfActions);
        currentHistoryM = responseSelectorF;
        responseSelectorF = (responseSelectorF * 10);
        Debug.Log("unrounded selection= " + responseSelectorF);
        responseSelector = (int)Mathf.Round(responseSelectorF);
        Debug.Log("rounded selection= " + responseSelector);
        numOfActions++;
    }

   


    //DISCLAIMER: No real animals were harmed in the making of this code.
    public String responseOptions()
    {
        switch (responseSelector)
        {
            case -10:
                responsePrint= "The bunny has been deeply hurt by your actions, he has hopped away and no longer wishes to associate with you." +
                    "\r\n While coding this I have become rather fond of the unnamed bunny. If you are receiving this message I am sending you my hatred from afar. I hope your coffee is lukewarm tomorrow morning.";
                break;

            case -9:
                responsePrint = "The bunny's tears have created a puddle on the floor. Why do we live just to suffer?";
                break;

            case -8:
                responsePrint = "The bunny is backing away even furthur, it looks like he's scared of you.";
                break;

            case -7:
                responsePrint = "The bunny has tears in his eyes and is slowly backing away.";
                break;

            case -6:
                responsePrint = "The bunny yelps as if he is in pain.";
                break;

            case -5:
                responsePrint = "The bunny frowns and turns his head away from you.";
                break;

            case -4:
                responsePrint = "The bunny's ears flop forward, sadly.";
                break;

            case -3:
                responsePrint = "The bunny is scrunching up his nose in annoyance.";
                break;

            case -2:
                responsePrint = "The bunny sticks out his tongue. For some reason, you get the feeling he's mocking you.";
                break;

            case -1:
                responsePrint = "The Bunny sneezes.";
                break;

            case 0:
                responsePrint = "The bunny looks forward blankly. He really couldn't care less about you.";
                break;

            case 1:
                responsePrint = "The bunny's little shoulders twitch up, it almost looks like a shrug.";
                break;

            case 2:
                responsePrint = "The bunny thinks you're a little weird.";
                break;

            case 3:
                responsePrint = "The bunny looks cautious, but interested in you.";
                break;

            case 4:
                responsePrint = "The edges of the bunny's mouth twitch up in a smile.";
                break;

            case 5:
                responsePrint = "The bunny slowly shuffles a little closer to you.";
                break;

            case 6:
                responsePrint = "Some of the bunny's caution is gone now, he hops a little closer.";
                break;

            case 7:
                responsePrint = "The bunny mews affectionately.";
                break;

            case 8:
                responsePrint = "The bunny comes up to you and sniffes you curiously.";
                break;

            case 9:
                responsePrint = "The bunny brushes up against you affectionately.";
                break;

            case 10:
                responsePrint = "The bunny kisses your hand and freely cuddles up next to you. You have won his love and devotion.";
                break;

            default:
                responsePrint = "You are getting a response value not within the predicted range: Unfortunately the student who coded this must not be very smart if you're getting this error.";
                throw new ArgumentException("You are getting a response value not within the predicted range: Unfortunately the student who coded this must not be very smart if you're getting this error.");
        }
        return responsePrint;
    }

}
