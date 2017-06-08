using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour {
    private Text textComponent;
    private GameManager gameManager;
    private int cardId;

    public int CardId
    {
        get
        {
            return cardId;
        }

        set
        {
            cardId = value;
        }
    }

    void Start () {
        textComponent = gameObject.GetComponentInChildren<Text>();
    }

    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }

    public void OnClicked()
    {
        gameManager.OnCardClicked(cardId);
    }

    public void SetEnable(bool enable)
    {
        gameObject.GetComponent<Button>().interactable = enable;
    }

    public void SetText(string text)
    {
        textComponent.text = text;
    }

    public void ResetText()
    {
        textComponent.text = "?";
    }
}
