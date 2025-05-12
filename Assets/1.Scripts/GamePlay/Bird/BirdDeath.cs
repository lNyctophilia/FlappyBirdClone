using UnityEngine;

public class BirdDeath : MonoBehaviour
{
    public static BirdDeath Instance;

    private void Awake() => Instance = this;

    private void OnCollisionEnter2D(Collision2D collision) => HandleCollisionOrTrigger(collision.gameObject);
    private void OnTriggerEnter2D(Collider2D collision) => HandleCollisionOrTrigger(collision.gameObject);

    private void HandleCollisionOrTrigger(GameObject obj)
    {
        if (obj.CompareTag("Ground") || obj.CompareTag("Pipe"))
            GameManager.Instance.ChangeState(GameState.Ending);
    }
}
