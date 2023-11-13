using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        PlayNextAnimalSound();
        scoreText.text = "Starting The Game! Good Luck";
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
                    }
                    else
                    {
                        // Restart the scene if the wrong animal is clicked
                        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
                    }

                    PlayNextAnimalSound();
                }
            }
        }
    }

    void PlayNextAnimalSound()
    {
        // Check if there are more animals to play sounds
        if (currentAnimalIndex < animals.Length - 1)
        {
            currentAnimalIndex++;
            audioSource.PlayOneShot(animalSounds[randomizedIndices[currentAnimalIndex]]);
            attemptMade = false;
        }
        else
        {
            // Game over
            Debug.Log("Game Over");
            // You can add additional logic here for game over, such as displaying a game over screen or resetting the game.
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
}
