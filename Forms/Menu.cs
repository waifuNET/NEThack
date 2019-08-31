using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NEThack.Cheats;
using NEThack.Classes;
using NEThack.Utilities;
using System.Runtime.InteropServices;

namespace NEThack
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();

            if (Main.RunStartup())
            {
                OffsetUpdater.UpdateOffsets();
                #region Start Threads
                // found the process and everything, lets start our cheats in a new thread
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    CheckMenu();
                }).Start();

                Tools.InitializeGlobals();

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Bunnyhop.Run();
                }).Start();

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Visuals v = new Visuals();
                    v.Initialize();
                    v.Run();
                }).Start();
                #endregion
                #region HACK
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    AimBot.Aim();
                }).Start();

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    SalentAim.Salent();
                }).Start();

                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    Waifu.Cheats.TriggerBot.triggerBot();
                }).Start();
                #endregion
            }
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            TopMost = true; // make this hover over the game, can remove if you want
            Appy_Click(sender, e);
        }

        public void CheckMenu()
        {
            bool insert = true;

            while (true)
            {
                if ((Memory.GetAsyncKeyState(Keys.VK_INSERT) & 1) > 0)
                {
                    if (insert)
                        BeginInvoke(new Action(() => this.WindowState = FormWindowState.Minimized));
                    if (!insert)
                    {
                        BeginInvoke(new Action(() => this.WindowState = FormWindowState.Maximized));
                        BeginInvoke(new Action(() => this.Activate()));
                    }

                    insert = !insert;
                }
                Thread.Sleep(50); // Greatly reduces cpu usage
                if ((Memory.GetAsyncKeyState(Keys.VK_XBUTTON2) & 1) > 0)
                {
                    BeginInvoke(new Action(() => SALENT_AIM.Checked = !SALENT_AIM.Checked));
                    Main.S.SALENT_AIM = !Main.S.SALENT_AIM;
                }
                Thread.Sleep(50); // Greatly reduces cpu usage
            }
        }

        private void Appy_Click(object sender, EventArgs e)
        {
            Main.S.BunnyhopEnabled = BunnyhopCheck.Checked;

            Main.S.ESP = ESPCheck.Checked;
            Main.S.ESP_BOX = ESP_BOX.Checked;
            Main.S.ESP_LINE = ESP_LINE.Checked;
            Main.S.ESP_HP = ESP_HP.Checked;
            Main.S.ESP_HP2 = ESP_HP2.Checked;
            Main.S.ESP_WEAPON = ESP_WEAPON.Checked;
            Main.S.ESP_HEAD = ESP_HEAD.Checked;

            Main.S.SALENT_AIM = SALENT_AIM.Checked;

            Main.S.AIM_AIM = AIM_AIM.Checked;
            Main.S.SALENT_NOTBACK = SALENT_AIM_NOTBACK.Checked;
            Main.S.TRIGGERBOT = TRIGGERBOT.Checked;

            Main.S.ESP_GLOW = ESP_GLOW.Checked;
            BeginInvoke(new Action(() => Main.S.TeamColor = Color.FromArgb(Int32.Parse(TEAM_A.Text), Int32.Parse(TEAM_R.Text), Int32.Parse(TEAM_G.Text), Int32.Parse(TEAM_B.Text))));
            BeginInvoke(new Action(() => Main.S.EnemyColor = Color.FromArgb(Int32.Parse(ENEMY_A.Text), Int32.Parse(ENEMY_R.Text), Int32.Parse(ENEMY_G.Text), Int32.Parse(ENEMY_B.Text))));

            if (Main.S.AIM_AIM)
            {
                if (Main.S.AIM_FOV >= 1)
                    BeginInvoke(new Action(() => Main.S.AIM_FOV = Int32.Parse(AIM_FOV.Text)));
                else
                    Main.S.AIM_FOV = 2;
            }
            if (Main.S.SALENT_AIM)
            {
                if (Main.S.AIM_FOV >= 1)
                    BeginInvoke(new Action(() => Main.S.AIM_FOV = Int32.Parse(AIM_FOV.Text)));
                else
                    Main.S.AIM_FOV = 2;
            }
            if (TRIGGERBOT.Checked)
            {
                if (Main.S.TRIGGERBOT_FOV >= 1)
                    BeginInvoke(new Action(() => Main.S.TRIGGERBOT_FOV = Int32.Parse(TRIGGERBOT_FOV.Text)));
                else
                    Main.S.TRIGGERBOT_FOV = 2;
            }
            if (TRIGGERBOT.Checked)
            {
                if (Main.S.TRIGGERBOT_SLEEP >= 1)
                    BeginInvoke(new Action(() => Main.S.TRIGGERBOT_SLEEP = Int32.Parse(TRIGGERBOTDELEY.Text)));
                else
                    Main.S.TRIGGERBOT_SLEEP = 2;
            }
        }
    }
}
