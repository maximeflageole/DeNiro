public class Pyre : TdUnit
{
    protected void Start()
    {
        Init(500.0f);
    }

    public override void Die(bool wasKilled = true)
    {
        GameManager.Instance.EndGame(false);
        gameObject.SetActive(false);
    }
}