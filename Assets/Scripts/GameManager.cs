using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public CardScript[] cards;
    public string[]     matchTextsArray;

    //use to store which cards user clicked
    private int firstCardId = -1;
    private int secondCardId = -1;
    private bool isWrongMatchShowing;

    [SerializeField]
    private float MaxWrongMatchShowTime = 1.0f;
    private float wrongMatchDelayTimer = 0;

    [SerializeField]
    private Button RestartButton;

    private int numOfMatches;

    void Start () {
        InitCards();

        InitGame();
	}

    private void InitCards()
    {
        int len = cards.Length;
        for (int i = 0; i < len; i++)
        {
            cards[i].CardId = i;
            cards[i].SetGameManager(this);
        }
    }

    private void InitGame()
    {
        numOfMatches = 0;

        RestartButton.gameObject.SetActive(false);
        ShuffleItemArray();
    }

    private void ShuffleItemArray()
    {
        int len = matchTextsArray.Length;
        for (int i = 0; i < len-1; i++)
        {
            //choose and swap the item from other items
            int randId = Random.Range(i, len - 1);

            string temp = matchTextsArray[i];
            matchTextsArray[i] = matchTextsArray[randId];
            matchTextsArray[randId] = temp;
        }
    }

    void Update () {
        if (isWrongMatchShowing)
        {
            wrongMatchDelayTimer -= Time.deltaTime;
            if (wrongMatchDelayTimer < 0)
            {
                isWrongMatchShowing = false;

                //Reset the cards
                cards[firstCardId].ResetText();
                cards[secondCardId].ResetText();

                firstCardId = -1;
                secondCardId = -1;
            }
        }
	}

    public void OnCardClicked(int cardId)
    {
        if (isWrongMatchShowing)
        {
            return;
        }

        Debug.Log("User Clicked Card ID = " + cardId);

        if (firstCardId < 0)
        {
            firstCardId = cardId;   //First Card Clicked
            cards[cardId].SetText(matchTextsArray[cardId]);
        }
        else
        {
            secondCardId = cardId;
            cards[cardId].SetText(matchTextsArray[cardId]);

            if (matchTextsArray[firstCardId] == matchTextsArray[secondCardId])
            {
                Debug.Log("Cards Correct Match");
                cards[firstCardId].SetEnable(false);
                cards[secondCardId].SetEnable(false);

                firstCardId = -1;
                secondCardId = -1;

                numOfMatches += 1;
                if(numOfMatches >= cards.Length / 2)
                {
                    RestartButton.gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.Log("Cards Wrong Match");
                ResetWrongMatchAfterDelay();
            }
        }
    }

    private void ResetWrongMatchAfterDelay()
    {
        isWrongMatchShowing = true;
        wrongMatchDelayTimer = MaxWrongMatchShowTime;
    }

    public void RestartGame()
    {
        firstCardId = -1;
        secondCardId = -1;

        int len = cards.Length;
        for (int i = 0; i < len; i++)
        {
            cards[i].SetEnable(true);
            cards[i].ResetText();
        }

        InitGame();
    }

}
