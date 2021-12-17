using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Campfire : MonoBehaviour
{
    public PlayerSanity sanityVal;
    public Interact curfire;

    //campfire light reference
    public Light campfireLight;
    public SphereCollider thisCollider;

    public Transform player;
    //public Collider playerCollider;
    //private bool regenerateSanity;

    public Vector3 sphereRange;

    private bool isPlayerInRange = false;

    public bool curFireOn = false;

    // Start is called before the first frame update
    void Start()
    {
        //set default values
        campfireLight = gameObject.GetComponentInChildren<Light>();

        StoreValues.LightRange = campfireLight.range;

        thisCollider = gameObject.GetComponentInChildren<SphereCollider>();

        thisCollider.radius = StoreValues.LightRange;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        //playerCollider = player.GetComponent<BoxCollider>();

        sanityVal = GameObject.FindGameObjectWithTag("UI").GetComponent<PlayerSanity>();

        curfire = player.GetComponentInChildren<Interact>();




    }

    // Update is called once per frame
    void Update()
    {
        sphereRange = new Vector3(transform.position.x + StoreValues.LightRange, transform.position.y + StoreValues.LightRange, transform.position.z + StoreValues.LightRange);
        //Debug.Log(transform.position.x + StoreValues.LightRange);
        //light range

        if(thisCollider.radius != campfireLight.range)//updates sphere collider range
        {
            thisCollider.radius = campfireLight.range;
        }

        //Light tmpLightSelected = curfire.tmpCampfire.GetComponent<Light>();




        if (curFireOn)//if fire is on
        {
            //Debug.Log("fire is ON");
            if (isPlayerInRange)// if player in range
            {
                Debug.Log("play in range");
                StoreValues.atCampfire = true;//player is at a campfire
                    //regenerateSanity = true;
                Debug.Log("Regenerate Player Sanity");
                if(StoreValues.Sanity < StoreValues.MaxStanity)
                {
                    //Debug.Log(StoreValues.MaxStanity);
                    StoreValues.Sanity += .001f;
                    sanityVal.UpdateSanity();
                    

                }
            }
            //else
            //{
            //    if (!StoreValues.atCampfire)
            //    {
            //        Debug.Log("Player out of light range");
            //        if (StoreValues.Sanity > StoreValues.MinStanity && !isPlayerInRange)//if alive & not in range
            //        {
            //            //regenerateSanity = false;

            //            StoreValues.Sanity -= .007f;
            //            sanityVal.UpdateSanity();

            //        }

            //    }
            //}

        }
        //else
        //{
        //    Debug.Log("Player out of light range");
        //    if (StoreValues.Sanity > StoreValues.MinStanity && !isPlayerInRange)//if alive & not in range
        //    {
        //        //regenerateSanity = false;

        //        StoreValues.Sanity -= .007f;
        //        sanityVal.UpdateSanity();

        //    }
        //}
    }

    //void OnDrawGizmos()
    //{
    //    // Draw a yellow sphere at the transform's position
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawSphere(transform.position, transform.position.x + StoreValues.LightRange);
    //}
    
    private void OnTriggerEnter(Collider otherObj)
    {
        if (otherObj.tag == "Player")
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider otherObj)
    {
        if (otherObj.tag == "Player")
        {
            isPlayerInRange = false;
            StoreValues.atCampfire = false;//player is NOT at a campfire
            Debug.Log("Player left campfire");
        }
    }
}
