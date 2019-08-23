
public class InputManager
{
    private int growth_count;
    private bool smile_sun, sad_sun, rain, fog, lightning, bomb;
    private int L_count, R_count, Up_count;

    public InputManager()
    {
        growth_count = 0;

        smile_sun = false;
        sad_sun = false;
        rain = false;
        fog = false;
        lightning = false;
        bomb = false;

        L_count = 0;
        R_count = 0;
        Up_count = 0;
    }
    

    public void PlusGrowth()//성장변수 1씩증가
    {
        growth_count++;
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
        L_count++;
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
        R_count++;
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


    public bool Smile_Sun { get { return smile_sun; } set { smile_sun = Smile_Sun; } }//웃는 해
    public bool Sad_Sun { get { return sad_sun; } set { sad_sun = Sad_Sun; } }//찡그린 해
    public bool Rain { get { return rain; } set { rain = Rain; } }//비구름
    public bool Fog { get { return fog; } set { fog = Fog; } }//안개
    public bool Lightning { get { return lightning; } set { lightning = Lightning; } }//번개
    public bool Bomb { get { return bomb; } set { bomb = Bomb; } }//폭탄
}
