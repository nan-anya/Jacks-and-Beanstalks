using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioSource _bgmPlayer;
    private static AudioSource _BGMPlayer
    {
        get
        {
            if (!_bgmPlayer)
            {
                AudioSource[] list = FindObjectsOfType<AudioSource>();
                foreach(AudioSource instance in list)
                {
                    if (instance.name == "BGM Player")
                    {
                        _bgmPlayer = instance;
                        break;
                    }
                }
                if (!_bgmPlayer)
                {
                    _bgmPlayer = new GameObject().AddComponent<AudioSource>();
                    _bgmPlayer.name = "BGM Player";
                }
            }
            return _bgmPlayer;
        }
    }

    /*
    사용법
    AudioManager.AudioPlay(클립 이름)으로 사용가능합니다.
    static 함수이므로 AudioManager를 Scene 내부에 배치할 필요는 없습니다.
    사용할 클립을 매개변수로 넣어서 사용할 수 있어요.
    */
    public static AudioSource AudioPlay(AudioClip clip, bool isLoop = false)
    {
        AudioSource _audio = new GameObject().AddComponent<AudioSource>();
        _audio.name = "Sound Effect Player";
        _audio.clip = clip;
        _audio.loop = isLoop;
        _audio.Play();
        GameObject.Destroy(_audio.gameObject, _audio.clip.length);

        return _audio;
    }

    public static AudioSource BGMStart(AudioClip bgmClip, bool isLoop = true)
    {
        //BGMStop();
        _BGMPlayer.clip = bgmClip;
        _BGMPlayer.loop = isLoop;
        _BGMPlayer.Play();

        return _BGMPlayer;
    }

    public static void BGMStop()
    {
       _BGMPlayer.Stop();
    }

    /* 사용 예시 및 임시 함수 */
    public AudioClip beepP1;
    public AudioClip beepP2;
    public AudioClip bgmClip;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AudioPlay(beepP1);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            AudioPlay(beepP2);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BGMStart(bgmClip);
        }
    }
}