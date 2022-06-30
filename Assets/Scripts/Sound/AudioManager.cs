using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; // ������ �̸�.
    private AudioSource source; // ���� �÷��̾�

    public AudioClip clip; // ���� ����

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
    public AudioSource[] sfxPlayer =  null; //��ü�� ȿ����.
    public AudioSource bgmPlayer = null;
  
    //�̱���
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


    /// PlayBGM �� �����ؼ� �ش� string������ ���� BGM�� ���ϰ� �����Ų��.

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




    /// PlaySFX �� �����ؼ� �ش� string������ ȿ������ ���ϰ� �����Ų��.

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
                    Debug.Log(" ��� ����� �÷��̾ ������̴�.");
                    return;
                }
            }
            Debug.Log(p_sfxName + "�̸��� �����ϴ�.");
        }
    }

}
