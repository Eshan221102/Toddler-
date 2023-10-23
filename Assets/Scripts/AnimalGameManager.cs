using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalGameManager : MonoBehaviour
{
    public AudioSource[] AnimalSounds;

    private bool[] hasSoundPlayed;

    public AnimalTurn[] Animals;

    private int activeAnimalIndex = -1; // Stores the index of the currently active animal

    void Start()
    {
        // Initialize the array to keep track of played sounds
        hasSoundPlayed = new bool[AnimalSounds.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayRandomUnplayedSound();
        }
    }

    void PlayRandomUnplayedSound()
    {
        List<int> unplayedIndices = new List<int>();

        // Find indices of unplayed sounds
        for (int i = 0; i < AnimalSounds.Length; i++)
        {
            if (!hasSoundPlayed[i])
            {
                unplayedIndices.Add(i);
            }
        }

        // If there are unplayed sounds, select a random one and play it
        if (unplayedIndices.Count > 0)
        {
            int randomIndex = unplayedIndices[Random.Range(0, unplayedIndices.Count)];
            AnimalSounds[randomIndex].Play();

            // Deactivate the previous active animal
            if (activeAnimalIndex >= 0)
            {
                Animals[activeAnimalIndex].MyTurn = false;
            }

            // Activate the new active animal
            Animals[randomIndex].MyTurn = true;

            activeAnimalIndex = randomIndex; // Update the active animal index
            hasSoundPlayed[randomIndex] = true;

            Debug.Log("Playing sound for: " + Animals[randomIndex].MyTurn);
        }
        else
        {
            Debug.Log("All sounds have been played.");
        }
    }
    void OnMouseDown()
    {
        if (AnimalTurn.instance.MyTurn)
        {
            Debug.Log("true");
        }
        else
        {
            Debug.Log("false");
        }
        Debug.Log("lalal");
    }

}

