using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SpaceShooter
{
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        /// <summary>
        /// ��� ���������
        /// </summary>
        [SerializeField] private Image m_VisualJoystick;

        /// <summary>
        /// �������� ������ ����
        /// </summary>
        [SerializeField] private Image m_Stick;

        /// <summary>
        /// �������� ���������
        /// </summary>
        public Vector3 Value { get; private set; }

        public void OnDrag(PointerEventData eventData) // �����������, ����� �� ���������� ������� UI, ���� �� �� ���� ������
        {
            Vector2 position = Vector2.zero;
            
            //��������� ���������� ������ �� ������ ��� ��������� (�� 1920�1080 � 400�400)
            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (m_VisualJoystick.rectTransform,
                eventData.position, // ������� ������� 
                eventData.pressEventCamera, // ������, ��������� � ��������� �������� OnPointerPress.
                out position); // 0, 0 - ����� ������, 400, 400 - ������ ������� ����

            // ����������� �������� � ���� ���������
            position.x = (position.x / m_VisualJoystick.rectTransform.sizeDelta.x); // sizeDelta.x = �������� Width � Rect Transform
            position.y = (position.y / m_VisualJoystick.rectTransform.sizeDelta.y); // 400/400 = 1, 200/400 = 0.5, 0/400 = 0

            /* ���� ��
            // ������� ���������� � �����, ����� ��� ������������� � ������������� ��� �� -1 �� +1
            position.x = position.x * 2 - 1;
            position.y = position.y * 2 - 1;

            Value = new Vector3(position.x, position.y, 0);

            // ������������� ������������, ���� �� ������ �� ������� ����, �� �� ����� �������� �� -1 �� +1
            if (Value.magnitude > 1) // ����� �������
                Value = Value.normalized;

            // Pivot point � ����� � ������(0.5, 0.5). ��� ����� ��� �� �������� ���� �� ����� ���� �� ������ �� �������?
            // �� ����������� ���� ������ �� 200(�������� ����) � �������� 75(�������� �����)
            // ��� �� ��� ������� �� 1/2 ���� ����� 1/2 ������ �����
            float offsetX = m_VisualJoystick.rectTransform.sizeDelta.x / 2 - m_Stick.rectTransform.sizeDelta.x / 2; 
            float offsetY = m_VisualJoystick.rectTransform.sizeDelta.y / 2 - m_Stick.rectTransform.sizeDelta.y / 2;

            //  anchoredPosition - ��������� ������ ����� RectTransform ������������ ������� ����� ��������.
            m_Stick.rectTransform.anchoredPosition = new Vector2(Value.x * offsetX, Value.y * offsetY);
            */

            float x = (m_VisualJoystick.rectTransform.pivot.x == 1f) ? position.x * 2 + 1 : position.x * 2 - 1;
            float y = (m_VisualJoystick.rectTransform.pivot.y == 1f) ? position.y * 2 + 1 : position.y * 2 - 1;

            Value = new Vector3(x, y, 0);
            Value = (Value.magnitude > 1) ? Value.normalized : Value;

            //to define the area in which joystick can move around
            m_Stick.rectTransform.anchoredPosition = new Vector3(Value.x * (m_VisualJoystick.rectTransform.sizeDelta.x / 3)
                                                                   , Value.y * (m_VisualJoystick.rectTransform.sizeDelta.y) / 3);

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Value = Vector3.zero;
            m_Stick.rectTransform.anchoredPosition = Vector3.zero;
        }
    }
}

