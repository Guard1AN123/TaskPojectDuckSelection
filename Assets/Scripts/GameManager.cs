using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoSingleton<GameManager>
{
    [field: SerializeField] public SpawnPointParent basketsSpawnPoints { get; private set; }
    [field: SerializeField] public SpawnPointParent ducksSpawnPoints { get; private set; }
    [field: SerializeField] public RectTransform ducksInitialSpawnPoint { get; private set; }
    [field: SerializeField] public RectTransform basketsParent { get; private set; }
    [field: SerializeField] public RectTransform ducksParent { get; private set; }
    [field: SerializeField] public List<Color> colorSchemes { get; private set; }

    public List<Color> selectedColors { get; private set; } = new();
    public List<Basket> generatedBaskets { get; private set; } = new();
    public List<Duck> generatedDucks { get; private set; } = new();
    public int currentRound { get; private set; } = 0;
    public int guessesToMake { get; private set; } = 0;


    private void Start()
    {
        InitializeGame();
        AudioManager.Instance.PlayAudio(SoundType.Music);
    }

    #region Initialization
    public void InitializeGame()
    {
        guessesToMake = basketsSpawnPoints.spawnPoints.Count;
        InitializeBackets();
    }
    public void InitializeBackets()
    {
        generatedBaskets.Clear();
        selectedColors = selectColors(basketsSpawnPoints.spawnPoints.Count);
        for (int i = 0; i < basketsSpawnPoints.spawnPoints.Count; i++)
        {
            var basket = Instantiate(
                ConfigsManager.Instance.basketsConfig.basket,
                basketsSpawnPoints.spawnPoints[i].position,
                Quaternion.identity,
                basketsParent
                );
            basket.InitializeBusket(selectedColors[i],i);
            generatedBaskets.Add(basket);
        }
        StartCoroutine(DelayDuckInitialization());
    }

    public List<Color> selectColors(int selectionAmount)
    {
        List<Color> selectedColors = new List<Color>();

        List<Color> colorsCopy = new List<Color>(colorSchemes);

        selectionAmount = Mathf.Min(selectionAmount, colorsCopy.Count);

        for (int i = 0; i < selectionAmount; i++)
        {
            int randomIndex = Random.Range(0, colorsCopy.Count);
            selectedColors.Add(colorsCopy[randomIndex]);
            colorsCopy.RemoveAt(randomIndex);
        }

        return selectedColors;
    }
    public IEnumerator DelayDuckInitialization()
    {
        yield return new WaitForSeconds(ConfigsManager.Instance.basketsConfig.basketAppearTime);
        AudioManager.Instance.PlayAudio(SoundType.Pop);
        InitializeDucks();
    }
    public void InitializeDucks()
    {
        generatedDucks.Clear();
        List<Basket> basketsCopy = new List<Basket>(generatedBaskets);
        for (int i = 0; i < ducksSpawnPoints.spawnPoints.Count; i++)
        {
            int randomIndex = Random.Range(0, basketsCopy.Count);
            int selectedIndex = basketsCopy[randomIndex].selectionIndex;
            var duck = Instantiate(
                ConfigsManager.Instance.ducksConfig.duck,
                ducksInitialSpawnPoint.position,
                Quaternion.identity,
                ducksParent
                );
            duck.InitializeBusket(selectedColors[selectedIndex], selectedIndex, ducksSpawnPoints.spawnPoints[i]);
            basketsCopy.RemoveAt(randomIndex);
            generatedDucks.Add(duck);

        }
        AudioManager.Instance.PlayAudio(SoundType.WaterSplash);
        HintManager.Instance.InitializeHints();
    }
    #endregion


    public void CheckPair(Duck duck,Basket basket)
    {
        if(duck.selectionIndex == basket.selectionIndex)
        {
            AudioManager.Instance.PlayAudio(SoundType.Correct);
            generatedBaskets.Remove(basket);
            generatedDucks.Remove(duck);
            Destroy(basket.gameObject);
            Destroy(duck.gameObject);
            guessesToMake--;
            if(guessesToMake == 0)
            {
                currentRound++;
                if(currentRound == ConfigsManager.Instance.gameplayConfig.roundCount)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                else
                {
                    UIManager.Instance.advanceStage(currentRound-1);
                    InitializeGame();
                }
            }
        }
        else
        {
            AudioManager.Instance.PlayAudio(SoundType.Wrong);
            duck.ReturnToInitialPos();
        }
    }
}
