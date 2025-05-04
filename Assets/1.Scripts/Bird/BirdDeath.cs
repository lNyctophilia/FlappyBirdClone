using UnityEngine;

public class BirdDeath : MonoBehaviour
{
    public static BirdDeath Instance;
    public bool isDeath;
    Animator anim;
    Vector2 Startpos;
    private void Awake()
    {
        Startpos = transform.position;
        anim = GetComponent<Animator>();
        Instance = this;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            UIManager.Instance.OpenDeathMenu();
            DeathMenu.Instance.StartDeathUIAnim();
            DeathMenu.Instance.DeathUIScore();
            Death();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pipe")
        {
            UIManager.Instance.CloseGameMenu();
            Death();
        }
    }
    void Death()
    {
        Spawner.Instance.StopSpawn();
        Transition.Instance.Flash();
        gameObject.tag = "Untagged";
        isDeath = true;
        anim.speed = 0f;
    }
    public void Respawn()
    {
        transform.position = Startpos;
        anim.speed = 1f;
        isDeath = false;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        gameObject.tag = "Player";
        BirdMovement.Instance.Freeze();
    }
}
