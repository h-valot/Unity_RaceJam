using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler
{
    [Header("REFERENCES")]
    public Image image;
    public TextMeshProUGUI text;
    
    [Header("SETTINGS")]
    public Color enteredColor = Color.white;
    public Color exitedColor = Color.black;
    
    [Space(10)]
    public UnityEvent OnClick;

    public void OnPointerEnter(PointerEventData data)
    {
        image.color = enteredColor;
        text.color = exitedColor;
    }

    public void OnPointerExit(PointerEventData data)
    {
        image.color = exitedColor;
        text.color = enteredColor;
    }

    public void OnPointerDown(PointerEventData data)
    {
        OnClick.Invoke();
    }
}
