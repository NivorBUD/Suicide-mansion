using System;
using UnityEngine;

public class Piano : MonoBehaviour
{
    public BrokenDoorInteraction door;
    public GameObject key;
    public Transform keyPos;
    public PianoDeath deathScript;
    public bool isEnd;
    public BoxCollider2D floorCollider;

    private Hero playerScript;
    private GameObject player;
    private Rigidbody2D rb;
    private PolygonCollider2D bc;
    private string[] dialog = new string[11] { "Забыла я про этот рояль", "Ну, теперь вспомнила", 
        "Надеюсь, ты не сильно ушибся", "Зато эта дверь открылась", "Лет 50 не могла открыть её", 
        "Cлууушай", "Убей ту летучую мышь", "Cлишком уж она мне надоела", "Pогатка", "Клавиша от рояля", "Действуй!"};


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Hero>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc = gameObject.GetComponent<PolygonCollider2D>();

        rb.centerOfMass = new Vector2(0, -1.5f);
    }

    void Update()
    {
        if (transform.localPosition.x <= -10.7f && transform.localPosition.x > -11.35f)
        {
            var sc = player.transform.localScale;
            var mult = Math.Max((0.65f - Math.Abs(-10.7f - transform.localPosition.x)) / 0.65f, 0.2f);
            sc.x = 0.4f * mult;
            player.transform.localScale = sc;
        }

        if (!door.isBroke && transform.localPosition.x <= -11)
        {
            BreakDoor();
            PlayBreakPianoSound(); //звук ломания пианино
        }

        if (!isEnd && playerScript.isCutScene && playerScript.rb.velocity.x == 0 &&
            (Math.Abs(Math.Round(player.transform.rotation.eulerAngles.z, 1) - 90) < 0.1f || Math.Abs(Math.Round(player.transform.rotation.eulerAngles.z, 1) - 270) < 0.1f))
        {
            playerScript.Death();
            Invoke(nameof(ShowGhost), 2.5f);
            Invoke(nameof(TurnOnBlackOut), 3.5f);
        }

        if (!isEnd && transform.localPosition.x < -11 && rb.velocity.x == 0)
        {
            EndDeath();
        }
    }

    private void PlayBreakPianoSound()
    {

    }

    private void ShowGhost()
    {
        playerScript.ghostScript.Show();
        playerScript.ghostScript.ChangeDialog(dialog);
    }

    private void BreakDoor()
    {
        var speed = rb.velocity;
        speed.x /= 2;
        rb.velocity = speed;

        door.Break();

        playerScript.rb.AddForce(new Vector2(-10, 5), ForceMode2D.Impulse);
        playerScript.rb.freezeRotation = false;
        playerScript.isCutScene = true;
        deathScript.blackOut.SetActive(false);
    }

    private void EndDeath()
    {
        floorCollider.enabled = true;
        key.transform.position = keyPos.position;
        key.SetActive(true);
        
        isEnd = true;
        rb.simulated = false;
        bc.enabled = false;
        deathScript.col.enabled = false;
    }

    private void TurnOnBlackOut()
    {
        deathScript.blackOut.SetActive(true);
    }
}
