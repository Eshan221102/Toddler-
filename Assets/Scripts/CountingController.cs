using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountingController : MonoBehaviour
{
    public GameObject[] objects;
    public TMP_InputField inputField;
    public TextMeshProUGUI feedbackText;

    private int currentRound = 1;
    private int objectsInCurrentRound;
    private int[] usedNumbers;
    private int maxObjects = 10;
    private bool roundInProgress = false;

    void Start()
    {
        usedNumbers = new int[5]; // Initialize an array to store used numbers for each round
        NextRound();
    }

    public void CheckAnswer()
    {
        if (roundInProgress) return;

        int userAnswer;
        if (int.TryParse(inputField.text, out userAnswer))
        {
            if (userAnswer == objectsInCurrentRound)
            {
                feedbackText.text = "Correct!";
                StartCoroutine(NextRoundAfterDelay(1.5f));
            }
            else
            {
                feedbackText.text = "Wrong Answer. Correct answer is " + objectsInCurrentRound + " Restarting Now";
                StartCoroutine(RestartGameAfterDelay(3.5f));
            }
        }
    }

    void NextRound()
{
    if (currentRound > 5)
    {
        feedbackText.text = "Game Over!";
        return;
    }

    feedbackText.text = "Round " + currentRound;

    // Check if currentRound is within the bounds of the array
    if (currentRound > 0 && currentRound <= usedNumbers.Length)
    {
        int randomNumber;
        do
        {
            randomNumber = Random.Range(1, maxObjects + 1);
        } while (IsNumberUsed(randomNumber));

        objectsInCurrentRound = randomNumber;

        // Make sure currentRound - 1 is a valid index
        if (currentRound - 1 >= 0 && currentRound - 1 < usedNumbers.Length)
        {
            usedNumbers[currentRound - 1] = objectsInCurrentRound;
        }
        else
        {
            // Handle the out-of-bounds case (optional)
            Debug.LogError("Invalid index for usedNumbers array");
        }

        ActivateRandomObjects(objectsInCurrentRound);
        inputField.text = "";

        // // Increment currentRound for the next round
        // currentRound++;
    }
    else
    {
        // Handle the out-of-bounds case (optional)
        Debug.LogError("Invalid value for currentRound");
    }
}


    bool IsNumberUsed(int number)
    {
        for (int i = 0; i < usedNumbers.Length; i++)
        {
            if (usedNumbers[i] == number)
            {
                return true;
            }
        }
        return false;
    }

    void ActivateRandomObjects(int num)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i < num);
        }
    }

    IEnumerator NextRoundAfterDelay(float delay)
    {
        roundInProgress = true;
        yield return new WaitForSeconds(delay);
        currentRound++;
        NextRound();
        roundInProgress = false;
    }

    IEnumerator RestartGameAfterDelay(float delay)
    {
        roundInProgress = true;
        yield return new WaitForSeconds(delay);
        currentRound = 1;
        usedNumbers = new int[4];
        NextRound();
        roundInProgress = false;
    }
}
