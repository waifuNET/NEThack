using System;
using System.Numerics;
using NEThack.Classes;
using NEThack.Utilities;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Waifu.Cheats
{
    class TriggerBot
    {
        public static void triggerBot()
        {
            Entity TargetPlayer = default;
            Entity AimTargetPlayer = default;

            while (true)
            {
                if (NEThack.Main.S.TRIGGERBOT)
                {
                    //TargetPlayer = default;
                    foreach (Entity Player in G.EntityList)
                    {
                        TargetPlayer = Player;
                        if (TargetPlayer.Valid && !TargetPlayer.IsTeammate)
                        {
                            AimTargetPlayer = Tools.GetFovPlayer(NEThack.Main.S.TRIGGERBOT_FOV);
                        }
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
                                G.Engine.Shoot();
                                Thread.Sleep(NEThack.Main.S.TRIGGERBOT_SLEEP);
                            }
                        }
                    }
                }
                if (!NEThack.Main.S.TRIGGERBOT)
                {
                    Thread.Sleep(5000);
                }
            }
        }
    }
}