using UnityEngine;

public class CameraResizer : MonoBehaviour
{
    void Start()
    {
        // Соотношение сторон экрана
        float aspectRatio = (float)Screen.width / (float)Screen.height;

        // Настройка размера ортографической камеры
        if (aspectRatio >= 1.6f) // Соотношение близкое к 16:9
        {
            Camera.main.orthographicSize = 5; // Базовый размер для широких экранов
        }
        else // Соотношение более вертикальное (например, 4:3 или 9:16)
        {
            Camera.main.orthographicSize = 6; // Увеличенный размер для вертикальных экранов
        }
    }
}
