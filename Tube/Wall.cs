using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tube
{
    public class Wall:Load
    {
        private double _Section_width;

        public double Section_width
        {
            get { return _Section_width; }
            set { _Section_width = value; }
        }
        private double _Section_height;

        public double Section_height
        {
            get { return _Section_height; }
            set { _Section_height = value; }
        }


        public Wall(double section_width, double section_height, double height_plan_top, double height_plan_bot, double height_soil, double height_water, double vel_load)
            : base(height_plan_top, height_plan_bot, height_soil, height_water, vel_load)
        {
            this.Section_height = section_height;
            this.Section_width = section_width;

        }
        public double Wall_length
        {
            get
            {
                return base.Height_Plan_top - base.Height_Plan_bot;
            }
        }
    }
    public class Load
    {
        public double k0 = 0.5;
        private double _Height_Plan_top;//定义顶板标高
        private double _load_vel;

        public double Load_vel
        {
            get { return _load_vel; }
            set { _load_vel = value; }
        }

        public double Height_Plan_top
        {
            get { return _Height_Plan_top; }
            set { _Height_Plan_top = value; }
        }
        private double _Height_Plan_bot;//定义底板标高

        public double Height_Plan_bot
        {
            get { return _Height_Plan_bot; }
            set { _Height_Plan_bot = value; }
        }

        private double _Height_soil;//定义土层标高

        public double Height_soil
        {
            get { return _Height_soil; }
            set { _Height_soil = value; }
        }
        private double _Height_water;//定义水位标高;

        public double Height_water
        {
            get { return _Height_water; }
            set { _Height_water = value; }
        }


        public Load(double height_plan_top, double height_plan_bot, double height_soil, double height_water, double vel_load)
        {
            this.Height_Plan_top = height_plan_top;
            this.Height_Plan_bot = height_plan_bot;
            this.Height_soil = height_soil;
            this.Height_water = height_water;
            this.Load_vel = vel_load;
        }


        public double Water_load_top
        {
            get { return 10 * (Height_water - Height_Plan_top); }
        }
        public double Water_load_bot
        {
            get { return 10 * (Height_water - Height_Plan_bot); }
        }
        public double Soil_load_top
        {
            get
            {
                return k0 * (Height_soil - Height_Plan_top) * 11;
            }
        }
        public double Soil_load_bot
        {
            get
            {
                return k0 * (Height_soil - Height_Plan_bot) * 11;
            }
        }
        public double Vel_load_top
        {
            get
            {
                return k0 * Load_vel;
            }
        }
        public double Vel_load_bot
        {
            get
            {
                return k0 * Load_vel;
            }
        }
    }
}
