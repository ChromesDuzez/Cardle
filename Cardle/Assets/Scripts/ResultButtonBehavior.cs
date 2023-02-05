using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultButtonBehavior : MonoBehaviour
{
    public Text buttonText;
    public InputField inputField;
    public BestMatchSearch searchScript;

    // Start is called before the first frame update
    void Start()
    {
        buttonText = gameObject.GetComponentInChildren<Text>();
        inputField = GameObject.FindGameObjectWithTag("InputField").GetComponent<InputField>();
        searchScript = GameObject.FindGameObjectWithTag("InputField").GetComponent<BestMatchSearch>();
    }

    //update the input field with the result stored in the button
    public void updateTextElement()
    {
        BestMatchSearch.ignoreUpdate = true;
        inputField.text = buttonText.text;
        searchScript.startSearch();
    }
}
