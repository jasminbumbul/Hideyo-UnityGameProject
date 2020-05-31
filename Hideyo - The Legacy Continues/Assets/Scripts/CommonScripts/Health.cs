using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public float health;
    [SerializeField]

    public float Maxhealth;
    public GameObject heatlhBar;
    public Slider slider;
    public Animator transition;
    private Animator animator;
     float animationTime = 1f;
    public bool dead=false;
    public static Health instance;
    public GameObject EnemysKatana;
    private NavMeshAgent skripta;
    private enemy_2_detection skripta2;
    public AudioSource deathAudioSource;
    public GameObject Key;
    public GameObject ItemSpawnPoint;
    public Transform SpawnedItemsContainer;

    public PlayerMovement playerMovement;
    public PlayerMovement2 test;

    private bool soundHasPlayed;
    private bool hasDroppedKey;
    private float timer=0.0f;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        animator=this.GetComponent<Animator>();
        health = Maxhealth;
        skripta=this.GetComponent<NavMeshAgent>();
        skripta2=this.GetComponent<enemy_2_detection>();

        playerMovement=this.GetComponent<PlayerMovement>();
        test=this.GetComponent<PlayerMovement2>();

        slider.value = CalculateHealth();
     
      
    }

    // Update is called once per frame
    void Update()
    {

        timer+=Time.deltaTime;
        timer%=60;
        slider.value = CalculateHealth();


        if (health < Maxhealth)
        {
            heatlhBar.SetActive(true);
        }
        if (health <= 0)
        {
            if(this.transform.gameObject.name=="SATOSHI")
            {
                Invoke("Level0", 3);
            }
            if (this.transform.gameObject.name == "HumanModel")
            {
                playerMovement.enabled=false;
                test.enabled=false;
                animator.SetBool("Running",false);
                animator.SetBool("Death", true);
                if (!soundHasPlayed)
                {
                    deathAudioSource.Play();
                    soundHasPlayed = true;
                }

                if(timer>2)
                {
                    Player.Instance.RemoveAllInventoryItems();
                }
                //if (SceneManager.GetActiveScene().name == "main")
                //{
                    Invoke("Level1", 3);
                //}
                //if (SceneManager.GetActiveScene().name == "SecondScene")
                //{
                //    Invoke("Level2", 3);
                //}
                
            }
            else
            {
                animator.SetBool("Death", true);
                Destroy(EnemysKatana);
                skripta.enabled = false;
                skripta2.enabled = false;
                if (!soundHasPlayed)
                {
                    deathAudioSource.Play();
                    soundHasPlayed = true;
                }
                if(this.gameObject.name=="EnemyWithKey" && !hasDroppedKey)
                {
                    hasDroppedKey=true;
                    GameObject.Instantiate(Key, ItemSpawnPoint.transform.position, ItemSpawnPoint.transform.rotation, SpawnedItemsContainer);
                }
                Invoke("DestroyObject", 10);
                if(this.gameObject.name=="SATOSHI")
                {
                    postfight();
                }
            }
        }
        if (health > Maxhealth)
        {
            health = Maxhealth;
        }
    }
    private void Level1()
    {
        StartCoroutine(nextLevel(SceneManager.GetActiveScene().buildIndex));
    }private void Level0()
    {
        StartCoroutine(nextLevel(0));
    }
    private void postfight()
    {
        StartCoroutine(nextLevel(7));
    }
    //private void Level2()
    //{
    //    StartCoroutine(nextLevel(SceneManager.GetActiveScene().buildIndex));
    //}
    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
    public float CalculateHealth()
    {
        return health / Maxhealth;
    }
   public void IncreaseHelath(float health22)
    {
        health -= health22;
    }
    public IEnumerator nextLevel(int index)
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(animationTime);
        SceneManager.LoadScene(index);

    }
}
