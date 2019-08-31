using System;
using System.Numerics;
using NEThack.Classes;
using NEThack.Utilities;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace NEThack.Cheats
{
    class AimBot
    {
        public static void Aim()
        {
            Entity TargetPlayer = default;
            Entity AimTargetPlayer = default;
            while (true)
            {
                if (Main.S.AIM_AIM)
                {
                    if (!Main.S.SALENT_AIM)
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
                            if (AimTargetPlayer.EntityBase != G.Engine.LocalPlayer.EntityBase)
                            {
                                if (AimTargetPlayer.Valid && !AimTargetPlayer.IsTeammate)
                                {
                                    float EnemyX = AimTargetPlayer.HeadPosition.X;
                                    float EnemyY = AimTargetPlayer.HeadPosition.Y;
                                    float EnemyZ = AimTargetPlayer.Position.Z;

                                    float LocalPlayerX = G.Engine.LocalPlayer.Position.X;
                                    float LocalPlayerY = G.Engine.LocalPlayer.Position.Y;
                                    float LocalPlayerZ = G.Engine.LocalPlayer.Position.Z;

                                    Vector2 angel = Tools.CalcAngle(new Vector3(LocalPlayerX, LocalPlayerY, LocalPlayerZ), new Vector3(EnemyX, EnemyY, EnemyZ));
                                    G.Engine.Angle(new Vector3(angel.X, angel.Y, 0f));

                                    //float ViewAngleX = G.Engine.ViewAngles.X;

                                    //double scalemul = EnemyX - LocalPlayerX;
                                    //double length = Math.Sqrt(Math.Pow(EnemyX - LocalPlayerX, 2) + Math.Pow(EnemyY - LocalPlayerY, 2));
                                    //double result = Math.Acos(scalemul / length) * 180 / Math.PI;
                                    //if (EnemyY < LocalPlayerY)
                                    //{
                                    //    result *= -1;
                                    //}

                                    //double scalemulY = EnemyY - LocalPlayerY;
                                    //double lengthY = Math.Sqrt(Math.Pow(EnemyY - LocalPlayerY, 2) + Math.Pow(EnemyZ - LocalPlayerZ, 2));
                                    //double resultY = Math.Acos(scalemulY / lengthY) * 180 / Math.PI;

                                    //G.Engine.Test1(new Vector3((float)resultY, (float)result, 0f));
                                    //Tools.GetFovPlayer(5);

                                    //if (G.Engine.ViewAngles.X == (float)resultY)
                                    //{
                                    //    if (G.Engine.ViewAngles.Y == (float)result)
                                    //    {
                                    //        //G.Engine.Shoot();
                                    //        //Thread.Sleep(500);
                                    //    }
                                    //}

                                    //Vector3 salent = new Vector3(G.Engine.ViewAngles.Y, G.Engine.ViewAngles.X, 0f);
                                    //Vector3 NEsalent = new Vector3((float)result, (float)resultY, 0f);

                                    //G.Engine.Test1(new Vector3(NEsalent.Y, NEsalent.X, NEsalent.Z));

                                    //G.Engine.Shoot();
                                    //G.Engine.Test1(new Vector3(salent.Y, salent.X, salent.Z));

                                    //Thread.Sleep(500);
                                }
                            }
                        }
                    }
                }
                if (!Main.S.AIM_AIM)
                {
                    Thread.Sleep(5000);
                }
            }
        }
    }
}