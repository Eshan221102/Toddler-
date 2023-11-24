using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static int ChronologicalAge = 0;
    public GameObject InputAgePanel;
    public TMP_InputField ageInputField;

    void Start()
    {
        // Check if the game is launched for the first time
        if (!PlayerPrefs.HasKey("FirstLaunch"))
        {
            // If it's the first launch, activate the InputAgePanel
            InputAgePanel.SetActive(true);
            PlayerPrefs.SetInt("FirstLaunch", 1); // Set a flag to indicate the first launch
        }
        else
        {
            // If not the first launch, retrieve the ChronologicalAge from PlayerPrefs
            ChronologicalAge = PlayerPrefs.GetInt("ChronologicalAge", 0);
        }
    }

    // Function to be called when the user submits their age
    public void SetAge()
    {
        // Get the age from the input field
        int age = int.Parse(ageInputField.text);

        // Store the age in PlayerPrefs
        PlayerPrefs.SetInt("ChronologicalAge", age);
        ChronologicalAge = age;

        // Deactivate the InputAgePanel
        InputAgePanel.SetActive(false);
        Debug.Log("Set");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartAnimalGame()
    {
        SceneManager.LoadScene(1);
    }
    public void StartCountingGame()
    {
        SceneManager.LoadScene(2);
    }
}
