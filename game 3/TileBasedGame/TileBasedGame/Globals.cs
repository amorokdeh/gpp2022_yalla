using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TileBasedGame
{
    public static class Globals
    {
        public static int Lives = 100;

        public static int Velocity = 120;

        public static int BigImageSize = 64;
        public static int VeryBigImageSize = 128;
        public static int NormalImageSize = 16;
        public static int MediumImageSize = 32;
        public static int Multiplier = 2;
        public static int BigMultiplier = 4;

        public static int LeftBoundary = 0;

        public static float BulletGap = 1.5f;
        public static float BulletPowerUp = 0.15f;
        public static int EnemyGap = 1;
        public static float AnimationGap = 0.15f;
        public static int AnimationPause = 5;

        public static int Reset = 0;

        public static int SmallTextSize = 20;
        public static int NormalTextSize = 30;
        public static int BigTextSize = 40;

        public static int AudioStep = 10;

        public const float Gravity = 600.0f;
        public const float JUMP_VELOCITY = -350.0f;


    }
}
