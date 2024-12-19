using UnityEngine;

public class ResponsiveUI : MonoBehaviour
{
    public RectTransform buttonRect;

    void Start()
    {
        // ������� ������ ������������ ������� ������
        float width = Screen.width * 0.3f;  // 30% ������ ������
        float height = Screen.height * 0.1f; // 10% ������ ������

        // ��������� �������
        buttonRect.sizeDelta = new Vector2(width, height);

        // ������������� ������� (��������, �����)
        buttonRect.anchoredPosition = new Vector2(0, -Screen.height * 0.2f); // �������� ���� �� 20% ������
    }
}
