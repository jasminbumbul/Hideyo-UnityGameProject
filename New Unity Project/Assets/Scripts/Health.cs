using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    private Rigidbody rigidbody;
    public GameObject EnemysKatana;
    private enemy_2_detection skripta;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        animator=this.GetComponent<Animator>();
        rigidbody=this.GetComponent<Rigidbody>();
        health = Maxhealth;
        skripta=this.GetComponent<enemy_2_detection>();

        slider.value = CalculateHealth();
      
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateHealth();


        if (health < Maxhealth)
        {
            heatlhBar.SetActive(true);
        }
        if (health <= 0)
        {
            if (this.transform.gameObject.name == "HumanModel")
            {

               // Destroy(this.gameObject);
                
                StartCoroutine(nextLevel(SceneManager.GetActiveScene().buildIndex));
            }
            else
            {
                animator.SetBool("Death",true);
                Destroy(EnemysKatana);
                skripta.gameObject.SetActive(false);
            }
        }
        if (health > Maxhealth)
        {
            health = Maxhealth;
        }
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
