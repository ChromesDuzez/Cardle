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
    private Color32 redColor = new Color32(255, 71, 71, 255);

    public Image[] InGameIndicators;
    public Image[] GameOverScreenIndicators;
    public Image FinalImage;
    public TextMeshProUGUI finalMessage;
    public static int numTries = 6;
    private static GameManager gmScript;

    // Awake is called before the first frame update but after start and everytime the script becomes active
    void Awake()
    {
        gmScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        for(int i = 0; i < InGameIndicators.Length; i++)
        {
            GameOverScreenIndicators[i].GetComponent<Image>().color = InGameIndicators[i].GetComponent<Image>().color;
            if(InGameIndicators[i].GetComponent<Image>().color == greenColor)
            {
                numTries = i + 1;
            }
        }

        if(numTries == 1)
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


        FinalImage.sprite = gmScript.FinalImages[gmScript.answerImageIndex];
    }
}
