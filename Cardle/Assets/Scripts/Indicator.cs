using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    private Color32 whiteColor = new Color32(255, 255, 255, 255);
    private Color32 grayColor = new Color32(100, 100, 100, 255);
    private Color32 greenColor = new Color32(90, 255, 115, 255);
    private Color32 redColor = new Color32(255, 71, 71, 255);


    public bool submitted;
    public bool skipped;
    public bool correct;
    public bool wrong;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().color = whiteColor;
        submitted = false;
        skipped = false;
        correct = false;
        wrong = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!submitted)
        {
            if (skipped)
            {
                Submission(grayColor);
            }
            else if (correct)
            {
                Submission(greenColor);
            }
            else if (wrong)
            {
                Submission(redColor);
            }
        }
    }

    void Submission(Color32 changeColor)
    {
        gameObject.GetComponent<Image>().color = changeColor;
        Debug.Log(gameObject.GetComponent<Image>().color);
        submitted = true;
    }
}
