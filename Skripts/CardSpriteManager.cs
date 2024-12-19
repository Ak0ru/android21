using System.Collections.Generic;
using UnityEngine;

public class CardSpriteManager : MonoBehaviour
{
    public Sprite[] sprites; // Все спрайты карт, добавленные в Inspector
    private Dictionary<string, Sprite> cardSprites = new Dictionary<string, Sprite>();

    void Awake()
    {
        // Убедись, что количество спрайтов совпадает с количеством карт
        string[] cardNames = {
            "2c", "2d", "2h", "2s", "3c", "3d", "3h", "3s",
            "4c", "4d", "4h", "4s", "5c", "5d", "5h", "5s",
            "6c", "6d", "6h", "6s", "7c", "7d", "7h", "7s",
            "8c", "8d", "8h", "8s", "9c", "9d", "9h", "9s",
            "10c", "10d", "10h", "10s", "ac", "ad", "ah", "as",
            "jc", "jd", "jh", "js", "kc", "kd", "kh", "ks",
            "qc", "qd", "qh", "qs"
        };

        if (sprites.Length != cardNames.Length)
        {
            Debug.LogError("Number of sprites does not match the number of card names.");
            return;
        }

        // Связываем каждую карту с её спрайтом
        for (int i = 0; i < cardNames.Length; i++)
        {
            cardSprites[cardNames[i]] = sprites[i];
        }
    }

    public Sprite GetCardSprite(string cardName)
    {
        if (cardSprites.TryGetValue(cardName, out Sprite sprite))
        {
            Debug.Log($"Found sprite for {cardName}");
            return sprite;
        }
        Debug.LogError($"Card sprite for {cardName} not found.");
        return null;
    }
}
