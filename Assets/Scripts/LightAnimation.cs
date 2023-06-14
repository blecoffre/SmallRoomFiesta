using UnityEngine;
using MoreMountains.Feedbacks;
using Zenject;

public class LightAnimation : MonoBehaviour
{
    [SerializeField] private Light[] _discoLights = default;
    [SerializeField] private Light[] _darknessLights = default;
    
    [Inject] private MMF_Player _mainMMFPlayer = default;

    private void Awake()
    {
        MMFPlayerManagement();
        SwitchRoomAmbientLights(false);
    }

    private void MMFPlayerManagement()
    {
        _mainMMFPlayer.Events.OnComplete.AddListener(() => SwitchRoomAmbientLights(false));
    }

    public void SwitchRoomAmbientLights(bool enabled)
    {
        for(int i = 0; i < _discoLights.Length; i++)
        {
            _discoLights[i].enabled = enabled;
        }

        for(int j = 0; j < _darknessLights.Length; j++)
        {
            _darknessLights[j].enabled = !enabled;
        }
    }

    public void PlayLightAnimation()
    {
        SwitchRoomAmbientLights(true);
        for (int i = 0; i < _discoLights.Length; i++)
        {
            _discoLights[i].color = UnityEngine.Random.ColorHSV(); //Change color for more fun
        }

        _mainMMFPlayer.PlayFeedbacks();
    }
}
