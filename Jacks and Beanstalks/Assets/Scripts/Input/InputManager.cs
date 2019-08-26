using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    //True = L, False = R;
    public bool LR;

    private int growth_count;
    public bool smile_sun, sad_sun, rain, fog, lightning, bomb;
    private int L_count, R_count, Up_count;

    public int lightningCount;

    public GameManager gameManager;

    public InputManager()
    {
        growth_count = 0;//한칸 성장 100필요 기본 1회 입력시 2증가

        smile_sun = false;
        sad_sun = false;
        rain = false;
        fog = false;
        lightning = false;
        bomb = false;

        L_count = 0;
        R_count = 0;
        Up_count = -1;
    }
    
    public void PlusGrowth()//성장변수 1씩증가
    {
        if (!lightning)
        {
            if (smile_sun)
            {
                growth_count += 4;
            }

            else if (sad_sun)
            {
                growth_count += 1;
            }
            else
            {
                growth_count += 2;
            }

            if (growth_count >= gameManager.growPoint)
            {
                ResetGrowth();
                PlusUpCount();
                Up();

                if (LR)
                {
                    if (GetUpCount() >= 7)
                    {
                        gameManager.GameOver(true);
                    }
                    else
                    {
                        gameManager.LReach[Up_count, 2] = true;
                    }
                }
                else
                {
                    if (GetUpCount() >= 7)
                    {
                        gameManager.GameOver(false);
                    }
                    else
                    {
                        gameManager.RReach[Up_count, 2] = true;
                    }
                }

                gameManager.draw();
            }
        }

        else
        {
            lightningCount--;

            if (lightningCount <= 0)
            {
                lightning = false;
            }
        }
    }
    public void ResetGrowth()//성장변수 0으로 리셋
    {
        growth_count = 0;
    }
    public int GetGrowth()//성장변수 값알려주기
    {
        return growth_count;
    }
    
    public void PlusUpCount()//1회 올라가기
    {
        Up_count++;
    }
    public int GetUpCount()//몇번 올라갔는지
    {
        return Up_count;
    }

    public int GetLeftCount()//왼쪽 몇회
    {
         return L_count; 
    }
    public void PlusLeftCount()//왼쪽 1회 추가
    {
        if (!lightning)
        {
            if (Up_count >= 0)
            {
                L_count++;

                if (smile_sun)
                {
                    L_count += 4;
                }

                else if (sad_sun)
                {
                    L_count += 1;
                }
                else
                {
                    L_count += 2;
                }

                if (L_count >= (gameManager.growPoint / 2))
                {

                    if (LR)
                    {
                        if (gameManager.LReach[Up_count, 1])
                        {
                            gameManager.LReach[Up_count, 0] = true;

                            if (gameManager.LItemLoc[Up_count, 0])
                            {
                                gameManager.GetRandomItem(true);
                            }
                        }
                        else
                        {
                            gameManager.LReach[Up_count, 1] = true;
                        }
                    }
                    else
                    {
                        if (gameManager.RReach[Up_count, 1])
                        {
                            gameManager.RReach[Up_count, 0] = true;

                            if (gameManager.RItemLoc[Up_count, 0])
                            {
                                gameManager.GetRandomItem(false);
                            }
                        }
                        else
                        {
                            gameManager.RReach[Up_count, 1] = true;
                        }
                    }

                    ResetLeftCount();

                    gameManager.draw();
                }
            }
        }

        else
        {
            lightningCount--;

            if (lightningCount <= 0)
            {
                lightning = false;
            }
        }
    }  
    public void ResetLeftCount()//왼쪽 reset
    {
        L_count = 0;
    }

    public int GetRightCount()//오른쪽 몇회
    {
        return R_count;
    }
    public void PlusRightCount()//오른쪽 1회 추가
    {
        if (!lightning)
        {
            if (Up_count >= 0)
            {
                R_count++;

                if (smile_sun)
                {
                    R_count += 4;
                }

                else if (sad_sun)
                {
                    R_count += 1;
                }
                else
                {
                    R_count += 2;
                }

                if (R_count >= (gameManager.growPoint / 2))
                {

                    if (LR)
                    {
                        if (gameManager.LReach[Up_count, 3])
                        {
                            gameManager.LReach[Up_count, 4] = true;

                            if (gameManager.LItemLoc[Up_count, 4])
                            {
                                gameManager.GetRandomItem(true);
                            }
                        }
                        else
                        {
                            gameManager.LReach[Up_count, 3] = true;
                        }
                    }
                    else
                    {
                        if (gameManager.RReach[Up_count, 3])
                        {
                            gameManager.RReach[Up_count, 4] = true;

                            if (gameManager.RItemLoc[Up_count, 4])
                            {
                                gameManager.GetRandomItem(false);
                            }
                        }
                        else
                        {
                            gameManager.RReach[Up_count, 3] = true;
                        }
                    }

                    ResetRightCount();

                    gameManager.draw();
                }
            }
        }

        else
        {
            lightningCount--;

            if (lightningCount <= 0)
            {
                lightning = false;
            }
        }
    }
    public void ResetRightCount()//오른쪽 reset
    {
        R_count = 0;
    }

    public void Up()//성장하면 왼쪽과 오른쪽 카운트 0
    {
        L_count = 0;
        R_count = 0;
    }

    public void hitByLightning()
    {
        if (!rain)
        {
            lightningCount = 10;
        }
        else
        {
            rain = false;
        }
    }
   
    
    /*
    public bool Smile_Sun { get { return smile_sun; } set { smile_sun = Smile_Sun; } }//웃는 해
    public bool Sad_Sun { get { return sad_sun; } set { sad_sun = Sad_Sun; } }//찡그린 해
    public bool Rain { get { return rain; } set { rain = Rain; } }//비구름
    public bool Fog { get { return fog; } set { fog = Fog; } }//안개
    public bool Lightning { get { return lightning; } set { lightning = Lightning; } }//번개
    public bool Bomb { get { return bomb; } set { bomb = Bomb; } }//폭탄
    */
}
