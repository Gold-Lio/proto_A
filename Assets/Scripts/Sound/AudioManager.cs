using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; // 사운드의 이름.
    private AudioSource source; // 사운드 플레이어

    public AudioClip clip; // 사운드 파일

    //public void SetSource(AudioSource _source)
    //{
    //    source = _source;
    //    source.clip = clip;
    //    source.loop = loop;
    //    source.volume = volume;
    //}

    //public void SetVolumn()
    //{
    //    source.volume = volume;
    //}

    //public void Play()
    //{
    //    source.Play();
    //}

    //public void Stop()
    //{
    //    source.Stop();
    //}

    //public void SetLoop()
    //{
    //    source.loop = true;
    //}

    //public void SetLoopCancel()
    //{
    //    source.loop = false;
    //}
}

public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;

    [SerializeField]   
    Sound[] sfx = null;
    [SerializeField]
    Sound[] bgm = null;
   

    [SerializeField]
    public AudioSource[] sfxPlayer =  null; //전체의 효과음.
    public AudioSource bgmPlayer = null;
  
    //싱글턴
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }


    /// PlayBGM 에 접근해서 해당 string값으로 현재 BGM을 정하고 재생시킨다.

    public void PlayBGM(string p_bgmName)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (p_bgmName == bgm[i].name)
            {
                {
                    bgmPlayer.clip = bgm[i].clip;
                    bgmPlayer.Play();
                }
            }
        }
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }




    /// PlaySFX 에 접근해서 해당 string값으로 효과음을 정하고 재생시킨다.

    public void PlaySFX(string p_sfxName)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (p_sfxName == bgm[i].name)
            {
                {
                    for (int x = 0; x < sfxPlayer.Length; x++)
                    {
                        if (!sfxPlayer[x].isPlaying)
                        {
                            sfxPlayer[x].clip = sfx[i].clip;
                            sfxPlayer[x].Play();
                            return;
                        }
                    }
                    Debug.Log(" 모든 오디오 플레이어가 재생중이다.");
                    return;
                }
            }
            Debug.Log(p_sfxName + "이름이 없습니다.");
        }
    }

}
