using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LPlayerInput : MonoBehaviour
{
    private int count;
    public GameObject Smile_sun_on, Sad_sun_on, Rain_on, Jack_left, Jack_right;
    public InputManager inputmanager = new InputManager();
    private float timer;

    public AudioClip beep;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
        Jack_left.GetComponent<SpriteRenderer>().enabled = false;
        Jack_right.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))//왼쪽물주기
        {
            AudioManager.AudioPlay(beep);

            timer += Time.deltaTime;
            inputmanager.PlusLeftCount();
            Jack_left.GetComponent<SpriteRenderer>().enabled = true;
            
        }
        else if (Input.GetKeyDown(KeyCode.E) )//오른쪽물주기
        {
            AudioManager.AudioPlay(beep);

            inputmanager.PlusRightCount();
            Jack_right.GetComponent<SpriteRenderer>().enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.W))//성장시키기
        {
            AudioManager.AudioPlay(beep);

            inputmanager.PlusGrowth();
            Jack_left.GetComponent<SpriteRenderer>().enabled = true;
            Jack_right.GetComponent<SpriteRenderer>().enabled = true;
        }

        if(timer > 0.3f)
        {
            Jack_left.GetComponent<SpriteRenderer>().enabled = false;
            Jack_right.GetComponent<SpriteRenderer>().enabled = false;
            timer = 0.0f;
        }
        else if(timer <= 0.3f)
        {
            timer += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            timer = 0.0f;
            Jack_left.GetComponent<SpriteRenderer>().enabled = false;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            timer = 0.0f;
            Jack_right.GetComponent<SpriteRenderer>().enabled = false;
        }

        /*
        Smile_sun_on.GetComponent<SpriteRenderer>().enabled = inputmanager.Smile_Sun;
        Debug.Log(inputmanager.Smile_Sun);
        Sad_sun_on.GetComponent<SpriteRenderer>().enabled = inputmanager.Sad_Sun;
        Rain_on.GetComponent<SpriteRenderer>().enabled = inputmanager.Rain;
        */

        Smile_sun_on.GetComponent<SpriteRenderer>().enabled = inputmanager.smile_sun;
        Sad_sun_on.GetComponent<SpriteRenderer>().enabled = inputmanager.sad_sun;
        Rain_on.GetComponent<SpriteRenderer>().enabled = inputmanager.rain;
    }
}
