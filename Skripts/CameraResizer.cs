using UnityEngine;

public class CameraResizer : MonoBehaviour
{
    void Start()
    {
        // ����������� ������ ������
        float aspectRatio = (float)Screen.width / (float)Screen.height;

        // ��������� ������� ��������������� ������
        if (aspectRatio >= 1.6f) // ����������� ������� � 16:9
        {
            Camera.main.orthographicSize = 5; // ������� ������ ��� ������� �������
        }
        else // ����������� ����� ������������ (��������, 4:3 ��� 9:16)
        {
            Camera.main.orthographicSize = 6; // ����������� ������ ��� ������������ �������
        }
    }
}
