using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public static bool win;

    //Image Arrays
    public Sprite[] Level1Images; // icons array
    public Sprite[] Level2Images; // icons array
    public Sprite[] Level3Images; // icons array
    public Sprite[] Level4Images; // icons array
    public Sprite[] Level5Images; // icons array
    public Sprite[] FinalImages; // icons array
    public Sprite[] CurrentImageSet;
    
    //Display Image Information Variables
    public static Image ImageDisplay;
    public int answerImageIndex;
    public string answer;

    //Status Indicator Variables
    public Image[] Indicators;
    public int currentLevelIndex;
    public GameObject gameOverPanel;
    public GameObject gamePanel;

    // Awake is called before the first frame update and everytime the script becomes active
    void Awake()
    {
        //Initialize Images
        LoadIcons();
        ImageDisplay = GameObject.FindGameObjectWithTag("ImageDisplay").GetComponent<Image>();
        answerImageIndex = Random.Range(0, Level1Images.Length);
        ImageDisplay.sprite = Level1Images[answerImageIndex];
        answer = Level1Images[answerImageIndex].name;

        if(Indicators.Length  == 0)
        {
            Debug.LogError("The indicators need to be set in the inspector.");
        }

        currentLevelIndex = 0;
        gameOver = false;
        win = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            if (Indicators[Indicators.Length - 1].GetComponent<Indicator>().submitted)
            {
                gameOver = true;
                if (Indicators[Indicators.Length - 1].GetComponent<Indicator>().correct)
                {
                    win = true;
                }
            }
            else if (currentLevelIndex > 0 && Indicators[currentLevelIndex - 1].GetComponent<Indicator>().correct)
            {
                gameOver = true;
                win = true;
            }
        }
        else
        {
            gameOverPanel.SetActive(true);
            gamePanel.SetActive(false);
        }
    }

    //if the player skips the current image
    public void skip()
    {
        if(!gameOver)
        {
            Indicators[currentLevelIndex].GetComponent<Indicator>().skipped = true;
            nextLevel();
        }
    }

    //if the player submits a guess
    public void submit()
    {
        if(!gameOver)
        {
            if(GameObject.FindGameObjectWithTag("Submission").GetComponent<Text>().text == answer)
            {
                Indicators[currentLevelIndex].GetComponent<Indicator>().correct = true;
            }
            else
            {
                Indicators[currentLevelIndex].GetComponent<Indicator>().wrong = true;
            }
            nextLevel();
        }
    }

    private void nextLevel()
    {
        currentLevelIndex++;
        switch(currentLevelIndex)
        {
            case 1:
                CurrentImageSet = Level2Images;
                break;
            case 2:
                CurrentImageSet = Level3Images;
                break;
            case 3:
                CurrentImageSet = Level4Images;
                break;
            case 4:
                CurrentImageSet = Level5Images;
                break;
            default:
                CurrentImageSet = FinalImages;
                break;
        }

        ImageDisplay.sprite = CurrentImageSet[answerImageIndex];
    }

    //populate all of the icon arrays
    void LoadIcons()
    {
        //level 1 icons
        object[] loadedIcons = Resources.LoadAll("level-1", typeof(Sprite));
        Level1Images = new Sprite[loadedIcons.Length];
        
        for (int x = 0; x < loadedIcons.Length; x++)
        {
            Level1Images[x] = (Sprite)loadedIcons[x];
        }

        //level 2 icons
        loadedIcons = Resources.LoadAll("level-2", typeof(Sprite));
        Level2Images = new Sprite[loadedIcons.Length];

        for (int x = 0; x < loadedIcons.Length; x++)
        {
            Level2Images[x] = (Sprite)loadedIcons[x];
        }

        //level 3 icons
        loadedIcons = Resources.LoadAll("level-3", typeof(Sprite));
        Level3Images = new Sprite[loadedIcons.Length];

        for (int x = 0; x < loadedIcons.Length; x++)
        {
            Level3Images[x] = (Sprite)loadedIcons[x];
        }

        //level 4 icons
        loadedIcons = Resources.LoadAll("level-4", typeof(Sprite));
        Level4Images = new Sprite[loadedIcons.Length];

        for (int x = 0; x < loadedIcons.Length; x++)
        {
            Level4Images[x] = (Sprite)loadedIcons[x];
        }

        //level 5 icons
        loadedIcons = Resources.LoadAll("level-5", typeof(Sprite));
        Level5Images = new Sprite[loadedIcons.Length];

        for (int x = 0; x < loadedIcons.Length; x++)
        {
            Level5Images[x] = (Sprite)loadedIcons[x];
        }

        //final icons
        loadedIcons = Resources.LoadAll("Final-Image", typeof(Sprite));
        FinalImages = new Sprite[loadedIcons.Length];

        for (int x = 0; x < loadedIcons.Length; x++)
        {
            FinalImages[x] = (Sprite)loadedIcons[x];
        }
    }
}
