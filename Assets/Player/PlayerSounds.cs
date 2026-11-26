using Audio.Scripts;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public void Steps()
    {
        Sfx.OnSfxAudioEvent("footsteps");
    }
    public void Hurt()
    {
        PlayerAudio.OnPlayerAudioEvent("hurt");    
    }
}
