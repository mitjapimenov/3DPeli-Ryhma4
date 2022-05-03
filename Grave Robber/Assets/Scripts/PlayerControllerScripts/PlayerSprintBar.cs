using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSprintBar : MonoBehaviour
{

    public Slider sprintBar;

    public float maxSprint;
    public float sprint;



    public float smoothing = 5;

    public void Awake()
    {
        sprint = maxSprint;
        sprintBar.maxValue = maxSprint;
        sprintBar.value = sprint;
    }

    public void DrainStamina(float stamina)
    {
        if(sprint - stamina >= 0)
        {
            sprint -= stamina;
        }
        else
        {
            sprint = 0;

        }
    }

    public void Breathing(float increase){
        if(sprint + increase <= maxSprint)
        {
            sprint += increase;
        }
        else{
            sprint = maxSprint;
        }
    }

    public void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift))
        {
            DrainStamina(1);
        }
        else //(Input.GetKey(KeyCode.E))
        {
            Breathing(0.1f);
        }



        if(sprint == 0)
        {
            GetComponent<PlayerController>().walkSpeed = 6f;
        }




        if(sprintBar.value != sprint)
        {
            sprintBar.value = Mathf.Lerp(sprintBar.value, sprint, smoothing * Time.deltaTime);
        }
    }
}
