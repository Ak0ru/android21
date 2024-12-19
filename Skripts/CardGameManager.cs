using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardGameManager : MonoBehaviour
{
    public CardSpriteManager spriteManager; // ������ �� CardSpriteManager
    public CardMover cardMover;            // ������ �� CardMover
    public GameObject cardPrefab;          // ������ �����
    public Transform deckPosition;         // ������� ������
    public Transform playerCardStartPosition; // ������� ���� ������
    public Transform dealerCardStartPosition; // ������� ���� ������

    // UI ��������
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI dealerScoreText;
    public Button hitButton;
    public Button standButton;
    public Button restartButton;

    private int playerScore = 0;
    private int dealerScore = 0;
    private bool gameOver = false;

    void Start()
    {
        SetupButtons();
        ResetGame();
    }

    void SetupButtons()
    {
        hitButton.onClick.AddListener(() => DealCardToPlayer());
        standButton.onClick.AddListener(() => DealerTurn());
        restartButton.onClick.AddListener(() => ResetGame());
    }

    // ����� ��� ������� ����� ������
    public void DealCardToPlayer()
    {
        if (gameOver)
        {
            Debug.Log("Game is over. Cannot deal card.");
            return;
        }

        if (deckPosition == null)
        {
            Debug.LogError("Deck position is not assigned. Cannot deal card.");
            return;
        }

        string cardName = GetRandomCard();
        Debug.Log($"Dealing card: {cardName} from {deckPosition.name}");

        GameObject newCard = SpawnCard(deckPosition, cardName); // ������ ����� � ������

        if (newCard != null)
        {
            cardMover.MoveCardToPlayer(newCard); // ��������� ���������� ����� � ���� ������
            playerScore += GetCardValue(cardName); // ��������� �������� ����� � ����� ������
            UpdateUI();

            // ���������, �� �������� �� �����
            if (playerScore > 21)
            {
                EndGame("Player Bust! Dealer Wins!");
            }
        }
        else
        {
            Debug.LogError("Failed to spawn card. newCard is null.");
        }
    }

    // ��� ������
    void DealerTurn()
    {
        if (gameOver) return;

        while (dealerScore < 17) // ����� ���� �����, ���� �� ������ 17 � ������
        {
            string cardName = GetRandomCard();
            Debug.Log($"Dealer drawing card: {cardName}");

            GameObject newCard = SpawnCard(deckPosition, cardName); // ����� ��������� � ������

            if (newCard != null)
            {
                cardMover.MoveCardToDealer(newCard); // ���������� ����� � ���� ������
                dealerScore += GetCardValue(cardName);
            }
            else
            {
                Debug.LogError("Failed to spawn card for dealer.");
            }
        }

        UpdateUI();

        if (dealerScore > 21 || playerScore > dealerScore)
            EndGame("Player Wins!");
        else if (playerScore == dealerScore)
            EndGame("It's a Draw!");
        else
            EndGame("Dealer Wins!");
    }

    // ����� ��� ������ �����
    GameObject SpawnCard(Transform startPosition, string cardName)
    {
        // ��������� ������� ��������� �������
        if (startPosition == null)
        {
            Debug.LogError("Start position is null in SpawnCard.");
            return null;
        }

        // ���������, �������� �� ������ �����
        if (cardPrefab == null)
        {
            Debug.LogError("Card prefab is not assigned in CardGameManager.");
            return null;
        }

        // ������ �����
        GameObject card = Instantiate(cardPrefab, startPosition.position, Quaternion.identity);
        if (card == null)
        {
            Debug.LogError("Failed to instantiate card prefab.");
            return null;
        }

        // ������������� �������� ��� �����
        card.transform.SetParent(startPosition, false);
        card.transform.localPosition = Vector3.zero;

        // ������� �������� ������ "R" (������� �����)
        Transform backSprite = card.transform.Find("R");
        if (backSprite != null)
        {
            Debug.Log("Removing back sprite object R.");
            Destroy(backSprite.gameObject);
        }

        // ��������� ������� ���������� Image
        Image cardImage = card.GetComponent<Image>();
        if (cardImage == null)
        {
            Debug.LogError("Card prefab does not have an Image component.");
            return null;
        }

        // �������� ������ ��� �����
        Sprite cardSprite = spriteManager.GetCardSprite(cardName);
        if (cardSprite == null)
        {
            Debug.LogError($"Card sprite not found for {cardName}. Using default sprite.");
            return null;
        }

        // ��������� ������ �����
        cardImage.sprite = cardSprite;

        Debug.Log($"Assigned sprite {cardSprite.name} to card {cardName}");

        return card;
    }


    // ��������� ��������� �����
    string GetRandomCard()
    {
        string[] cards = {
            "2c", "2d", "2h", "2s", "3c", "3d", "3h", "3s",
            "4c", "4d", "4h", "4s", "5c", "5d", "5h", "5s",
            "6c", "6d", "6h", "6s", "7c", "7d", "7h", "7s",
            "8c", "8d", "8h", "8s", "9c", "9d", "9h", "9s",
            "10c", "10d", "10h", "10s", "ac", "ad", "ah", "as",
            "jc", "jd", "jh", "js", "kc", "kd", "kh", "ks",
            "qc", "qd", "qh", "qs"
        };
        return cards[Random.Range(0, cards.Length)];
    }

    // ��������� �������� �����
    int GetCardValue(string cardName)
    {
        if (cardName.StartsWith("a")) return 11;
        if (cardName.StartsWith("k") || cardName.StartsWith("q") || cardName.StartsWith("j") || cardName.StartsWith("10")) return 10;
        return int.Parse(cardName.Substring(0, 1)); // ��� ���� 2-9
    }

    // ���������� UI
    void UpdateUI()
    {
        playerScoreText.text = $"Player: {playerScore}";
        dealerScoreText.text = $"Dealer: {dealerScore}";
    }

    // ���������� ����
    void EndGame(string message)
    {
        gameOver = true;
        Debug.Log(message);

        hitButton.interactable = false;
        standButton.interactable = false;
    }

    // ����� ����
    public void ResetGame()
    {
        gameOver = false;
        playerScore = 0;
        dealerScore = 0;

        UpdateUI();
        ClearCards();

        hitButton.interactable = true;
        standButton.interactable = true;
    }

    // ������� ���� ���� �� ������
    void ClearCards()
    {
        foreach (Transform child in playerCardStartPosition)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in dealerCardStartPosition)
        {
            Destroy(child.gameObject);
        }
    }
}
