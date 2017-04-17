using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RibbonNode {
    private List<Device>    providers;
    private List<Device>    requesters;
    private List<Device>    ribbons;

    public void addProvider(Device device) {
        providers.Add(device);
    }
    public void addRequester(Device device) {
        requesters.Add(device);
    }
    public void addRibbon(DeviceRibbon ribbon) {
        ribbons.Add(ribbon);
    }

    public void mergeNode(RibbonNode node) {
        foreach (Device device in node.providers) {
            providers.Add(device);
            if (device.nodeUp == node) device.nodeUp = this;
            if (device.nodeDn == node) device.nodeDn = this;
            if (device.nodeLt == node) device.nodeLt = this;
            if (device.nodeRt == node) device.nodeRt = this;
        }
        foreach (Device device in node.requesters) {
            requesters.Add(device);
            if (device.nodeUp == node) device.nodeUp = this;
            if (device.nodeDn == node) device.nodeDn = this;
            if (device.nodeLt == node) device.nodeLt = this;
            if (device.nodeRt == node) device.nodeRt = this;
        }
        foreach (DeviceRibbon ribbon in node.ribbons) {
            ribbons.Add(ribbon);
            if (ribbon.nodeUp == node) ribbon.nodeUp = this;
            if (ribbon.nodeDn == node) ribbon.nodeDn = this;
            if (ribbon.nodeLt == node) ribbon.nodeLt = this;
            if (ribbon.nodeRt == node) ribbon.nodeRt = this;
        }
    }
}