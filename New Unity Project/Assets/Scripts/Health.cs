using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour
{
    public float healt;
    [SerializeField]

    public float Maxhealt;
    public GameObject heatlBar;
    public Slider slider;
    public Animator transition;
     float animationTime = 1f;
    public bool dead=false;


    void Start()
    {
        
        healt = Maxhealt;

        slider.value = CalculateHealth();
      
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateHealth();


        if (healt < Maxhealt)
        {
            heatlBar.SetActive(true);
        }
        if (healt <= 0)
        {
            if(this.transform.gameObject.name=="HumanModel")
            {
             
                Destroy(this.gameObject);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
               // StartCoroutine(nextLevel(SceneManager.GetActiveScene().buildIndex));
            }
            else
            {

            Destroy(gameObject);
            }
            
          
            

        }
        if(healt>Maxhealt)
        {
            healt = Maxhealt;
        }
    }
    public float CalculateHealth()
    {
        return healt / Maxhealt;
    }
   public void IncreaseHelath(float health22)
    {
        healt -= health22;
    }
    public IEnumerator nextLevel(int index)
    {
        transition.SetTrigger("start");
        yield return new WaitForSeconds(animationTime);
        SceneManager.LoadScene(index);

    }
}
