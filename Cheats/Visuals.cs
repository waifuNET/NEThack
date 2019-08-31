﻿using System;
using System.Numerics;
using GameOverlay.Drawing;
using GameOverlay.Windows;
using NEThack.Classes;
using NEThack.Utilities;
using System.Collections.Generic;
using Color = System.Drawing.Color;
using System.Drawing;

namespace NEThack.Cheats
{
    public class Visuals
    {
        #region dx shid
        private OverlayWindow _window;
        private GameOverlay.Drawing.Graphics _graphics;

        public object Globals { get; private set; }

        public Visuals()
        {
            _window = new OverlayWindow(Main.ScreenRect.left, Main.ScreenRect.top, Main.ScreenSize.Width, Main.ScreenSize.Height)
            {
                IsTopmost = true,
                IsVisible = true
            };
            _window.SizeChanged += _window_SizeChanged;
            _graphics = new GameOverlay.Drawing.Graphics()
            {
                MeasureFPS = true,
                Height = _window.Height,
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true,
                UseMultiThreadedFactories = false,
                VSync = true,
                Width = _window.Width,
                WindowHandle = IntPtr.Zero
            };
        }

        ~Visuals()
        {
            _graphics.Dispose();
            _window.Dispose();
        }

        public void Initialize()
        {
            _window.CreateWindow();
            _graphics.WindowHandle = _window.Handle; // set the target handle before calling Setup()
            _graphics.Setup();
        }

        private void _window_SizeChanged(object sender, OverlaySizeEventArgs e)
        {
            if (_graphics == null) return;

            if (_graphics.IsInitialized)
            {
                // after the Graphics surface is initialized you can only use the Resize method in order to enqueue a size change
                _graphics.Resize(e.Width, e.Height);
            }
            else
            {
                // otherwise just set its members
                _graphics.Width = e.Width;
                _graphics.Height = e.Height;
            }
        }
        #endregion
        public void Run()
        {
            #region things
            var gfx = _graphics;
            GameOverlay.Drawing.SolidBrush GetBrushColor(Color color)
            {
                return gfx.CreateSolidBrush(color.R, color.G, color.B, color.A);
            }
            #endregion
            #region Draw

            while (true)
            {
                gfx.BeginScene();
                gfx.ClearScene();
                // start drawings here

                DrawTextWithOutline("NEThack", 10, 5, 25, Color.DeepSkyBlue, Color.Black, true, true);
                if (Main.S.SALENT_AIM)
                    DrawText("Salent: ON", 1225, 5, 25, Color.Green);
                else
                    DrawText("Salent: OFF", 1225, 5, 25, Color.Red);

                if (Main.S.ESP)
                {
                    foreach (Entity Player in G.EntityList)
                    {
                        if (Player.EntityBase != G.Engine.LocalPlayer.EntityBase)
                        {
                            Vector2 Player2DPos = Tools.WorldToScreen(new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z - 5));
                            Vector2 Player2DHeadPos = Tools.WorldToScreen(new Vector3(Player.HeadPosition.X, Player.HeadPosition.Y, Player.HeadPosition.Z + 10));

                            if (!Tools.IsNullVector2(Player2DPos) && !Tools.IsNullVector2(Player2DHeadPos) && Player.Valid)
                            {
                                float BoxHeight = Player2DPos.Y - Player2DHeadPos.Y;
                                float BoxWidth = (BoxHeight / 2) * 1.25f; //little bit wider box

                                Color drawcolor;
                                if (Player.IsTeammate)
                                    drawcolor = Color.Blue;
                                else
                                    drawcolor = Color.Red;
                                #region Box
                                if (Main.S.ESP_BOX)
                                    DrawOutlineBox(Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y, BoxWidth, BoxHeight, drawcolor);
                                //DrawFillOutlineBox(Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y, BoxWidth, BoxHeight, drawcolor, Color.FromArgb(50, 198, 198, 198));
                                //DrawBoxEdge(Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y, BoxWidth, BoxHeight, drawcolor, 1);
                                #endregion
                                #region Health Bar
                                float Health = Player.Health;
                                Color HealthColor = Tools.HealthGradient(Tools.HealthToPercent((int)Health));
                                float x = Player2DPos.X - (BoxWidth / 2) - 8;
                                float y = Player2DHeadPos.Y;
                                float w = 4;
                                float h = BoxHeight;
                                float HealthHeight = (Health * h) / 100;

                                //G.Engine.Flash();

                                if (Main.S.ESP_GLOW)
                                    if (!Player.IsTeammate)
                                        Player.Glow(Main.S.EnemyColor);

                                //DrawText(Player.HeadPosition.ToString(), x, y - 60, 14, Color.Green);

                                if (Main.S.ESP_HEAD)
                                {
                                    float EnemyX = Player.HeadPosition.X;
                                    float EnemyY = Player.HeadPosition.Y;
                                    float EnemyZ = Player.Position.Z;

                                    Vector2 Player2DPosHEAD = Tools.WorldToScreen(new Vector3(EnemyX, EnemyY, EnemyZ - 5));
                                    Vector2 Player2DHeadPosHEAD = Tools.WorldToScreen(new Vector3(Player.HeadPosition.X, Player.HeadPosition.Y, Player.HeadPosition.Z + 10));

                                    BoxHeight = (Player2DPosHEAD.Y - Player2DHeadPosHEAD.Y - 2);
                                    BoxWidth = (BoxHeight / 2);

                                    DrawOutlineBox(Player2DPosHEAD.X - (BoxWidth / 6), Player2DHeadPosHEAD.Y, BoxWidth / 4, BoxHeight / 5, drawcolor);
                                }

                                if (Main.S.ESP_WEAPON)
                                {
                                    if ((h / 100) <= 0.8)
                                        DrawText(Player.WeaponName, x, y - (45 - 16), 12, Color.Purple);
                                    else
                                        DrawText(Player.WeaponName, x + 5, y - (45 - 11), 16, Color.Purple);
                                }

                                if (Main.S.ESP_HP)
                                {
                                    if ((h / 100) <= 0.8)
                                        DrawText("HP: " + Health, x, y - (30 - 16), 12, HealthColor);
                                    else
                                        DrawText("HP: " + Health, x + 5, y - (30 - 11), 16, HealthColor);
                                }

                                if (Main.S.ESP_HP2)
                                {
                                    DrawBox(x, y, 10, 10, Color.Black, 1);
                                    DrawFilledBox(x + 1, y + 1, 2, HealthHeight - 1, HealthColor);
                                }
                                #endregion
                                #region Snaplines
                                if (Main.S.ESP_LINE)
                                    DrawLine(Main.MidScreen.X, Main.MidScreen.Y + Main.MidScreen.Y, Player2DPos.X, Player2DPos.Y, drawcolor);
                                #endregion
                            }
                        }

                    }
                }
                //end drawings
                gfx.EndScene();
            }
            #endregion
            #region drawing functions
            void DrawBoxEdge(float x, float y, float width, float height, Color color, float thiccness = 2.0f)
            {
                gfx.DrawRectangleEdges(GetBrushColor(color), x, y, x + width, y + height, thiccness);
            }

