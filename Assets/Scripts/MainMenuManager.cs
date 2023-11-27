using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static int ChronologicalAge = 1;
    public static int MentalAge = 1;
    public static float IQ = 0;
    public GameObject InputAgePanel;
    public TMP_InputField ageInputField;
    public TextMeshProUGUI IQText;

    void Start()
    {
        // Check if the game is launched for the first time
        if (!PlayerPrefs.HasKey("FirstLaunch"))
        {
            // If it's the first launch, activate the InputAgePanel
            InputAgePanel.SetActive(true);
        }
        else
        {
            // If not the first launch, retrieve the ChronologicalAge from PlayerPrefs
            ChronologicalAge = PlayerPrefs.GetInt("ChronologicalAge", 1);
            IQ = PlayerPrefs.GetFloat("IQ", 0); // Use GetFloat instead of GetInt for IQ
        }

        MentalAge = PlayerPrefs.GetInt("MentalAge", 1);

        // Move the IQ calculation after updating the values
        IQ = (float)MentalAge / ChronologicalAge * 100;
        IQ = Mathf.Clamp((float)MentalAge / ChronologicalAge * 100, 40f, 130f);
        PlayerPrefs.SetFloat("IQ", IQ);

        IQText.text = "IQ : " + IQ.ToString("F1");

        Debug.Log(IQ);
    }


    // Function to be called when the user submits their age
    public void SetAge()
    {
        int age = int.Parse(ageInputField.text);

        PlayerPrefs.SetInt("ChronologicalAge", age);
        ChronologicalAge = age;
        MentalAge = ChronologicalAge / 2;
        PlayerPrefs.SetInt("MentalAge", MentalAge);

        InputAgePanel.SetActive(false);
        Debug.Log("Chronological Age: " + ChronologicalAge + " Mental age: " + MentalAge);

        PlayerPrefs.SetInt("FirstLaunch", 1);

        // Ensure that the division is done using floating-point numbers
        IQ = (float)MentalAge / ChronologicalAge * 100;
        PlayerPrefs.SetFloat("IQ", IQ);
        IQText.text = "IQ: " + IQ.ToString("F1");
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
    public void ExitGame()
    {
        Application.Quit();
    }
}
