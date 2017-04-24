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
            if (device.graphUp == graph) device.graphUp = this;
            if (device.graphDn == graph) device.graphDn = this;
            if (device.graphLt == graph) device.graphLt = this;
            if (device.graphRt == graph) device.graphRt = this;
        }
        foreach (Device device in graph.requesters) {
            requesters.Add(device);
            if (device.graphUp == graph) device.graphUp = this;
            if (device.graphDn == graph) device.graphDn = this;
            if (device.graphLt == graph) device.graphLt = this;
            if (device.graphRt == graph) device.graphRt = this;
        }
        foreach (DeviceRibbon ribbon in graph.ribbons) {
            ribbons.Add(ribbon);
            if (ribbon.graphUp == graph) ribbon.graphUp = this;
            if (ribbon.graphDn == graph) ribbon.graphDn = this;
            if (ribbon.graphLt == graph) ribbon.graphLt = this;
            if (ribbon.graphRt == graph) ribbon.graphRt = this;
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