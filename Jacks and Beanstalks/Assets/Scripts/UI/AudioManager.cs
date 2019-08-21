using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /*
    사용법
    AudioManager.AudioPlay(음원 이름)으로 사용가능합니다.
    static 함수이므로 AudioManager를 Scene 내부에 배치할 필요는 없습니다.
    사용하실 음원은 Resources에 넣어주시고, _soundName에 음원의 이름을 넣어 사용해주세요.
    */
    public static void AudioPlay(string _soundName, bool isLoop = false)
    {
        AudioSource _audio = new GameObject().AddComponent<AudioSource>();
        _audio.gameObject.name = "Sound Effect Player";
        _audio.clip = Resources.Load("Sound/" + _soundName) as AudioClip;
        _audio.loop = isLoop;
        _audio.Play();
        GameObject.Destroy(_audio.gameObject, _audio.clip.length);
    }

    /* 임시 함수 */
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            AudioPlay("Boom_C_02_8-Bit_11025Hz 1");
        }
        else if(Input.GetMouseButtonDown(1))
        {
            AudioPlay("Blip_C_10");
        }
    }
}