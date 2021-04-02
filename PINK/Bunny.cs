using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//=====================================================================================
// This script is the main hub that controls the AI's emotions and reactions
//=====================================================================================

public class Bunny : MonoBehaviour
{
    private  Neuron mood;
    private  Neuron history;
    public  Neuron action;
    private  Neuron feeling;
    private  Neuron perception;
    public  Neuron response;

    public static int spriteInt = 3;

    private bool posOutcomeForAI;

    private static float allOutcomes = 0.0f;
    private static float mostRecentOutcome = 0.0f;
    public static float numOfActions = 0f;

    private static float moodWeighting = 0.3f;
    private static float historyWeighting = 0.7f;
    public static float feelingWeighting = 0.5f;
    public static float perceptionWeighting = 0.5f;
    public static float responseWeighting = 1.0f;

    private static bool allowable;

    private static float currentHistoryM = 0.0f;

    public Bunny bunny;

    public static int responseSelector;
    public static float responseSelectorF;
    public static string responsePrint;

    private static float moodWeightingCalc = 0.3f;
    private static float historyWeightingCalc = 0.7f;
    public static float feelingWeightingCalc = 0.5f;
    public static float perceptionWeightingCalc = 0.5f;
    public static float actionWeightingCalc = 0.3f;

    private int selector;
    private int selectorUpOrD;


    // Start is called before the first frame update
    void Start()
    {
        response = gameObject.AddComponent<Neuron>();
        mood = gameObject.AddComponent<Neuron>();
        history = gameObject.AddComponent<Neuron>();
        feeling = gameObject.AddComponent<Neuron>();
        perception = gameObject.AddComponent<Neuron>();
        action = gameObject.AddComponent<Neuron>();


        response.setValues1(0.0f, 1.0f);
        behaviourSprite();

    }

    // Update is called once per frame
    void Update()
    {
    }

    public  void behaviourSprite()
    {

        if (response.getResult() >= 0.5f)
        {
            spriteInt = 1;
            Debug.Log("AI is very happy");
        }
        else if (response.getResult() > 0.0f)
        {
            spriteInt = 2;
            Debug.Log("AI is happy");
        }
        else if (response.getResult() <= 0.0f)
        {
            spriteInt = 3;
            Debug.Log("AI is sad");
        }
        else if (response.getResult() < -0.5f)
        {
            spriteInt = 4;
            Debug.Log("AI is very sad");
        }
    }

    public bool reward()
    {
        if (posOutcomeForAI)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public void remember()
    {
        if (reward())
        {
            Debug.Log("reward");
            actionWeightingCalc += DoAction.actionWeighting;
            DoAction.actionWeighting = (actionWeightingCalc / (numOfActions+1));
            //if the AI has responded in a way that benefits it, the behaviour is reinforced

            moodWeightingCalc += moodWeighting;
            moodWeighting = (moodWeightingCalc / (numOfActions+1));

            historyWeightingCalc += historyWeighting;
            historyWeighting = (historyWeightingCalc / (numOfActions+1));

            feelingWeightingCalc += feelingWeighting;
            feelingWeighting = (feelingWeightingCalc / (numOfActions+1));

            perceptionWeightingCalc += perceptionWeighting;
            perceptionWeighting = (perceptionWeightingCalc / (numOfActions +1));
        }
        else
        {
            Debug.Log("punishment");
            var random = new System.Random();

            selector = random.Next(0, 3);
            selectorUpOrD = random.Next(-2, 2);
            selectorUpOrD = selectorUpOrD / 10;


            switch (selector)
            {
                case 1:
                    if ((DoAction.actionWeighting += selectorUpOrD) <= 0 || (DoAction.actionWeighting += selectorUpOrD) >= 0.7)
                    {
                        selectorUpOrD = -selectorUpOrD;
                    }
                    DoAction.actionWeighting += selectorUpOrD;
                    moodWeighting += selectorUpOrD;
                    historyWeighting -= selectorUpOrD;
                    break;
                    //if the AI has responded in a way that results in a negative outcome for itself it, the behaviour is altered by a set increment, either up or down.

                case 2:
                    perceptionWeighting += selectorUpOrD;
                    feelingWeighting -= selectorUpOrD;
                    break;
            }
        }
    }

    public void wasOutcomePositive()
    {
        if (DoAction.currentActionM >= 0 && allowed())
        {
            posOutcomeForAI = true;
            //if the player was going to do something positive and the AI allows it then the outcome is positive.
        }
        else if (DoAction.currentActionM >= 0)
        {
            posOutcomeForAI = false;
            //if the player was going to do something positive and the AI doesn't allow it then the outcome is negative.
        }
        else if (DoAction.currentActionM < 0 && allowed())
        {
            posOutcomeForAI = false;
            //if the player was going to do something negative and the AI allows it then the outcome is negative.
        }
        else if (DoAction.currentActionM < 0)
        {
            posOutcomeForAI = true;
            //if the player was going to do something negative and the AI doesn't allow it then the outcome is positive.
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
        //this is where the AI passes the information it has through its neurons to calculate a result
        historyCalculation();
        Debug.Log("action= " + action.getResult());
        mood.setValues1(response.getResult(), moodWeighting);
        Debug.Log("mood= " + mood.getResult());
        history.setValues1(currentHistoryM, historyWeighting);
        Debug.Log("history= " + history.getResult());
        feeling.setValues2(mood.getResult(), history.getResult(), feelingWeighting);
        Debug.Log("feeling= " + feeling.getResult());
        perception.setValues2(history.getResult(), action.getResult(), perceptionWeighting);
        Debug.Log("perception= " + perception.getResult());
        response.setValues2(feeling.getResult(), perception.getResult(), responseWeighting);
        Debug.Log("response= " + response.getResult());
        responseSelectorF = response.getResult() * 10;
        Debug.Log("unrounded selection= " + responseSelectorF);
        responseSelector = (int)Mathf.Round(responseSelectorF);
        Debug.Log("rounded selection= " + responseSelector);
        numOfActions++;
        mostRecentOutcome = DoAction.currentActionM;
    }

    private static void historyCalculation()
    {
        if (numOfActions > 0)
        {
            allOutcomes += mostRecentOutcome;
            currentHistoryM = allOutcomes / numOfActions;
            Debug.Log("current history= " + currentHistoryM);
            //history is averaged out by outcome
        }
        else
        {
            currentHistoryM = 0;
        }
    }

    public void processAction(float currentActionM, float actionWeighting)
    {
        action.setValues1(currentActionM, actionWeighting);
        Debug.Log("action= " + action.getResult());


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
