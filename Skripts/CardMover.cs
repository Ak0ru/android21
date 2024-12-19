using UnityEngine;

public class CardMover : MonoBehaviour
{
    public Transform playerHand;   // ������� ��� ���� ������
    public Transform dealerHand;   // ������� ��� ���� ������

    // ���������� ����� � ���� ������
    public void MoveCardToPlayer(GameObject card)
    {
        if (playerHand == null)
        {
            Debug.LogError("PlayerHand is not assigned in CardMover.");
            return;
        }

        card.transform.SetParent(playerHand, false); // ������������� ��������
        card.transform.localPosition = Vector3.zero; // ���������� ����� ������������ PlayerHand
    }

    // ���������� ����� � ���� ������
    public void MoveCardToDealer(GameObject card)
    {
        if (dealerHand == null)
        {
            Debug.LogError("DealerHand is not assigned in CardMover.");
            return;
        }

        card.transform.SetParent(dealerHand, false); // ������������� ��������
        card.transform.localPosition = Vector3.zero; // ���������� ����� ������������ DealerHand
    }
}
