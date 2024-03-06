using System;
using UnityEngine;

public interface IChangeCar
{
    Sprite CurrentCarIamge { get; }
    event Action<Sprite> CarImageChanged;
    void ChangeCarImage(int direction);
}
