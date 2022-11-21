using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterSndPlayer : MonoBehaviour
{
    public List<AudioClip> WaterSnd, IceSnd, MistSnd;
    public AudioSource audioSource;
    Rigidbody2D _rigidbody;
    CharacterController _chara;
    private void Awake()
    {
        _chara = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Math.Abs(Math.Abs(_rigidbody.velocity.x) - _chara.MoveSpeed) <= 0.1f && _chara.State == CharacterModel.CharacterState.Walk)
        {
            if (!audioSource.isPlaying)
            {
                switch (_chara.Form)
                {
                    case CharacterModel.CharacterForm.Ice:
                        audioSource.clip = IceSnd[Random.Range(0, IceSnd.Count)];
                        break;
                    case CharacterModel.CharacterForm.Water:
                        audioSource.clip = WaterSnd[Random.Range(0, WaterSnd.Count)];
                        break;
                    case CharacterModel.CharacterForm.Mist:
                        audioSource.clip = MistSnd[Random.Range(0, MistSnd.Count)];
                        break;
                }
                audioSource.Play();
            }
        }
    }
}
