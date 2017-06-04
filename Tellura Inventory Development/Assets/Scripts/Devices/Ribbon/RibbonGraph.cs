using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RibbonGraph {
    private List<DevicePort>    providers;
    private List<DevicePort>    requesters;
    private List<DevicePort>    ribbons;

    public RibbonGraph() {
        providers   = new List<DevicePort>();
        requesters  = new List<DevicePort>();
        ribbons     = new List<DevicePort>();
    }

    public void AddProvider(DevicePort port) {
        providers.Add(port);
    }
    public void AddRequester(DevicePort port) {
        requesters.Add(port);
    }
    public void AddRibbon(DevicePort port) {
        ribbons.Add(port);
    }
    public void AddPort(DevicePort port) {
        if (port.portType == Keywords.Names.PORT_TYPE_PROVIDER) AddProvider(port);
        else if (port.portType == Keywords.Names.PORT_TYPE_REQUESTER) AddRequester(port);
        else if (port.portType == Keywords.Names.PORT_TYPE_RIBBON) AddRibbon(port);
        else Debug.Log("ERROR\nCould not add port, unknown type: " + port.portType);
    }


    /// <summary>
    /// Merges another graph into this one.
    /// </summary>
    /// <param name="graph">Graph to be consumed.</param>
    public void mergeGraph(RibbonGraph graph) {
        foreach (DevicePort port in graph.providers) {
            providers.Add(port);
            port.graph = this;
        }
        foreach (DevicePort port in graph.requesters) {
            requesters.Add(port);
            port.graph = this;
        }
        foreach (DevicePort port in graph.ribbons) {
            ribbons.Add(port);
            port.graph = this;
        }
    }

    private void DijkstraFromProvider(DevicePort source) {
        List<DevicePort> vertexSet = new List<DevicePort>();
        vertexSet.AddRange(providers);
        vertexSet.AddRange(requesters);
        vertexSet.AddRange(ribbons);

        List<DevicePort> unvisitedSet = new List<DevicePort>();
        unvisitedSet.AddRange(vertexSet);

        foreach (DevicePort port in vertexSet) {
            port.dijkstraDistance = int.MaxValue;
            port.dijkstraPrevious = null;
        }
        source.dijkstraDistance = 0;

        // Loop
        while (unvisitedSet.Count > 0 ) {
            DevicePort shortestPathVertex = unvisitedSet[0];
            foreach (DevicePort port in unvisitedSet) if (port.dijkstraDistance < shortestPathVertex.dijkstraDistance) shortestPathVertex = port;
            unvisitedSet.Remove(shortestPathVertex);

            if (shortestPathVertex.siblings != null) {
                foreach (DevicePort sibling in shortestPathVertex.siblings) {
                    int newDistance = shortestPathVertex.dijkstraDistance;
                    if (newDistance < sibling.dijkstraDistance) {
                        sibling.dijkstraDistance = newDistance;
                        sibling.dijkstraPrevious = shortestPathVertex;
                    }
                }
            }

            if (shortestPathVertex.companion != null) {
                int newDistance = shortestPathVertex.dijkstraDistance + 1;
                    if (newDistance < shortestPathVertex.companion.dijkstraDistance) {
                        shortestPathVertex.companion.dijkstraDistance = newDistance;
                        shortestPathVertex.companion.dijkstraPrevious = shortestPathVertex;
                    }
            }
        }
    }

    public string DEBUGGraphReport(DevicePort source) {
        string returnString = "GRAPH REPORT from " + source.parentDevice.gameObject.name + "\nPROVIDERS \n";
        foreach (DevicePort port in providers) {
            returnString += ("\t" + port.parentDevice.gameObject.name + " as " + port.portType + "\n");
        }
        returnString += "REQUESTERS \n";
        foreach (DevicePort port in requesters) {
            returnString += ("\t" + port.parentDevice.gameObject.name + " as " + port.portType + "\n");
        }
        returnString += "RIBBONS \n";
        foreach (DevicePort port in ribbons) {
            returnString += ("\t" + port.parentDevice.gameObject.name + " as " + port.portType + "\n");
        }
        return returnString;
    }

    public string DEBUGDrawRout(Device source) {
        return null;
    }
}