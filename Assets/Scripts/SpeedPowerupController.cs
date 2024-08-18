using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class SpeedPowerupController : basePowerupController
{
    protected override void Activate()
    {
        m_shipController.tempSpeedIncrease((float)m_config.multiplier, m_config.duration);
    }
}
