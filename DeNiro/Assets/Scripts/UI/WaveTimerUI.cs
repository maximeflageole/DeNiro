using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveTimerUI : FadingComponent
{
    [SerializeField]
    private Image m_timerImage;
    [SerializeField]
    private TextMeshProUGUI m_timerTmpro;

    public void UpdateTimer(float currentTimer, float timerDuration)
    {
        float upperValue = Mathf.Ceil(timerDuration - currentTimer);
        m_timerImage.fillAmount = currentTimer / timerDuration;
        m_timerTmpro.text = (upperValue).ToString("0");
    }
}
