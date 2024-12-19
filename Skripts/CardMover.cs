using UnityEngine;

public class CardMover : MonoBehaviour
{
    public Transform playerHand;   // Позиция для карт игрока
    public Transform dealerHand;   // Позиция для карт дилера

    // Перемещаем карту в руку игрока
    public void MoveCardToPlayer(GameObject card)
    {
        if (playerHand == null)
        {
            Debug.LogError("PlayerHand is not assigned in CardMover.");
            return;
        }

        card.transform.SetParent(playerHand, false); // Устанавливаем родителя
        card.transform.localPosition = Vector3.zero; // Центрируем карту относительно PlayerHand
    }

    // Перемещаем карту в руку дилера
    public void MoveCardToDealer(GameObject card)
    {
        if (dealerHand == null)
        {
            Debug.LogError("DealerHand is not assigned in CardMover.");
            return;
        }

        card.transform.SetParent(dealerHand, false); // Устанавливаем родителя
        card.transform.localPosition = Vector3.zero; // Центрируем карту относительно DealerHand
    }
}
