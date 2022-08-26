using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tube
{
    public class RecDulTube
    {
     
            public double A { set; get; }
            public double B { set; get; }
            public double B1 { set; get; }
            public double B2 { set; get; }
            public double C { set; get; }
            public double D { set; get; }
            public double H { set; get; }
            public double Height
            {
                get
                {
                    return this.H + this.C / 2.0 + this.D / 2.0;
                }
            }
            public double LeftLength
            {
                get
                {
                    return this.A / 2.0 + this.B1 + this.B / 2.0;
                }
            }
            public double RightLength
            {
                get
                {
                    return this.A / 2.0 + this.B2 + this.B / 2.0;
                }
            }
            public double TopThick
            {
                get { return this.C; }
            }
            public double BotThick
            {
                get { return this.D; }
            }
            public double WallThick
            {
                get { return this.A; }
            }
            public double MiddelWallThick
            {
                get { return this.B; }
            }
            public double Vel { set; get; }
            public RecDulTube(double a, double b, double b1, double b2, double c, double d, double h, double vel, double futuh)
            {
                this.A = a;
                this.B = b;
                this.B1 = b1;
                this.B2 = b2;
                this.C = c;
                this.D = d;
                this.H = h;
                this.Vel = vel;
                this.FuTuHeight = futuh;
            }
            public double FuTuHeight { set; get; }
            public double SoilWallTopLoad
            {
                get
                {
                    return 0.5 * 10 * this.FuTuHeight;
                }
            }
            public double SoilWallBotLoad
            {
                get
                {
                    return 0.5 * 10 * (this.FuTuHeight + this.Height);
                }
            }
            public double SoilPlateTop
            {
                get
                {
                    return 10 * this.FuTuHeight;
                }
            }
            public double VelWallTopLoad
            {
                get
                {
                    return 0.5 * this.Vel;
                }
            }
            public double VelPlateTopLoad
            {
                get
                {
                    return this.Vel;
                }
            }
            public double VelWallBotLoad
            {
                get
                {
                    return 0.5 * this.Vel;
                }
            }
            public double WaterWallTopLoad
            {
                get
                {
                    return 10 * this.FuTuHeight;
                }
            }
            public double WaterPlateTopLoad
            {
                get
                {
                    return 10 * this.FuTuHeight;
                }
            }
            public double WaterWallBotLoad
            {
                get
                {
                    return 10 * (this.FuTuHeight + this.Height);
                }
            }
            public double WaterPlateBotLoad
            {
                get
                {
                    return 10 * (this.FuTuHeight + this.Height);
                }
            }


        }
    }


