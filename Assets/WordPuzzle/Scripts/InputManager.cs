using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [Header(" Elements ")]
    [SerializeField] private WordContainer[] wordContainers;
    [SerializeField] private Button tryButton;
    [SerializeField] private KeyboardColorizer keyboardColorizer;

    [Header(" Settings ")]
    private int currentWordContainerIndex;
    private bool canAddLetter = true;
    private bool shouldReset;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();

        KeyboardKey.onKeyPressed += KeyPressedCallBack;
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        KeyboardKey.onKeyPressed -= KeyPressedCallBack;
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Game:
                if (shouldReset)
                {
                    Initialize();
                }
                break;
            case GameState.LevelComplete:
                shouldReset = true;
                break;
            case GameState.Gameover:
                shouldReset = true;
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void Initialize()
    {
        currentWordContainerIndex = 0;
        canAddLetter = true;

        DisableTryButton();

        for (int i = 0; i < wordContainers.Length; i++)
        {
            wordContainers[i].Initialize();
        }

        shouldReset = false;
    }
    private void KeyPressedCallBack(char letter)
    {
        if (!canAddLetter)
        {
            return;
        }
        wordContainers[currentWordContainerIndex].Add(letter);
        if (wordContainers[currentWordContainerIndex].IsCompleted())
        {
            canAddLetter = false;
            EnableTryButton();
            //CheckWord();
        }
    }

    public void CheckWord()
    {
        string wordToCheck = wordContainers[currentWordContainerIndex].GetWord();
        string secretWord = WordManager.instance.GetSecretWord();

        wordContainers[currentWordContainerIndex].Colorized(secretWord);
        keyboardColorizer.Colorize(secretWord, wordToCheck);

        if (wordToCheck == secretWord)
        {
            SetLevelComplete();
        }
        else
        {
            Debug.Log("Wrong Word!");

            DisableTryButton();
            currentWordContainerIndex++;

            if (currentWordContainerIndex >= wordContainers.Length)
            {
                DataManager.instance.ResetScore();
                GameManager.instance.SetGameState(GameState.Gameover);
                Debug.Log("Gameover");
            }
            else
            {
                canAddLetter = true;
            }
        }
    }
    private void SetLevelComplete()
    {
        UpdateData();
        GameManager.instance.SetGameState(GameState.LevelComplete);
    }

    private void UpdateData()
    {
        int scoreToAdd = 6 - currentWordContainerIndex;

        DataManager.instance.IncreaseScore(scoreToAdd);
        DataManager.instance.AddCoin(scoreToAdd * 5);
    }
    public void BackspacePressedCallBack()
    {
        if (!GameManager.instance.IsGameState())
        {
            return;
        }
        bool removedLetter = wordContainers[currentWordContainerIndex].RemoveLetter();
        if (removedLetter)
        {
            DisableTryButton();
        }
        canAddLetter = true;
    }

    private void EnableTryButton()
    {
        tryButton.interactable = true;
    }
    private void DisableTryButton()
    {
        tryButton.interactable = false;
    }

    public WordContainer GetCurrentWordContainer()
    {
        return wordContainers[currentWordContainerIndex];
    }
}