            void DrawText(string text, float x, float y, int size, Color color, bool bold = false, bool italic = false)
            {
                if (Tools.InScreenPos(x, y))
                {
                    gfx.DrawText(_graphics.CreateFont("Arial", size, bold, italic), GetBrushColor(color), x, y, text);
                }
            }

            void DrawTextWithOutline(string text, float x, float y, int size, Color color, Color outlinecolor, bool bold = true, bool italic = false)
            {
                DrawText(text, x - 1, y + 1, size, outlinecolor, bold, italic);
                DrawText(text, x + 1, y + 1, size, outlinecolor, bold, italic);
                DrawText(text, x, y, size, color, bold, italic);
            }

            void DrawTextWithBackground(string text, float x, float y, int size, Color color, Color backcolor, bool bold = false, bool italic = false)
            {
                if (Tools.InScreenPos(x, y))
                {
                    gfx.DrawTextWithBackground(_graphics.CreateFont("Arial", size, bold, italic), GetBrushColor(color), GetBrushColor(backcolor), x, y, text);
                }
            }

            void DrawLine(float fromx, float fromy, float tox, float toy, Color color, float thiccness = 2.0f)
            {
                gfx.DrawLine(GetBrushColor(color), fromx, fromy, tox, toy, thiccness);
            }

            void DrawFilledBox(float x, float y, float width, float height, Color color)
            {
                gfx.FillRectangle(GetBrushColor(color), x, y, x + width, y + height);
            }

            void DrawCircle(float x, float y, float radius, Color color, float thiccness = 1)
            {
                gfx.DrawCircle(GetBrushColor(color), x, y, radius, thiccness);
            }

            void DrawCrosshair(CrosshairStyle style, float x, float y, float size, float thiccness, Color color)
            {
                gfx.DrawCrosshair(GetBrushColor(color), x, y, size, thiccness, style);
            }

            void DrawFillOutlineBox(float x, float y, float width, float height, Color color, Color fillcolor, float thiccness = 1.0f)
            {
                gfx.OutlineFillRectangle(GetBrushColor(color), GetBrushColor(fillcolor), x, y, x + width, y + height, thiccness);
            }

            void DrawBox(float x, float y, float width, float height, Color color, float thiccness = 2.0f)
            {
                gfx.DrawRectangle(GetBrushColor(color), x, y, x + width, y + height, thiccness);
            }

            void DrawOutlineBox(float x, float y, float width, float height, Color color, float thiccness = 2.0f)
            {
                gfx.OutlineRectangle(GetBrushColor(Color.FromArgb(0, 0, 0)), GetBrushColor(color), x, y, x + width, y + height, thiccness);
            }

            void DrawRoundedBox(float x, float y, float width, float height, float radius, Color color, float thiccness = 2.0f)
            {
                gfx.DrawRoundedRectangle(GetBrushColor(color), x, y, x + width, y + height, radius, thiccness);
            }
            #endregion
        }
    }
}
