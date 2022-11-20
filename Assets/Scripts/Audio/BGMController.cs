using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMController : MonoBehaviour
{
    public static BGMController Active = null;
    public static bool PausedDueToBGM = false;
    public AudioClip bgm;
    private AudioSource source;
    private void Awake()
    {
        transform.parent = transform.root;
        source = GetComponent<AudioSource>();
        source.clip = bgm;
        if (Active != null)
        {
            if (Active.source.clip != bgm)
            {
                if (Loading.activeAnimator != null)
                    Loading.activeAnimator.SetFloat("speed", 0);
                PausedDueToBGM = true;
                Active.GetComponent<Animator>().Play("BGMVolumeFadeOut", 0, 0.0f);
                source.Stop();
                Active = this;
                DontDestroyOnLoad(this.gameObject);
                return;
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }
        }
        Active = this;
        DontDestroyOnLoad(this.gameObject);
        source.Play();
    }
    public void OnFadeOut()
    {
        Destroy(this.gameObject);
        PausedDueToBGM = false;
        if (Loading.activeAnimator != null)
            Loading.activeAnimator.SetFloat("speed", 1);
        Active.GetComponent<Animator>().Play("BGMVolumeAni", 0, 0.0f);
        Active.source.Play();
    }
}
