using System.Collections;
using UnityEngine;

namespace Tellura {
    public static class GameValues {
        public static float MAX_SCROLL_SPEED            { get { return 24; } }
        public static float MIN_SCROLL_SPEED            { get { return 2; } }
        public static float ZOOM_SCROLL_SPEED           { get { return 32; } }
        public static int SCROLL_ACTIVATE_WIDTH         { get { return 20; } }
        public static float MIN_CAMERA_HEIGHT           { get { return 2; } }
        public static float MAX_CAMERA_HEIGHT           { get { return 20; } }
        public static float MAX_CAMERA_SCROLL_HEIGHT    { get { return 32; } }
        public static float MAX_CAMERA_SCROLL_WIDTH     { get { return 32; } }

        public static int LM_BUILD_PLANE                { get { return 1<<8; } }
        public static int LM_DEVICE                     { get { return 1<<9; } }
    }
}
