using Audio.Scripts;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public void Steps()
    {
        Debug.Log("Footsteps");
        Sfx.OnSfxAudioEvent("footsteps");
    }
}
