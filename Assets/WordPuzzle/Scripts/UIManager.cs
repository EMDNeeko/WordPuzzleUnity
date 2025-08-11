using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Elements")]
    [SerializeField] private CanvasGroup menuCG;
    [SerializeField] private CanvasGroup gameCG;
    [SerializeField] private CanvasGroup levelCompleteCG;
    [SerializeField] private CanvasGroup gameOverCG;

    [Header("Menu Element")]
    [SerializeField] private TextMeshProUGUI menuCoins;
    [SerializeField] private TextMeshProUGUI menuBestScore;

    [Header("Level Complete Element")]
    [SerializeField] private TextMeshProUGUI levelCompleteCoins;
    [SerializeField] private TextMeshProUGUI levelCompleteScore;
    [SerializeField] private TextMeshProUGUI levelCompleteBestScore;
    [SerializeField] private TextMeshProUGUI levelCompleteSecretWord;

    [Header("Game Element")]
    [SerializeField] private TextMeshProUGUI gameCoins;
    [SerializeField] private TextMeshProUGUI gameScore;

    [Header("Game Over Element")]
    [SerializeField] private TextMeshProUGUI gameOverCoins;
    [SerializeField] private TextMeshProUGUI gameOverBestScore;
    [SerializeField] private TextMeshProUGUI gameOverSecretWord;

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
        ShowMenu();
        HideGame();
        HideLevelComplete();
        HideGameOver();

        GameManager.onGameStateChanged += GameStateChangedCallback;
        DataManager.onCoinsUpdated += UpdateCoinsText;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
        DataManager.onCoinsUpdated -= UpdateCoinsText;
    }

    public void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                ShowMenu();
                HideGame();
                HideGameOver();
                HideLevelComplete();
                break;
            case GameState.Game:
                ShowGame();
                HideMenu();
                HideLevelComplete();
                HideGameOver();
                break;
            case GameState.LevelComplete:
                ShowLevelComplete();
                HideGame();
                break;
            case GameState.Gameover:
                ShowGameOver();
                HideGame();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCoinsText()
    {
        menuCoins.text = DataManager.instance.GetCoins().ToString();
        gameCoins.text = menuCoins.text;
        levelCompleteCoins.text = menuCoins.text;
        gameOverCoins.text = menuCoins.text;
    }

    private void ShowMenu()
    {
        menuCoins.text = DataManager.instance.GetCoins().ToString();
        menuBestScore.text = DataManager.instance.GetBestScore().ToString();
        ShowCG(menuCG);
    }
    private void HideMenu()
    {
        HideCG(menuCG);
    }
    private void ShowGame()
    {
        gameCoins.text = DataManager.instance.GetCoins().ToString();
        gameScore.text = DataManager.instance.GetScore().ToString();
        ShowCG(gameCG);
    }
    private void HideGame()
    {
        HideCG(gameCG);
    }
    private void ShowLevelComplete()
    {
        levelCompleteCoins.text = DataManager.instance.GetCoins().ToString();
        levelCompleteSecretWord.text = WordManager.instance.GetSecretWord();
        levelCompleteScore.text = DataManager.instance.GetScore().ToString();
        levelCompleteBestScore.text = DataManager.instance.GetBestScore().ToString();
        ShowCG(levelCompleteCG);
    }
    private void HideLevelComplete()
    {
        HideCG(levelCompleteCG);
    }
    private void ShowGameOver()
    {
        gameOverCoins.text = DataManager.instance.GetCoins().ToString();
        gameOverBestScore.text = DataManager.instance.GetBestScore().ToString();
        gameOverSecretWord.text = WordManager.instance.GetSecretWord();

        ShowCG(gameOverCG);
    }
    private void HideGameOver()
    {
        HideCG(gameOverCG);
    }
    private void ShowCG(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    private void HideCG(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }


}
