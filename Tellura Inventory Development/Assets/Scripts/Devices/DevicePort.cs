using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Keywords;

public class DevicePort {
    public RibbonGraph  graph;
    public string       portType { get { return _portType; } }
    private string      _portType;

    public DevicePort(string portType = null) {
        graph           = null;
        _portType       = portType;
        if (_portType != Keywords.Names.PORT_TYPE_PROVIDER ||
            _portType != Keywords.Names.PORT_TYPE_REQUESTER) {
            _portType   = null;
        }
    }
}
