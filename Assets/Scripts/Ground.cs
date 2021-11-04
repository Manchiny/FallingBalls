using UnityEngine;

public class Ground : MonoBehaviour
{
    private void Awake()
    {
        MoveUnderScreen();
    }

    /// <summary>
    /// ��������� ��������� ����� �� ����������� ������� ��� �������� ������������, <br/>
    /// � ����������� �� ���������� ������.
    /// </summary>
    private void MoveUnderScreen()
    {
        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        float yPos = bottomLeft.y - 0.5f - 0.7f; // ������� �������� ������� ������� "�����" � ������� �����

        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
    }
}
