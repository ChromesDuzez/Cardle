using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestMatchSearch : MonoBehaviour
{
    public TextAsset CarListCSV;
    public static List<string> CarList = new List<string>();
    public List<string> carlistvisual;

    // Start is called before the first frame update
    void Start()
    {
        createListFromCSVFile();
        carlistvisual = CarList;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createListFromCSVFile()
    {
        string temp = "";
        foreach(char c in CarListCSV.text)
        {
            if(temp == "" && c == ' ')
            {
                continue;
            }
            else if(c != ',')
            {
                temp += c;
            }
            else
            {
                CarList.Add(temp);
                temp = "";
            }
        }
        if(temp != "")
        {
            CarList.Add(temp);
        }
    }
}
