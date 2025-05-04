using UnityEngine;

public class BirdScore : MonoBehaviour
{
    public static BirdScore Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ScoreArea")
        {
            Score.Instance.IncreaseScore();
        }
    }
}
