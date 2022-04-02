using UnityEngine;
using UnityEngine.UI;

public class SoundsButton : MonoBehaviour
{
    public Image buttonImage;
    public Sprite enabledSprite;
    public Sprite disabledSprite;

    private void Start() 
    {
        MenuManager.instance.OnPauseEnabled += SpriteSetup;
    }

    private void OnDisable() 
    {
        MenuManager.instance.OnPauseEnabled -= SpriteSetup;
    }

    public void SpriteSetup()
    {
        if(AudioManager.instance.soundsEnabled)
        {
            buttonImage.sprite = enabledSprite;
        }
        else
        {
            buttonImage.sprite = disabledSprite;
        }
    }

    public void ButtonSwitch()
    {
        if(AudioManager.instance.soundsEnabled)
        {
            AudioManager.instance.DisableSounds();
        }
        else
        {
            AudioManager.instance.EnableSounds();
        }

        SpriteSetup();
    }
}
