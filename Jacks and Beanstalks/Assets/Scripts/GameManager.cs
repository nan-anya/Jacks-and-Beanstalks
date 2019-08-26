using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int growPoint;

    public GameObject leftPlayer;
    public GameObject rightPlayer;

    InputManager LInput;
    InputManager RInput;

    public bool[,] LReach = new bool[7, 5];
    public bool[,] RReach = new bool[7, 5];

    public bool[,] LItemLoc = new bool[7, 5];
    public bool[,] RItemLoc = new bool[7, 5];

    GameObject[,] LAppearance = new GameObject[7, 5];
    GameObject[,] RAppearance = new GameObject[7, 5];

    Coroutine LSmileSun;
    Coroutine LSadSun;
    Coroutine LFog;

    Coroutine RSmileSun;
    Coroutine RSadSun;
    Coroutine RFog;

    public AudioClip winSound;

    void Start()
    {
        growPoint = 20;

        LInput = leftPlayer.GetComponent<LPlayerInput>().inputmanager;
        RInput = rightPlayer.GetComponent<RPlayerInput>().inputmanager;

        LInput.gameManager = this;
        RInput.gameManager = this;

        LInput.LR = true;
        RInput.LR = false;

        leftPlayer.transform.Find("Beanstalk").Find("Bottom").Find("FirstStem").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Stem_1");
        rightPlayer.transform.Find("Beanstalk").Find("Bottom").Find("FirstStem").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Stem_2");

        for (int i = 0; i < 7; i++)
        {
            LAppearance[i, 0] = leftPlayer.transform.Find("Beanstalk").transform.Find("Branch_L2_" + (i + 1)).gameObject;
            LAppearance[i, 1] = leftPlayer.transform.Find("Beanstalk").transform.Find("Branch_L1_" + (i + 1)).gameObject;
            LAppearance[i, 2] = leftPlayer.transform.Find("Beanstalk").transform.Find("Stem_" + (i + 1)).gameObject;
            LAppearance[i, 3] = leftPlayer.transform.Find("Beanstalk").transform.Find("Branch_R1_" + (i + 1)).gameObject;
            LAppearance[i, 4] = leftPlayer.transform.Find("Beanstalk").transform.Find("Branch_R2_" + (i + 1)).gameObject;

            RAppearance[i, 0] = rightPlayer.transform.Find("Beanstalk").transform.Find("Branch_L2_" + (i + 1)).gameObject;
            RAppearance[i, 1] = rightPlayer.transform.Find("Beanstalk").transform.Find("Branch_L1_" + (i + 1)).gameObject;
            RAppearance[i, 2] = rightPlayer.transform.Find("Beanstalk").transform.Find("Stem_" + (i + 1)).gameObject;
            RAppearance[i, 3] = rightPlayer.transform.Find("Beanstalk").transform.Find("Branch_R1_" + (i + 1)).gameObject;
            RAppearance[i, 4] = rightPlayer.transform.Find("Beanstalk").transform.Find("Branch_R2_" + (i + 1)).gameObject;
        }

        placeRandomItem();
    }

    public void draw()
    {
        //왼쪽 플레이어
        for (int i = 0; i < 7; i++)
        {
            if (LReach[i, 2])
            {
                //좌 가지
                if (LReach[i, 0])
                {
                    LAppearance[i, 0].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/BranchTip_1_L");
                    LAppearance[i, 1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Branch_1");
                }
                else if (LReach[i, 1])
                {
                    LAppearance[i, 1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/BranchTip_1_L");
                }

                //우 가지
                if (LReach[i, 4])
                {
                    LAppearance[i, 4].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/BranchTip_1_R");
                    LAppearance[i, 3].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Branch_1");
                }
                else if (LReach[i, 3])
                {
                    LAppearance[i, 3].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/BranchTip_1_R");
                }

                //줄기
                if (LReach[i, 1])
                {
                    if (LReach[i, 3])
                    {
                        LAppearance[i, 2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Stem_1_LR");
                    }
                    else
                    {
                        LAppearance[i, 2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Stem_1_L");
                    }
                }
                else if (LReach[i, 3])
                {
                    LAppearance[i, 2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Stem_1_R");
                }

                else
                {
                    LAppearance[i, 2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Stem_1");
                }
            }
        }

        //오른쪽 플레이어
        for (int i = 0; i < 7; i++)
        {
            if (RReach[i, 2])
            {
                //좌 가지
                if (RReach[i, 0])
                {
                    RAppearance[i, 0].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/BranchTip_2_L");
                    RAppearance[i, 1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Branch_2");
                }
                else if (RReach[i, 1])
                {
                    RAppearance[i, 1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/BranchTip_2_L");
                }

                //우 가지
                if (RReach[i, 4])
                {
                    RAppearance[i, 4].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/BranchTip_2_R");
                    RAppearance[i, 3].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Branch_2");
                }
                else if (RReach[i, 3])
                {
                    RAppearance[i, 3].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/BranchTip_2_R");
                }

                //줄기
                if (RReach[i, 1])
                {
                    if (RReach[i, 3])
                    {
                        RAppearance[i, 2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Stem_2_LR");
                    }
                    else
                    {
                        RAppearance[i, 2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Stem_2_L");
                    }
                }
                else if (RReach[i, 3])
                {
                    RAppearance[i, 2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Stem_2_R");
                }

                else
                {
                    RAppearance[i, 2].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Stem_2");
                }
            }
        }
    }

    //true = 왼쪽 false = 오른쪽
    public void GameOver(bool winner)
    {
        if (winner)
        {
            Debug.Log("Left Win!");

            leftPlayer.transform.Find("Beanstalk").transform.Find("Stem_8").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Top_1");

            leftPlayer.transform.Find("Win").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Win_ON");
        }
        else
        {
            Debug.Log("Right Win!");

            rightPlayer.transform.Find("Beanstalk").transform.Find("Stem_8").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Top_2");

            rightPlayer.transform.Find("Win").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/Win_ON");
        }

        AudioManager.AudioPlay(winSound);

        leftPlayer.GetComponent<LPlayerInput>().enabled = false;
        rightPlayer.GetComponent<RPlayerInput>().enabled = false;
    }

    public void placeRandomItem()
    {
        for (int i = 0; i < 3; i++)
        {
            int c = Random.Range(0, 2);

            if (c == 1)
            {
                c = 4;
            }

            int r = Random.Range(0, 7);

            if (!LItemLoc[r, c])
            {
                LItemLoc[r, c] = true;

                LAppearance[r, c].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/ItemBox");
            }
            else
            {
                i--;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            int c = Random.Range(0, 2);

            if (c == 1)
            {
                c = 4;
            }

            int r = Random.Range(0, 7);

            if (!RItemLoc[r, c])
            {
                RItemLoc[r, c] = true;

                RAppearance[r, c].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/ItemBox");
            }
            else
            {
                i--;
            }
        }
    }

    public void GetRandomItem(bool player)
    {
        //0 = 자신에게 웃는 태양, 1 = 상대에게 찡그린 태양, 2 = 자신에게 비, 3 = 상대에게 번개, 4 = 상대에게 안개

        int item = Random.Range(0, 5);

        if (item == 0)
        {
            smileSun(player);
        }
        else if (item == 1)
        {
            sadSun(!player);
        }
        else if (item == 2)
        {
            rain(player);
        }
        else if (item == 3)
        {
            lightning(!player);
        }
        else if (item == 4)
        {
            fog(!player);
        }
    }

    public void smileSun(bool player)
    {
        if (player)
        {
            if (LInput.sad_sun == true)
            {
                LInput.sad_sun = false;
                StopCoroutine(LSadSun);
            }
            else
            {
                LInput.smile_sun = true;

                LSmileSun = StartCoroutine(smileSunC(player));
            }
        }
        else
        {
            if (RInput.sad_sun == true)
            {
                RInput.sad_sun = false;
                StopCoroutine(RSadSun);
            }
            else
            {
                RInput.smile_sun = true;
                RSmileSun = StartCoroutine(smileSunC(player));
            }
        }
    }

    IEnumerator smileSunC(bool player)
    {
        if (player)
        {
            yield return new WaitForSeconds(3.0f);

            LInput.smile_sun = false;

            Debug.Log("left smile sun end");
        }
        else
        {
            yield return new WaitForSeconds(3.0f);

            RInput.smile_sun = false;

            Debug.Log("right smile sun end");
        }
    }

    public void sadSun(bool player)
    {
        if (player)
        {
            if (LInput.smile_sun == true)
            {
                LInput.smile_sun = false;
                StopCoroutine(LSmileSun);
            }
            else
            {
                LInput.sad_sun = true;
                LSadSun = StartCoroutine(sadSunC(player));
            }
        }
        else
        {
            if (RInput.smile_sun == true)
            {
                RInput.smile_sun = false;
                StopCoroutine(RSmileSun);
            }
            else
            {
                RInput.sad_sun = true;
                RSadSun = StartCoroutine(sadSunC(player));
            }
        }
    }

    IEnumerator sadSunC(bool player)
    {
        if (player)
        {
            yield return new WaitForSeconds(3.0f);

            LInput.sad_sun = false;
        }
        else
        {
            yield return new WaitForSeconds(3.0f);

            RInput.sad_sun = false;
        }
    }

    public void rain(bool player)
    {
        if (player)
        {
            LInput.rain = true;
        }
        else
        {
            RInput.rain = true;
        }
    }

    public void lightning(bool player)
    {
        if (player)
        {
            LInput.hitByLightning();
        }
        else
        {
            RInput.hitByLightning();
        }
    }

    public void fog(bool player)
    {
        StartCoroutine(fogC(player));
    }

    IEnumerator fogC(bool player)
    {
        if (player)
        {
            leftPlayer.transform.Find("Fog").gameObject.SetActive(true);

            yield return new WaitForSeconds(2.0f);

            leftPlayer.transform.Find("Fog").gameObject.SetActive(false);
        }
        else
        {
            rightPlayer.transform.Find("Fog").gameObject.SetActive(true);

            yield return new WaitForSeconds(2.0f);

            rightPlayer.transform.Find("Fog").gameObject.SetActive(false);
        }
    }

    
}
