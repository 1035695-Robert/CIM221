using Audio.Scripts;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public void Steps()
    {
        Sfx.OnSfxAudioEvent?.Invoke("footsteps");
    }
    public void Hurt()
    {
        PlayerAudio.OnPlayerAudioEvent?.Invoke("hurt");    
    }
}
