using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using YG;

public class Hero : MonoBehaviour
{
    public int levelComplete; // 1 - пройдена смерть в подвале, 2 - от рояля, 3 - от люстры, 4 - от мэри,
                              // 5 - от падения, 6 - от растения, 7 - от утопленя, 8 - на чердаке, 9 - от молнии,
                              // 10 - игра пройдена

    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private ParticleSystem respawnPoof;
    [SerializeField] private TextMeshProUGUI missionText, saveText;
    [SerializeField] GameObject ghost, getPlace, holdingPlace;
    [SerializeField] private GameObject piano, bathKey, bathBomb, acid, flamethrower, key, candle, board, treasureKey;
    public GameObject bullet;
    public Rigidbody2D rb;
    public BoxCollider2D col;
    public Dictionary<string, GameObject> inventory = new();
    public Transform bulletPlace, pointerAimTransform;
    public bool isCutScene, isHandsUp, isHorizontalLift, isAtTerrace, isPause, canPause, isReadyToShot;
    public Ghost ghostScript;
    public GameObject pointer;
    public Dictionary<string, GameObject> inventoryObjects = new();
    public Dictionary<string, int> inventoryObjectsIndex = new();

    private Vector3 liftPos, pointerAim;
    private bool isLift, isScared, isUppingHands, isAcid, isSit, isSlingshot, needToShowSaveText;
    private Animator anim;
    private SpriteRenderer sprite;
    private CameraController mainCamera;
    private Sprite standardSprite;

    private void Start()
    {
        InitilizeInventoryObjects();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        bulletPlace = GameObject.FindWithTag("Bullet Start Place").GetComponent<Transform>();
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        ghost = GameObject.FindWithTag("Ghost");
        ghostScript = ghost.GetComponent<Ghost>();
        ghostScript.Start();
        standardSprite = sprite.sprite;
        canPause = true;
        ChangeMission("Войти в дом");

        if (!YandexGame.savesData.isFirstSession)
            LoadSave();
    }

    private void InitilizeInventoryObjects()
    {
        inventoryObjects["Shovel"] = GameObject.FindWithTag("Shovel");
        inventoryObjects["Screwdriver"] = GameObject.FindWithTag("Screwdriver");
        inventoryObjects["Marker"] = GameObject.FindWithTag("Marker");
        inventoryObjects["Slingshot"] = GameObject.FindWithTag("Slingshot");
        inventoryObjects["Bathroom key"] = bathKey;
        inventoryObjects["Pantaloons"] = GameObject.FindWithTag("Pantaloons");
        inventoryObjects["Rope"] = GameObject.FindWithTag("Rope");
        inventoryObjects["Bath bomb"] = bathBomb;
        inventoryObjects["H2SO4"] = GameObject.FindWithTag("H2SO4");
        inventoryObjects["CaF2"] = GameObject.FindWithTag("CaF2");
        inventoryObjects["Acid"] = acid;
        inventoryObjects["Flamethrower"] = flamethrower;
        inventoryObjects["Screws"] = GameObject.FindWithTag("Screws");
        inventoryObjects["Key"] = key;
        inventoryObjects["Candle"] = candle;
        inventoryObjects["Board"] = board;
        inventoryObjects["Axe"] = GameObject.FindWithTag("Axe");
        inventoryObjects["Treasure key"] = treasureKey;
        
        inventoryObjectsIndex["Shovel"] = 0;
        inventoryObjectsIndex["Screwdriver"] = 1;
        inventoryObjectsIndex["Marker"] = 2;
        inventoryObjectsIndex["Slingshot"] = 3;
        inventoryObjectsIndex["Bathroom key"] = 4;
        inventoryObjectsIndex["Pantaloons"] = 5;
        inventoryObjectsIndex["Rope"] = 6;
        inventoryObjectsIndex["Bath bomb"] = 7;
        inventoryObjectsIndex["H2SO4"] = 8;
        inventoryObjectsIndex["CaF2"] = 9;
        inventoryObjectsIndex["Acid"] = 10;
        inventoryObjectsIndex["Flamethrower"] = 11;
        inventoryObjectsIndex["Screws"] = 12;
        inventoryObjectsIndex["Key"] = 13;
        inventoryObjectsIndex["Candle"] = 14;
        inventoryObjectsIndex["Board"] = 15;
        inventoryObjectsIndex["Axe"] = 16;
        inventoryObjectsIndex["Treasure key"] = 17;
    }

