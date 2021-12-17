using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//1.need to keep track of amount of wood player has

//2.player picks up and stores wood
//-e to pick up wood
//--player needs to be in 'interact' range(might use 'OnTriggerEnter')
//                      needs trigger collision for interact script

//3.player places wood in camp fire if any
//-e to place wood
//--player needs to be in 'interact' range 

public class Interact : MonoBehaviour
{
    Campfire campfire;

    //player
    float playerRadius = 2;
    
    //wood
    private bool woodInRange; //rangeCondition for update
    GameObject tmpObject; //selected wood
    int amountOfWood = 0;
    public Text woodAmountText;

    //campfire
    private bool campfireInRange;//rangeCondition for update
    public GameObject tmpCampfire; //selected campfire
    public float IncreaseAmount = 4;
    


    // Start is called before the first frame update
    void Start()
    {
        woodAmountText = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<Text>();
        woodAmountText.text = "" + amountOfWood;

        amountOfWood = 0;
        //playerRadius = 2;
        //Debug.Log("Started");

        tmpObject = null;
    }

    // Update is called once per frame
    void Update()
    {

        //diplay amount of wood collects currently
        woodAmountText.text = "" + amountOfWood;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (woodInRange)
            {
                //collects wood

                amountOfWood++;
                //Debug.Log("Wood");
                //Debug.Log("Collected: " + amountOfWood);
                Destroy(tmpObject);
                tmpObject = null;
                woodInRange = false;

            }
            if (campfireInRange)
            {
                //lightup campfire
                //Debug.Log("Light Up fire");
                Light tmpLightSelected = tmpCampfire.GetComponentInChildren<Light>();
                if (!tmpLightSelected.enabled)
                {
                    //Debug.Log("fire On");
                    tmpLightSelected.enabled = true;
                    tmpLightSelected.range = tmpLightSelected.range + IncreaseAmount;// increases range from 0
                    StoreValues.LightRange = tmpLightSelected.range;//store light range
                    

                }
                else//increase light range
                {
                    //tmpLightSelected = tmpLightSelected.range + 1;
                    if (tmpLightSelected.range < 7f)
                    {
                        tmpLightSelected.range = tmpLightSelected.range + IncreaseAmount;//increase light range

                    }
                    if(tmpLightSelected.range > 7f)//if light range is at max
                    {
                        tmpLightSelected.range = 7f;
                    }
                }
                campfire.curFireOn = tmpLightSelected.enabled;//checks if current fire is on
            }

        }
    }

    //if player near a piece of wood or campfire
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Object in range");
        if (other.tag == "Wood")
        {
            woodInRange = true;

            tmpObject = other.gameObject;
            

        }
        if(other.tag == "Campfire")
        {

            campfireInRange = true;
            tmpCampfire = other.gameObject;
            campfire = other.gameObject.GetComponentInChildren<Campfire>();
            //Debug.Log("Campfire");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wood")
        {
            woodInRange = false;
            //Debug.Log("Wood");
        }
        if (other.tag == "Campfire")
        {
            campfireInRange = false;
            //Debug.Log("Out of Campfire range");
        }
    }

    //void OnDrawGizmos()
    //{
    //    // Draw a yellow sphere at the transform's position
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(transform.position, playerRadius);
    //}

    
}
