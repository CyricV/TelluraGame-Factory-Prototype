using System.Collections;
using System.Collections.Generic;

public class Grid {
    Device[,] deviceGrid;

    public Grid(int x = 100, int y = 100) {
        deviceGrid = new Device[x,y];
    }

    public Device getIndex(int x, int y) {
        return this.deviceGrid[x, y];
    }

    public void setIndex(int x, int y, Device device) {
        this.deviceGrid[x, y] = device;
    }
}