    public void LoadSave()
    {
        var data = YandexGame.savesData;
        transform.position = new Vector3(data.playerPos[0], data.playerPos[1], 0);
        mainCamera.TPToPlayer();
        pointerAim = new Vector3(data.playerHintDirection[0], data.playerHintDirection[1]);

        ghostScript.ChangeDialog(data.ghostDialog);
        ghostScript.canChangePhraseByButton = data.ghostCanChangePhraseByButton;
        ghostScript.phraseIndex = data.ghostPhraseIndex;
        missionText.text = data.mission;

        levelComplete = data.levelComplete;
        LadderInteraction.canUseLadders = data.canUseLadders;
        InventoryLogic.canGetItems = data.canGetItems;
        ChangeMission(data.mission);

        if (data.inventoryItems[0] != null && data.inventoryItems[0] != "")
            AddToInventory(inventoryObjects[data.inventoryItems[0]]);
        if (data.inventoryItems[1] != null && data.inventoryItems[1] != "")
            AddToInventory(inventoryObjects[data.inventoryItems[1]]);
        var inventoryObjectsKeys = inventoryObjects.Keys.ToArray();

        for (int i = 0; i < 18; i++)
        {
            if (data.isUsedInventoryItems[i])
            {
                Destroy(inventoryObjects[inventoryObjectsKeys[i]]);
                continue;
            }
            if (data.activeColliderInventoryItems[i])
                inventoryObjects[inventoryObjectsKeys[i]].GetComponent<BoxCollider2D>().enabled = true;
        }

        if (levelComplete == 1)
        {
            ghost.SetActive(true);
            ghostScript.ChangeDialog(data.ghostDialog);
            ghostScript.canChangePhraseByButton = data.ghostCanChangePhraseByButton;
            ghostScript.transform.position = new Vector3(data.ghostPos[0], data.ghostPos[1], data.ghostPos[2]);
            ghostScript.sprite.color = new Color(255, 255, 255, 1);
            ghostScript.phraseIndex = data.ghostPhraseIndex;
            ghostScript.StartDialog();
            ghostScript.ChangeAimToPlayer();
            ghostScript.isDialog = false;
            isCutScene = false;
        }
        else if (levelComplete == 4)
        {
            GameObject.FindWithTag("Mirror").GetComponent<MirrorDeath>().SpawnMary();
        }
        else if (levelComplete == 5)
        {
            ghostScript.ChangeDialog(new string[]{"Деревянный пол в ванной?", "Это провал… Да какой огромный!",
            "Ты видишь? Растения оплели дверь!", "Нужно от них избавиться",
            "Тут есть пара склянок с химикатами", "Смешай их в котле", "Ничего сложного, правда?",
            "Ты же встречал вид <I>лианиус резняриус</I>", "Если так не выйдет, то...", "Попробуй что-то другое"});
            ghostScript.Show();
            ghostScript.mission = "Смешать в котле химикаты и вылить на растения";
        }
    }

    public void SaveSave()
    {
        YandexGame.savesData.isFirstSession = false;
        YandexGame.savesData.playerPos = new float[] { transform.position.x, transform.position.y, 0 };
        YandexGame.savesData.playerHintDirection = new float[] { pointerAim.x, pointerAim.y, 0 };
        YandexGame.savesData.levelComplete = levelComplete;
        YandexGame.savesData.mission = missionText.text;

        YandexGame.savesData.ghostDialog = ghostScript.GetDialog();
        YandexGame.savesData.ghostPos = new float[3] { ghostScript.transform.position.x, ghostScript.transform.position.y, ghostScript.transform.position.z };
        YandexGame.savesData.ghostPhraseIndex = ghostScript.phraseIndex;
        YandexGame.savesData.ghostCanChangePhraseByButton = ghostScript.canChangePhraseByButton;
        
        YandexGame.savesData.canGetItems = InventoryLogic.canGetItems;
        YandexGame.savesData.canUseLadders = LadderInteraction.canUseLadders;
        
        YandexGame.savesData.pianoPos = new float[3] { piano.transform.position.x, 
            piano.transform.position.y, piano.transform.position.z };
        

        var inventoryKeys = inventory.Keys.ToArray();
        if (inventoryKeys.Length == 0)
        {
            YandexGame.savesData.inventoryItems[0] = "";
            YandexGame.savesData.inventoryItems[1] = "";
        }
        else if (inventoryKeys.Length == 1)
        {
            YandexGame.savesData.inventoryItems[0] = inventoryKeys[0];
            YandexGame.savesData.inventoryItems[1] = "";
        }
        else
        {
            YandexGame.savesData.inventoryItems[0] = inventoryKeys[0];
            YandexGame.savesData.inventoryItems[1] = inventoryKeys[1];
        }

        YandexGame.SaveProgress();
        YandexGame.SaveLocal();

        needToShowSaveText = true;
    }

