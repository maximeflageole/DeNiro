using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectUI : MonoBehaviour
{
    [SerializeField] private Image Image;
    [SerializeField] private TextMeshProUGUI StackAmountTxt;
    public int StackAmount { get; private set; }

    public void Init(Sprite sprite, int stackAmount)
    {
        gameObject.SetActive(true);
        Image.sprite = sprite;
        SetStackAmount(stackAmount);
    }

    public void SetStackAmount(int amount)
    {
        gameObject.SetActive(amount > 0);
        StackAmount = amount;
        StackAmountTxt.text = amount.ToString();
        Debug.Log("Status effect X set to " + amount);
    }
}
