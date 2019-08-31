using System;
using System.Numerics;
using NEThack.Classes;
using NEThack.Utilities;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace NEThack.Cheats
{
    class SalentAim
    {
        public static void Salent()
        {
            Entity TargetPlayer = default;
            Entity AimTargetPlayer = default;
            while (true)
            {
                if ((Memory.GetAsyncKeyState(Keys.VK_LBUTTON) & 1) > 0 || (Memory.GetAsyncKeyState(Keys.VK_XBUTTON1) & 1) > 0)
                {
                    if (Main.S.SALENT_AIM)
                    {
                        //TargetPlayer = default;
                        foreach (Entity Player in G.EntityList)
                        {
                            TargetPlayer = Player;
                            if (TargetPlayer.Valid && !TargetPlayer.IsTeammate)
                            {
                                AimTargetPlayer = Tools.GetFovPlayer(Main.S.AIM_FOV);
                            }
                        }
                        if (AimTargetPlayer != default)
                        {
                            if (AimTargetPlayer.SpottedByMask || AimTargetPlayer.Spotted)
                            {
                                if (AimTargetPlayer.EntityBase != G.Engine.LocalPlayer.EntityBase)
                                {
                                    if (AimTargetPlayer.Valid && !AimTargetPlayer.IsTeammate)
                                    {
                                        //G.Engine.SendPackets(false);
                                        float EnemyX = AimTargetPlayer.HeadPosition.X;
                                        float EnemyY = AimTargetPlayer.HeadPosition.Y;
                                        float EnemyZ = AimTargetPlayer.Position.Z;

                                        float LocalPlayerX = G.Engine.LocalPlayer.Position.X;
                                        float LocalPlayerY = G.Engine.LocalPlayer.Position.Y;
                                        float LocalPlayerZ = G.Engine.LocalPlayer.Position.Z;

                                        Vector2 angel = Tools.CalcAngle(new Vector3(LocalPlayerX, LocalPlayerY, LocalPlayerZ), new Vector3(EnemyX, EnemyY, EnemyZ));
                                        Vector3 salent = new Vector3(G.Engine.ViewAngles.X, G.Engine.ViewAngles.Y, 0f);

                                        G.Engine.Angle(new Vector3(angel.X, angel.Y, 0f));
                                        G.Engine.Shoot();
                                        G.Engine.Shoot();
                                        if (!Main.S.SALENT_NOTBACK)
                                            G.Engine.Angle(new Vector3(salent.X, salent.Y, 0f));
                                        //G.Engine.SendPackets(true);
                                    }
                                }
                            }
                        }
                        G.Engine.Shoot();
                    }
                }
                if (!Main.S.SALENT_AIM)
                {
                    Thread.Sleep(5000);
                }
            }
        }
    }
}