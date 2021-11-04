using UnityEngine;

public class Ground : MonoBehaviour
{
    private void Awake()
    {
        MoveUnderScreen();
    }

    /// <summary>
    /// Подгоняет положение земли на необходимую позицию под экранное пространство, <br/>
    /// в зависимости от разрешения экрана.
    /// </summary>
    private void MoveUnderScreen()
    {
        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        float yPos = bottomLeft.y - 0.5f - 0.7f; // вычитаю половину толщины спрайта "земли" и диаметр шаров

        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }
}
