using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float healt;
    [SerializeField]

    public float Maxhealt;
    public GameObject heatlBar;
    public Slider slider;


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
            Destroy(gameObject);
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
}
