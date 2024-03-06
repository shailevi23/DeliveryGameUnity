using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCar : MonoBehaviour, IChangeCar
{
    [SerializeField] private GameObject changeCarPanel;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button applyButton;
    [SerializeField] private List<RawImage> carImages;
    [SerializeField] private RawImage currentCarImage;

    public event Action<Sprite> CarImageChanged;
    public Sprite CurrentCarIamge => currentCarImage.GetComponent<SpriteRenderer>().sprite;

    private int currIndex = 0;

    private void Start()
    {
        AddButtonListeners();
    }

    private void AddButtonListeners()
    {
        rightButton.onClick.AddListener(() => ChangeCarImage(1));
        leftButton.onClick.AddListener(() => ChangeCarImage(-1));
        applyButton.onClick.AddListener(ApplyCarImage);
    }

    public void ChangeCarImage(int direction)
    {
        currentCarImage.gameObject.SetActive(!currentCarImage.gameObject.activeSelf);

        var newIndex = currIndex + direction;
        if (newIndex < 0)
        {
            currIndex = carImages.Count - 1;
        }
        else if (newIndex >= carImages.Count)
        {
            currIndex = 0;
        }
        else
        {
            currIndex = newIndex;
        }

        currentCarImage = carImages[currIndex];
        currentCarImage.gameObject.SetActive(!currentCarImage.gameObject.activeSelf);
    }

    private void ApplyCarImage()
    {
        CarImageChanged?.Invoke(CurrentCarIamge);
        changeCarPanel.SetActive(!changeCarPanel.activeSelf);
    }
}
