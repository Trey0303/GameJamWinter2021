using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSanity : MonoBehaviour
{
    private Slider sanityBar;

    public int sanityMax = 1;

    public int gameoverScene = 2;

    // Start is called before the first frame update
    void Start()
    {
        sanityBar = gameObject.GetComponentInChildren<Slider>();
        if(sanityBar != null)
        {
            sanityBar.maxValue = sanityMax;
            sanityBar.value = sanityMax;
            StoreValues.Sanity = sanityBar.value;
            StoreValues.MaxStanity = sanityBar.maxValue;
            StoreValues.MinStanity = sanityBar.minValue;

        }
        else
        {
            Debug.Log("couldnt find SanityBar");
        }
    }

    private void FixedUpdate()
    {
        Debug.Log(StoreValues.atCampfire);
        UpdateSanity();
        if(sanityBar.value == sanityBar.minValue)
        {
            SceneManager.LoadScene(gameoverScene);
        }
        if (!StoreValues.atCampfire)
        {
            Debug.Log("Player out of light range");
            if (StoreValues.Sanity > StoreValues.MinStanity)//if alive & not in range
            {
                //regenerateSanity = false;

                StoreValues.Sanity -= .007f;
                UpdateSanity();

            }
        }
    }
    public void UpdateSanity()
    {
        if(StoreValues.Sanity != sanityBar.value)
        {
            sanityBar.value = StoreValues.Sanity;
        }
    }

}
