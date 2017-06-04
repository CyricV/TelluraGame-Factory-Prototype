using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Keywords;

public class DevicePort {
    public  RibbonGraph     graph;
    public  string          portType { get { return _portType; } }
    private string          _portType;
    /// <summary>
    /// The port this one is married to. Distance of 1.
    /// </summary>
    public  DevicePort      companion {
        get { return _companion; }
        set {
            SetCompanion(value);
        }
    }
    private DevicePort      _companion;
    /// <summary>
    /// Ports connected to this one internally on the same device, with a distance of 0 between them.
    /// </summary>
    public List<DevicePort> siblings;
    /// <summary>
    /// The device to which this port belongs.
    /// </summary>
    public  Device parentDevice { get { return _parentDevice; } }
    private Device _parentDevice;

    public int          dijkstraDistance;
    public DevicePort   dijkstraPrevious;

    public DevicePort(Device parentDevice, string portType = null, DevicePort companion = null, List<DevicePort> siblings = null) {
        _parentDevice           = parentDevice;
        graph                   = null;
        _portType               = portType;
        _companion              = companion;
        if (siblings == null) {
            this.siblings       = new List<DevicePort>();
        } else this.siblings    = siblings;
    }

    private void SetCompanion(DevicePort companionPort) {
        if (companionPort == null) {
            // Split the graph at this port.
            return;
        }
        if (graph == null && companionPort.graph == null) {
            // Create a new graph.
            graph = new RibbonGraph();
            graph.AddPort(this);
            foreach (DevicePort siblingPort in siblings) {
                graph.AddPort(siblingPort);
                siblingPort.graph = graph;
            }
            _companion = companionPort;
            companionPort.companion = this;
            Debug.Log(graph.DEBUGGraphReport(this));
        } else if (graph == companionPort.graph) {
            // Just recompute paths.
            _companion = companionPort;
            // Escape from set cycle
            if (companionPort.companion != this) companionPort.companion = this;
            Debug.Log(graph.DEBUGGraphReport(this));
        } else if (graph == null && companionPort.graph != null) {
            // Join companionPort's graph.
            graph = companionPort.graph;
            graph.AddPort(this);
            foreach (DevicePort siblingPort in siblings) {
                graph.AddPort(siblingPort);
                siblingPort.graph = graph;
            }
            _companion = companionPort;
            companionPort.companion = this;
            Debug.Log(graph.DEBUGGraphReport(this));
        } else if (graph != null && companionPort.graph == null) {
            // Just set _companion, companionPort will join this graph.
            _companion = companionPort;
            companionPort.companion = this;
            Debug.Log(graph.DEBUGGraphReport(this));
        } else {
            // Merge this port's graph into companionPort's
            _companion = companionPort;
            companionPort.graph.mergeGraph(graph);
            companionPort.companion = this;
            Debug.Log(graph.DEBUGGraphReport(this));
        }
    }

    public void RemoveSibling() {

    }

    private void AddSibling() {

    }

}
