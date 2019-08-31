using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = System.Drawing.Color;
using System.Drawing;

namespace NEThack.Classes
{
    public class Settings
    {
        public bool BunnyhopEnabled;
        //ESP//
        public bool ESP;
        public bool ESP_BOX;
        public bool ESP_LINE;
        public bool ESP_HP;
        public bool ESP_HP2;
        public bool ESP_WEAPON;
        public bool ESP_HEAD;
        public bool ESP_GLOW;
        public Color TeamColor;
        public Color EnemyColor;
        //AIM//
        public bool AIM_AIM;
        public int AIM_FOV;
        public bool SALENT_AIM;
        public bool SALENT_NOTBACK;
        public int TRIGGERBOT_FOV;
        public bool TRIGGERBOT;
        public int TRIGGERBOT_SLEEP;
    }
}
