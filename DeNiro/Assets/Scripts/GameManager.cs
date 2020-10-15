using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const float RateOfConversion = 0.3f;
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
