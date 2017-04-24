using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevicePort {
    public int direction{
        get { return _direction; }
        set {
            value = Mathf.Abs(value);
            if (value <= 45)                        _direction = 0;
            else if (45 > value && value <= 135)    _direction = 90;
            else if (135 > value && value <= 225)   _direction = 180;
            else if (225 > value)                   _direction = 270;
        }
    }
    private int _direction;
    public RibbonGraph graph;
    public bool enabled;

    public DevicePort(int direction) {
        this.direction  = direction;
        graph           = null;
        enabled         = false;
    }
}
