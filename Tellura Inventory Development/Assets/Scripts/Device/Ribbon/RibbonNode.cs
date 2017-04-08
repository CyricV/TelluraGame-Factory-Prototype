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
            if (device.upNode == node) device.upNode = this;
            if (device.downNode == node) device.downNode = this;
            if (device.leftNode == node) device.leftNode = this;
            if (device.rightNode == node) device.rightNode = this;
        }
        foreach (Device device in node.requesters) {
            requesters.Add(device);
            if (device.upNode == node) device.upNode = this;
            if (device.downNode == node) device.downNode = this;
            if (device.leftNode == node) device.leftNode = this;
            if (device.rightNode == node) device.rightNode = this;
        }
        foreach (DeviceRibbon ribbon in node.ribbons) {
            ribbons.Add(ribbon);
            if (ribbon.upNode == node) ribbon.upNode = this;
            if (ribbon.downNode == node) ribbon.downNode = this;
            if (ribbon.leftNode == node) ribbon.leftNode = this;
            if (ribbon.rightNode == node) ribbon.rightNode = this;
        }
    }
}