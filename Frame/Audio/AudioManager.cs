using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class AudioManager
{
    /// <summary>
    /// ���ִ�С
    /// </summary>
    public float musicVol = 0.5f;

    /// <summary>
    /// �����е���Ƶ�ֵ�
    /// </summary>
    public Dictionary<string, Sound> soundDic = new Dictionary<string, Sound>();

    /// <summary>
    /// ����������
    /// </summary>
    /// <param name="musicName"></param>
    /// <param name="clip"></param>
    /// <param name="isLoop"></param>
    /// <returns></returns>
    public Sound PlayMusic(string musicName, AudioClip clip, bool isLoop)
    {
        GameObject audioOb = new GameObject(musicName);
        audioOb.transform.SetParent(GameApp.Instance.transform);
        AudioSource source = audioOb.AddComponent<AudioSource>();
        source.loop = isLoop;
        source.volume = musicVol;
        Sound sound = new Sound(clip, source);
        soundDic.Add(musicName, sound);
        return sound;
    }

    /// <summary>
    /// ������ͣ/�ָ�
    /// </summary>
    /// <param name="musicName"></param>
    /// <returns></returns>
    public Sound PauseMusic(string musicName, bool b)
    {
        Sound _sound;
        if (soundDic.TryGetValue(musicName, out _sound))
        {
            _sound.playing = b;
        }
        return _sound;
    }

    /// <summary>
    /// ����ֹͣ
    /// </summary>
    /// <param name="musicName"></param>
    public void StopMusic(string musicName)
    {
        Sound _sound;
        if (soundDic.TryGetValue(musicName, out _sound))
        {
            _sound.Finish();
        }
    }

    public void Update()
    {
        for (int i = 0; i < soundDic.Count; i++)
        {
            Sound _sound = soundDic.Values.ElementAt(i);
            string _key = soundDic.Keys.ElementAt(i);
            _sound.Update();
            if (_sound.source == null)
            {
                soundDic.Remove(_key);
            }
        }
    }

    /// <summary>
    /// ��������(ֻ�ܴ���һ����������ѭ���ģ������µĻ��Զ��滻�ɵ�)
    /// </summary>
    private AudioSource bgmSource;

    /// <summary>
    /// BGM��С
    /// </summary>
    public float bgmVol = 0.5f;

    /// <summary>
    /// ���ű�������
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="isLoop"></param>
    public void PlayBGM(AudioClip clip, bool isLoop)
    {
        bgmSource.clip = clip;
        bgmSource.loop = isLoop;
        bgmSource.volume = bgmVol;
        bgmSource.Play();
    }

    /// <summary>
    /// ��ͣ/�ָ���������
    /// </summary>
    public void PauseBGM()
    {
        if (bgmSource.clip == null)
        {
            Debug.LogWarning("û�����ñ�������");
            return;
        }
        if (bgmSource.isPlaying)
        {
            bgmSource.Pause();
        }
        else
        {
            bgmSource.UnPause();
        }
    }

    /// <summary>
    /// ֹͣ��������
    /// </summary>
    public void StopBGM()
    {
        bgmSource.Stop();
    }
}