    IEnumerator ShowSaveText()
    {
        saveText.color = new Color(255, 255, 255, 1);
        yield return new WaitForSeconds(1.5f);

        while (saveText.color.a > 0)
        {
            saveText.color = new Color(255, 255, 255, saveText.color.a - 0.01f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void Run()
    {
        State = States.walk;

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0.0f;
        col.offset = new Vector2(-0.05326271f, -0.0412395f);
        col.size = new Vector2(2.506526f, 4.292479f);
    }

    public void StartLift(bool isHorizontalLadder, Vector3 anotherLadderPos)
    {
        rb.velocity = Vector3.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        this.isHorizontalLift = isHorizontalLadder;
        isLift = !isHorizontalLadder;
        liftPos = anotherLadderPos;
        isCutScene = true;
        sprite.flipX = transform.position.x > anotherLadderPos.x;
    }

    public void StopLift()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        isLift = false;
        isHorizontalLift = false;
        isCutScene = false;
    }

    public void StartSit()
    {
        isSit = true;
        State = States.sit;
    }

    public void EndSit()
    {
        isSit = false;
    }

    public void StartSlingshot()
    {
        isSlingshot = true;
        State = States.slingshot;
    }

    public void StopSlingshot()
    {
        isSlingshot = false;
    }

    public void AddToInventory([SerializeField] GameObject InventoryObject)
    {
        inventory[InventoryObject.name] = InventoryObject;
        YandexGame.savesData.isUsedInventoryItems[inventoryObjectsIndex[InventoryObject.name]] = true;
        if (InventoryObject.name == "Shovel")
            return;
        StopPointerAiming();
    }

    public void DelFromInventory([SerializeField] GameObject InventoryObject)
    {
        inventory.Remove(InventoryObject.name);
    }

    void Update()
    {
        if (needToShowSaveText && gameObject.activeSelf)
        {
            needToShowSaveText = false;
            StartCoroutine(ShowSaveText());
        }

        if (levelComplete == 1)
            ghostScript.isDialog = false;

        if (levelComplete == 5 && ghostScript.GetDialog() != null && ghostScript.GetDialog().Length != 10)
        {
            ghostScript.ChangeDialog(new string[]{"Деревянный пол в ванной?", "Это провал… Да какой огромный!",
            "Ты видишь? Растения оплели дверь!", "Нужно от них избавиться",
            "Тут есть пара склянок с химикатами", "Смешай их в котле", "Ничего сложного, правда?",
            "Ты же встречал вид <I>лианиус резняриус</I>", "Если так не выйдет, то...", "Попробуй что-то другое"});
        }

        if (isPause)
            return;

        if (transform.position.z != 0 && !isHorizontalLift)
        {
            var pos = transform.position;
            pos.z = 0;
            transform.position = pos;
        }
        else if (isHorizontalLift)
        {
            var pos = transform.position;
            pos.z = 1;
            transform.position = pos;
        }

        ChangePointerAngle();
        ChangeSprite();

        if (!ghostScript.isDialog && !isCutScene && Input.GetButtonDown("Jump"))
            Jump();

        if (!ghostScript.isDialog && !isCutScene && Input.GetButton("Horizontal"))
            Run();

        if (isLift || isHorizontalLift)
        {
            State = isHorizontalLift ? States.walk : States.lift;
            transform.position = Vector3.MoveTowards(transform.position, liftPos, speed * Time.deltaTime);
            if (Math.Abs(transform.position.x - liftPos.x) <= 0.1f &&
                Math.Abs(transform.position.y - liftPos.y) <= 0.1f)
                StopLift();
        }

        getPlace.transform.localPosition = new Vector3((sprite.flipX ? 1 : -1) * Math.Abs(getPlace.transform.localPosition.x), getPlace.transform.localPosition.y, getPlace.transform.localPosition.z);
        holdingPlace.transform.localPosition = new Vector3((sprite.flipX ? -1 : 1) * Math.Abs(holdingPlace.transform.localPosition.x), holdingPlace.transform.localPosition.y, holdingPlace.transform.localPosition.z);
    }

    public void ChangeMission(string mission)
    {
        missionText.text = mission;
    }

    private void ChangeSprite()
    {
        if (State != States.walk)
        {
            col.offset = new Vector2(-0.05326271f, -0.07494116f);
            col.size = new Vector2(2.506526f, 4.359882f);
        }

        if (!isSlingshot && !isSit && !isAcid && !isUppingHands && !isScared && State != States.electric)
            State = States.idle;

        else if (isScared)
        {
            State = States.scare;
            if (sprite.sprite.name == "Player Scaried 9")
                Death();
        }
        else if (isAcid)
        {
            State = States.acid;
            if (sprite.sprite.name == "Player Acid 9")
                Death();
        }
        else if (isSit)
            State = States.sit;
        else if (isUppingHands)
        {
            if (sprite.sprite.name == "PlayerPrimer 7" || State == States.handsUpStand)
            {
                isHandsUp = true;
                State = States.handsUpStand;
            }
            else
                State = States.handsUp;
        }
        else if (isSlingshot)
        {
            State = States.slingshot;
            if (sprite.sprite.name == "9")
                isReadyToShot = true;
        }
    }

    public void TurnRight()
    {
        sprite.flipX = false;
    }

    public void TurnLeft()
    {
        sprite.flipX = true;
    }

    private void ChangePointerAngle()
    {
        if (!pointer.activeSelf)
            return;

        var zAngle = Math.Atan2(pointerAim.y - pointer.transform.position.y,
            pointerAim.x - pointer.transform.position.x) * 180 / Math.PI;

        pointer.transform.rotation = Quaternion.Euler(0, 0, (float)zAngle);
    }

    public void ChangePointerAim(Transform pos)
    {
        pointerAimTransform = pos;
        pointerAim = pos.position;
    }

    public void StopPointerAiming()
    {
        pointer.SetActive(false);
    }

    public void EletricSchock()
    {
        State = States.electric;
    }

    public void EndCutScene()
    {
        isCutScene = false;
        mainCamera.ZoomIn(5);
        mainCamera.ChangeAimToPlayer();
        ghostScript.canChangePhraseByButton = true;
    }

    public void DeadlyScare()
    {
        isScared = true;
        PlayScareSound(); // звук испуга
        sprite.flipX = !sprite.flipX;
    }

    public void UpHands()
    {
        isUppingHands = true;
        sprite.flipX = false;
    }

    public void StopHandsUp()
    {
        isUppingHands = false;
        isHandsUp = false;
    }

    public void Acid()
    {
        isAcid = true;
        PlayAcidSound();
    }

    private void PlayAcidSound()
    {

    }

    private void PlayScareSound()
    {

    }

    public void Death()
    {
        EndCutScene();
        isScared = false;
        isUppingHands = false;
        isAcid = false;
        gameObject.SetActive(false);
        State = States.idle;
        Invoke(nameof(RespawnPoof), 1);

        if (transform.localScale.y != 0.4 || transform.localScale.x != 0.4)
        {
            var sc = transform.localScale;
            sc.y = 0.4f;
            sc.x = 0.4f;
            transform.localScale = sc;
        }
    }

    public void NoRespawnDeath()
    {
        EndCutScene();
        isScared = false;
        isUppingHands = false;
        isAcid = false;
        gameObject.SetActive(false);
        State = States.idle;
    }

    public void RespawnPoof()
    {
        var pos = transform.position;
        pos.z = respawnPoof.gameObject.transform.position.z;
        respawnPoof.gameObject.transform.position = pos;
        respawnPoof.Play();
        Invoke(nameof(Respawn), 1.5f);
    }

    private void Respawn()
    {
        var rot = gameObject.transform.rotation;
        rot.z = 0;
        rb.freezeRotation = true;
        transform.rotation = rot;
        gameObject.SetActive(true);
    }

    public void Up()
    {
        EndSit();
        sprite.flipX = true;
        rb.AddForce(transform.right * jumpForce, ForceMode2D.Impulse);
        Invoke(nameof(TurnUp), 0.05f);
    }

    private void TurnUp()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
        isCutScene = false;
    }

    public States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Jump()
    {
        if (rb.velocity.y == 0)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
}

public enum States
{
    idle,
    walk,
    lift,
    electric,
    scare,
    handsUp,
    handsUpStand,
    acid,
    sit,
    slingshot
}
