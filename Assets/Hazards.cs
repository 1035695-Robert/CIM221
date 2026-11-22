using System;
using System.Collections;
using Audio.Scripts;
using UnityEngine;

public class Hazards : MonoBehaviour
{
    public static Action<string> OnHazardClear;
    private bool isInDanger = false;

    private void OnEnable()
    {
        OnHazardClear += ClearHazards;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player_Model model))
        {
            model.moveSpeed /= 2;
            Music.OnMusicAudioEvent?.Invoke("Panic");
            PlayerAudio.OnPlayerAudioEvent?.Invoke("scared");
            Debug.Log("exited Zone");
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player_Model model))
        {
            model.ResetMoveSpeed();
            Music.OnMusicAudioEvent?.Invoke("Base");
        }
    }

    private IEnumerator Hurt()
    {
        isInDanger = true;
        while (isInDanger)
        {
            PlayerAudio.OnPlayerAudioEvent?.Invoke("hurt");
            yield return new WaitForSeconds(2f);
        }

        yield break;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        StartCoroutine(Hurt());
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        isInDanger = false;
    }

    private void ClearHazards(string obj)
    {
        gameObject.SetActive(false);
        Debug.Log("clear hazard");
        Sfx.OnSfxAudioEvent?.Invoke("");
    }
}