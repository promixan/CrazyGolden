using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnergyHandler : MonoBehaviour
{
    public RawImage[] energyBatteries;
    
    private GameManager _gameManager;
    private const float DefaultDisabledEnergy = 0.5f;
    private const float DefaultEnabledEnergy = 1.0f;

    private void Start()
    {
        ServiceLocator.Register(this);
        ResetEnergy();
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister<ResultsHandler>();
    }

    public void ResetEnergy()
    {
        foreach (var battery in energyBatteries)
        {
            var color = battery.color;
            color.a = DefaultEnabledEnergy;
            battery.color = color;
        }
    }

    public int DecreaseEnergy()
    {
        var i = 0;
        foreach (var battery in energyBatteries)
        {
            i++;
            var color = battery.color;
            if (color.a < DefaultEnabledEnergy) continue;
            color.a = DefaultDisabledEnergy;
            battery.color = color;
            break;
        }
        return energyBatteries.Length - i;
    }

    public void RestoreEnergy()
    {
        foreach (var battery in energyBatteries.Reverse())
        {
            var color = battery.color;
            if (!(color.a < DefaultEnabledEnergy))  continue;
            color.a = DefaultEnabledEnergy;
            battery.color = color;
            break;
        }
    }
}
