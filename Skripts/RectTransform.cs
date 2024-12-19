using UnityEngine;

public class ResponsiveUI : MonoBehaviour
{
    public RectTransform buttonRect;

    void Start()
    {
        // Масштаб кнопки относительно размера экрана
        float width = Screen.width * 0.3f;  // 30% ширины экрана
        float height = Screen.height * 0.1f; // 10% высоты экрана

        // Применяем размеры
        buttonRect.sizeDelta = new Vector2(width, height);

        // Устанавливаем позицию (например, центр)
        buttonRect.anchoredPosition = new Vector2(0, -Screen.height * 0.2f); // Смещение вниз на 20% экрана
    }
}
