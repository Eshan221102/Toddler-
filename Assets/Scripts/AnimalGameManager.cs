using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSoundGameController : MonoBehaviour
{
    public GameObject[] animals; // Array to hold your animal game objects
    public AudioClip[] animalSounds; // Array to hold your animal sounds
    public TextMeshProUGUI scoreText;

    private int[] randomizedIndices; // Array to hold the randomized order of animal sounds
    private int currentAnimalIndex;
    private AudioSource audioSource;
    private int score;
    bool attemptMade = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        score = 0;
        currentAnimalIndex = -1; // Initialize to -1 so that the first animal sound will start on the first click
        RandomizeAnimalSounds();
        scoreText.text = "Starting The Game! Good Luck";
        StartCoroutine(PlayNextAnimalSound(1));
    }

    void RandomizeAnimalSounds()
    {
        // Create an array of indices and shuffle it
        randomizedIndices = new int[animalSounds.Length];
        for (int i = 0; i < randomizedIndices.Length; i++)
        {
            randomizedIndices[i] = i;
        }
        for (int i = randomizedIndices.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            int temp = randomizedIndices[i];
            randomizedIndices[i] = randomizedIndices[j];
            randomizedIndices[j] = temp;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !attemptMade)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;

                // Check if the clicked object is an animal
                if (ArrayContains(animals, hitObject))
                {
                    attemptMade = true;
                    Debug.Log("hit animal");
                    // Check if the clicked animal is the correct one
                    if (hitObject == animals[randomizedIndices[currentAnimalIndex]])
                    {
                        score++;
                        scoreText.text = "Good Job!";
                        StartCoroutine(PlayNextAnimalSound(2));
                    }
                    else
                    {
                        scoreText.text = "Wrong Answer.. Try Again!";
                        StartCoroutine(LoadGameAfterDelayAnimal(2));
                    }
                }
            }
        }
    }

    IEnumerator PlayNextAnimalSound(int delay)
    {
        yield return new WaitForSeconds(delay);
        scoreText.text = "Playing Animal Sound Now!";
        if (currentAnimalIndex < animals.Length - 1)
        {
            currentAnimalIndex++;
            audioSource.PlayOneShot(animalSounds[randomizedIndices[currentAnimalIndex]]);
            attemptMade = false;
        }
        else
        {
            scoreText.text = "Well done Returning to Main Menu!";
            StartCoroutine(LoadMainMenu(2));
        }
    }

    bool ArrayContains(GameObject[] array, GameObject obj)
    {
        foreach (GameObject element in array)
        {
            if (element == obj)
            {
                return true;
            }
        }
        return false;
    }
    IEnumerator LoadGameAfterDelayAnimal(float delay)
    {
        yield return new WaitForSeconds(delay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LoadMainMenu(float delay)
    {
        yield return new WaitForSeconds(delay);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
