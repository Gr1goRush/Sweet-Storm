using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class fruit : MonoBehaviour
{
    public float speed = 2f; // Скорость движения объекта
    public string playerTag = "Player"; // Тег для поиска объекта Player
    public Sprite newSprite; // Новый спрайт для объекта после разрезания
    private SpriteRenderer spriteRenderer; // Ссылка на компонент SpriteRenderer
    private GameObject playerObject; // Ссылка на объект Player
    private bool isCut = false; // Флаг для отслеживания разрезания объекта
    public HpManager hpManager;
    GameObject hpManagerGm;
    [SerializeField] bool def, gnil, gold, megaGold;
    Animator anim;
    bool cliceVol = true;
    [SerializeField] GameObject ledenec;
    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerObject = GameObject.FindGameObjectWithTag(playerTag);
        hpManagerGm = GameObject.FindGameObjectWithTag("HpManager");
        hpManager = hpManagerGm.GetComponent<HpManager>();
        Destroy(gameObject,7f);
    }

    void Update()
    {
        // Плавное движение объекта вниз
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Проверяем, зажата ли кнопка мыши и наведена ли она на объект
       
        if (Input.GetMouseButton(0) && IsMouseOverObject())
        {
            isCut = true;
            spriteRenderer.sprite = newSprite; // Смена спрайта после разрезания
            if (playerObject != null)
            {
                Vector3 targetPosition = new Vector3(transform.position.x, playerObject.transform.position.y, playerObject.transform.position.z);
                StartCoroutine(MovePlayer(playerObject.transform, targetPosition, 0.2f)); // Плавное передвижение объекта Player
            }
        }

        // Если объект разрезан, то уничтожаем его через некоторое время
        if (isCut)
        {
            
            anim.SetBool("rez",true);

            
            if (def)
            {
                hpManager.AddHealth(0.075f);
            }
            else if (gold)
            {
                hpManager.AddHealth(0.05f);
                PlayerPrefs.SetInt("score",PlayerPrefs.GetInt("score")+3);
                Instantiate(ledenec, transform.position, Quaternion.identity);
            }
            else if (megaGold)
            {
                hpManager.IncreaseHealthToMax(1);
            }
            else if (gnil)
            {
                hpManager.DecreaseHealth(0.2f);
            }
            OchistBool();
            Destroy(gameObject, 1f);
            if (PlayerPrefs.GetInt("SoundEnabled") == 1 && cliceVol)
            {
                hpManager.CliceVolPeredach();
                cliceVol = false;
                if (PlayerPrefs.GetInt("VibeEnabled") == 1)
                {
                    Handheld.Vibrate();
                }

            }
        }
    }

    // Проверка, находится ли курсор над объектом
    bool IsMouseOverObject()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = transform.position.z;
        return GetComponent<Collider2D>().bounds.Contains(mousePosition);
    }

    // Плавное перемещение объекта к целевой позиции
    IEnumerator MovePlayer(Transform playerTransform, Vector3 targetPosition, float duration)
    {
        if (transform.position.x>playerObject.transform.position.x)
        {
            playerObject.transform.rotation = new Quaternion(0,180,0,0);
        }
        else
        {
            playerObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        float elapsedTime = 0;
        Vector3 startingPosition = playerTransform.position;

        while (elapsedTime < duration)
        {
            playerTransform.position = Vector3.Lerp(startingPosition, targetPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerTransform.position = targetPosition; // Убедимся, что объект достигает точки точно
    }
    public void OchistBool()
    {
        gnil = false;
        gold = false;
        megaGold = false;
        def = false;      
    }
    public void DestroyAfterAnim()
    {
        Destroy(gameObject,2f);
    }
}
