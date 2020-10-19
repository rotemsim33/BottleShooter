#pragma warning disable 649
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    [SerializeField] private Text newScore;
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    private HighscoreEntry newhighscoreEntry;


    private void Awake()
    {
        entryContainer = transform.Find("HighestScoreContaner");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplat");

        entryTemplate.gameObject.SetActive(false);

        // Check if need to add the new score
        CheckToAddHighscoreEntry(StaticObjectScript.totalScore);
    

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);


        // Sort entry list by Scores
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    // Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList, newhighscoreEntry.score == highscoreEntry.score);
        }

    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList, bool addBlinkdScript)
    {
        float templateHeight = 140;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector3(0, -templateHeight * transformList.Count, 0);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        int score = highscoreEntry.score;

        entryTransform.Find("PosText").GetComponent<Text>().text = rankString;
        entryTransform.Find("ScoreText").GetComponent<Text>().text = score.ToString();
        // Set background visible odds and evens, easier to read
        entryTransform.Find("BackGround").gameObject.SetActive(rank % 2 == 1);

        // Highlight First
        if (rank == 1)
        {
            entryTransform.Find("PosText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("ScoreText").GetComponent<Text>().color = Color.green;
        }
        // Show the player where is he in the table
        if (addBlinkdScript)
        {
            entryTransform.Find("PosText").gameObject.AddComponent<BlinkText>();
            entryTransform.Find("ScoreText").gameObject.AddComponent<BlinkText>();
        }

        transformList.Add(entryTransform);
    }

    private void CheckToAddHighscoreEntry(int newScore)
    {
        bool scoreAppersInTable=false;
        // Create HighscoreEntry
        newhighscoreEntry = new HighscoreEntry { score = newScore };

        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // There is no stored table then need to initialize
        if (highscores ==null)
        {
            highscores = new Highscores()
            {
                highscoreEntryList = new List<HighscoreEntry>()
            };
        }
        

        // Check if there is another score like the new one already in table
        if (highscores.highscoreEntryList.FindIndex(highscoreEntry => highscoreEntry.score == newScore) != -1)
        {
            scoreAppersInTable = true;
        }

        if (!scoreAppersInTable)
        {
            if (highscores.highscoreEntryList.Count < 10)
            {
                // Add new entry to Highscores
                highscores.highscoreEntryList.Add(newhighscoreEntry);
            }
            else
            {
                int lowest_score = highscores.highscoreEntryList.Min(highscoreEntry => highscoreEntry.score);
                int indexlowest_score = highscores.highscoreEntryList.FindIndex(highscoreEntry => highscoreEntry.score == lowest_score);

                if (lowest_score < newScore)
                {
                    highscores.highscoreEntryList.RemoveAt(indexlowest_score);
                    highscores.highscoreEntryList.Add(newhighscoreEntry);

                }
            }

            // Save updated Highscores
            string json = JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("highscoreTable", json);
            PlayerPrefs.Save();
        }
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    
     //represents a single High score entry
    
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
    }

}


