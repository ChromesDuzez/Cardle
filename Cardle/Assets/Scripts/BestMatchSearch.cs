using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestMatchSearch : MonoBehaviour
{
    public TextAsset CarListCSV;
    public static bool ignoreUpdate = false;
    public static List<string> CarList = new List<string>();
    public List<string> carlistvisual;
    public Text searchInput;
    public List<int> searchResults = new List<int>();
    public List<string> sortedSearchResults = new List<string>();
    public GameObject resultPrefab;
    public Transform ParentObject;
    public GameManager gmScript;
    public bool firstTimeSearch = true;

    // Start is called before the first frame update
    void Start()
    {
        gmScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        createListFromCSVFile();
        carlistvisual = CarList;
        searchInput = gameObject.GetComponent<InputField>().textComponent;
        ParentObject = GameObject.FindGameObjectWithTag("List").GetComponent<Transform>();
    }

    void ensureListCompletness()
    {
        foreach (Sprite img in gmScript.FinalImages)
        {
            bool found = false;
            foreach(string car in CarList)
            {
                if(img.name == car)
                {
                    found = true;
                    break;
                }
            }
            if(!found)
            {
                Debug.Log("[BestMatchSearch.cs] - Added " + img.name + " to CarList.");
                CarList.Add(img.name);
            }
        }
    }

    public void startSearch()
    {
        if(firstTimeSearch)
        {
            ensureListCompletness();
            firstTimeSearch = false;
        }

        //wipe out any current results before instantiating new ones
        destroyOldResults();

        if (ignoreUpdate)
        {
            ignoreUpdate = false;
        }
        else
        {
            searchResults.Clear();
            sortedSearchResults.Clear();

            char[] separators = new char[] { ' ', '.' };
        
            foreach (string car in CarList)
            {
                List<int> hits = new List<int>();
                foreach (string word in searchInput.text.Split(separators, StringSplitOptions.RemoveEmptyEntries))
                {
                    foreach(int i in SearchString(car, word))
                    {
                        hits.Add(i);
                    }
                }

                if(hits.Count == 0)
                {
                    searchResults.Add(-1);
                }
                else
                {
                    int value = 0;
                    foreach (int i in hits)
                    {
                        value += i;
                    }
                    searchResults.Add(value);
                }
            }

            sortSearchResults();
            displayResults();
        }
    }

    void displayResults()
    {
        if(sortedSearchResults.Count != 1)
        {
            foreach (string result in sortedSearchResults)
            {
                GameObject newResult = Instantiate(resultPrefab, ParentObject);
                newResult.GetComponentInChildren<Text>().text = result;
            }
        }
    }

    void destroyOldResults()
    {
        foreach (GameObject result in GameObject.FindGameObjectsWithTag("Result"))
        {
            Destroy(result);
        }
    }

    void sortSearchResults()
    {
        bool finishedTask = false;
        while (sortedSearchResults.Count != searchResults.Count && !finishedTask)
        {
            int index = -1;
            int minValue = -256;

            for(int i = 0; i < searchResults.Count; i++)
            {
                if(searchResults[i] <= -1)
                {
                    continue;
                }
                else if(minValue <= -1)
                {
                    minValue = searchResults[i];
                    index = i;
                }
                else if(searchResults[i] < minValue)
                {
                    minValue = searchResults[i];
                    index = i;
                }
            }

            if(index == -1)
            {
                finishedTask = true;
            }
            else
            {
                sortedSearchResults.Add(CarList[index]);
                searchResults[index] = -1;
            }
        }
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
            Debug.Log("[BestMatchSearch.cs] - Added " + temp + " to CarList.");
            CarList.Add(temp);
        }
    }

    private static List<int> SearchString(string str, string pat)
    {
        List<int> retVal = new List<int>();
        int m = pat.Length;
        int n = str.Length;

        int[] badChar = new int[256];

        BadCharHeuristic(pat, m, ref badChar);

        int s = 0;
        while (s <= (n - m))
        {
            int j = m - 1;

            while (j >= 0 && pat[j] == str[s + j])
                --j;

            if (j < 0)
            {
                retVal.Add(s);
                s += (s + m < n) ? m - badChar[str[s + m]] : 1;
            }
            else
            {
                s += Math.Max(1, j - badChar[str[s + j]]);
            }
        }

        return retVal;
    }

    private static void BadCharHeuristic(string str, int size, ref int[] badChar)
    {
        int i;

        for (i = 0; i < 256; i++)
            badChar[i] = -1;

        for (i = 0; i < size; i++)
            badChar[(int)str[i]] = i;
    }
}
