using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabIterator : MonoBehaviour
{
    [SerializeField] private Button _plusButton;
    [SerializeField] private Button _minusButton;
    [SerializeField] Text _text;

    public int Number { get; private set; } = 0;

    private void Start()
    {
        _plusButton.onClick.AddListener(() => {
            if (Number >= 2)
                Number = 0;
            else
                Number++;

            _text.text = Number.ToString();
        });

        _minusButton.onClick.AddListener(() => {
            if (Number <= 0)
                Number = 2;
            else
                Number--;

            _text.text = Number.ToString();
        });
    }
}
