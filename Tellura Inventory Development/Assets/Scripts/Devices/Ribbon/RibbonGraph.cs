using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RibbonGraph {
    private List<Device>    providers;
    private List<Device>    requesters;
    private List<Device>    ribbons;

    public RibbonGraph() {
        providers   = new List<Device>();
        requesters  = new List<Device>();
        ribbons     = new List<Device>();
    }

    public void addProvider(Device device) {
        providers.Add(device);
    }
    public void addRequester(Device device) {
        requesters.Add(device);
    }
    public void addRibbon(DeviceRibbon ribbon) {
        ribbons.Add(ribbon);
    }

    /// <summary>
    /// Merges another graph into this one.
    /// </summary>
    /// <param name="graph">Graph to be consumed.</param>
    public void mergeGraph(RibbonGraph graph) {
        foreach (Device device in graph.providers) {
            providers.Add(device);
            if (device.portUp.graph == graph) device.portUp.graph = this;
            if (device.portDn.graph == graph) device.portDn.graph = this;
            if (device.portLt.graph == graph) device.portLt.graph = this;
            if (device.portRt.graph == graph) device.portRt.graph = this;
        }
        foreach (Device device in graph.requesters) {
            requesters.Add(device);
            if (device.portUp.graph == graph) device.portUp.graph = this;
            if (device.portDn.graph == graph) device.portDn.graph = this;
            if (device.portLt.graph == graph) device.portLt.graph = this;
            if (device.portRt.graph == graph) device.portRt.graph = this;
        }
        foreach (DeviceRibbon ribbon in graph.ribbons) {
            ribbons.Add(ribbon);
            if (ribbon.portUp.graph == graph) ribbon.portUp.graph = this;
            if (ribbon.portDn.graph == graph) ribbon.portDn.graph = this;
            if (ribbon.portLt.graph == graph) ribbon.portLt.graph = this;
            if (ribbon.portRt.graph == graph) ribbon.portRt.graph = this;
        }
    }

    private void DijkstraFromProvider(Device source) {
        List<Device> vertexSet = new List<Device>();
        vertexSet.AddRange(providers);
        vertexSet.AddRange(requesters);
        vertexSet.AddRange(ribbons);

        List<Device> unvisitedSet = new List<Device>();
        unvisitedSet.AddRange(vertexSet);

        foreach (Device device in vertexSet) {
            device.dijkstraDistance = int.MaxValue;
            device.dijkstraPrevious = null;
        }
        source.dijkstraDistance = 0;

        // Loop
        while (vertexSet.Count > 0 ) {
            Device shortestPathVertex = unvisitedSet[0];
            foreach (Device device in unvisitedSet) if (device.dijkstraDistance < shortestPathVertex.dijkstraDistance) shortestPathVertex = device;
            unvisitedSet.Remove(shortestPathVertex);
            if (shortestPathVertex.connectedUp && unvisitedSet.Contains(shortestPathVertex.neighborUp)) {
                int newDistance = shortestPathVertex.dijkstraDistance + 1;
                if (newDistance < shortestPathVertex.neighborUp.dijkstraDistance) {
                    shortestPathVertex.neighborUp.dijkstraDistance = newDistance;
                    shortestPathVertex.neighborUp.dijkstraPrevious = shortestPathVertex;
                }
            }
            if (shortestPathVertex.connectedDn && unvisitedSet.Contains(shortestPathVertex.neighborDn)) {
                int newDistance = shortestPathVertex.dijkstraDistance + 1;
                if (newDistance < shortestPathVertex.neighborDn.dijkstraDistance) {
                    shortestPathVertex.neighborDn.dijkstraDistance = newDistance;
                    shortestPathVertex.neighborDn.dijkstraPrevious = shortestPathVertex;
                }
            }
            if (shortestPathVertex.connectedLt && unvisitedSet.Contains(shortestPathVertex.neighborLt)) {
                int newDistance = shortestPathVertex.dijkstraDistance + 1;
                if (newDistance < shortestPathVertex.neighborLt.dijkstraDistance) {
                    shortestPathVertex.neighborLt.dijkstraDistance = newDistance;
                    shortestPathVertex.neighborLt.dijkstraPrevious = shortestPathVertex;
                }
            }
            if (shortestPathVertex.connectedRt && unvisitedSet.Contains(shortestPathVertex.neighborRt)) {
                int newDistance = shortestPathVertex.dijkstraDistance + 1;
                if (newDistance < shortestPathVertex.neighborRt.dijkstraDistance) {
                    shortestPathVertex.neighborRt.dijkstraDistance = newDistance;
                    shortestPathVertex.neighborRt.dijkstraPrevious = shortestPathVertex;
                }
            }
        }
    }

    public string DEBUGDrawRout(Device source) {

        return null;
    }
}