using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SyncIndicators : MonoBehaviour
{
    private Color32 whiteColor = new Color32(255, 255, 255, 255);
    private Color32 grayColor = new Color32(100, 100, 100, 255);
    private Color32 greenColor = new Color32(90, 255, 115, 255);
    private Color32 yellowColor = new Color32(255, 253, 97, 255);
    private Color32 redColor = new Color32(255, 71, 71, 255);

    public Transform indicatorPanel;
    public Image[] indicators;
    public Image FinalImage;
    public TextMeshProUGUI finalMessage;
    public static int numTries = 6;
    private static GameManager gmScript;

    void Start()
    {
        gmScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //updateIndicators();
        //updateFinalMessage();
        FinalImage.sprite = gmScript.FinalImages[gmScript.answerImageIndex];
    }

    // Awake is called before the first frame update but after start and everytime the script becomes active
    void Awake()
    {
        //FinalImage.sprite = gmScript.FinalImages[gmScript.answerImageIndex];
    }

    public void updateIndicators()
    {
        indicatorPanel.position = gameObject.transform.position;

        for(int i = 0; i < indicators.Length; i++)
        {
            if (indicators[i].GetComponent<Indicator>().correct)
            {
                numTries = i + 1;
            }
        }

        updateFinalMessage();
    }

    void updateFinalMessage()
    {
        if (numTries == 1)
        {
            finalMessage.text = "You got it first try? You're on Fire!";
        }
        else if (numTries == 2)
        {
            finalMessage.text = "You got it on the second guess. You know your cars.";
        }
        else if (numTries == 3)
        {
            finalMessage.text = "As they say, third times the charm.";
        }
        else if (numTries == 4)
        {
            finalMessage.text = "You got it fourth guess! Good Job.";
        }
        else if (numTries == 5)
        {
            finalMessage.text = "You got it! There is definetly room for improvement though.";
        }
        else
        {
            finalMessage.text = "Better Luck Next Time";
        }
    }
}


/*
         * 
         * 
        int i = 0;
        foreach(GameObject go in InGameIndicators)
        {
            if(go.GetComponent<Indicator>().correct)
            {
                GameOverScreenIndicators[i].GetComponent<Image>().color = greenColor;
                numTries = i + 1;
            }
            else if (go.GetComponent<Indicator>().wrongWithCorrectManufacturer)
            {
                GameOverScreenIndicators[i].GetComponent<Image>().color = yellowColor;
            }
            else if (go.GetComponent<Indicator>().wrong)
            {
                GameOverScreenIndicators[i].GetComponent<Image>().color = redColor;
            }
            else if (go.GetComponent<Indicator>().skipped)
            {
                GameOverScreenIndicators[i].GetComponent<Image>().color = grayColor;
            }
            else
            {
                GameOverScreenIndicators[i].GetComponent<Image>().color = whiteColor;
            }
            i++;
        }

        for (int i = 0; i < InGameIndicators.Length; i++)
        {
            GameOverScreenIndicators[i].GetComponent<Image>().color = InGameIndicators[i].GetComponent<Image>().color;
            if (InGameIndicators[i].GetComponent<Image>().color == greenColor)
            {
                numTries = i + 1;
            }
        }
        */