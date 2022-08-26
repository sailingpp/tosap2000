using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crack
{
    public class Concrete
    {
        public string Name { set; get; }
        public double Fc { set; get; }
        public double Ft { set; get; }
        public double Fck { set; get; }
        public double Ftk { set; get; }
        public double Ec { set; get; }
        public double Vc { set; get; }
        public Concrete(string name, double fc, double ft, double fck, double ftk, double ec, double vc)
        {
            this.Name = name;
            this.Fc = fc;
            this.Ft = ft;
            this.Fck = fck;
            this.Ftk = ftk;
            this.Ec = ec;
            this.Vc = vc;
        }
        public Concrete()
        { }
        public double Cas { set; get; }
        public int N1 { set; get; }
        public int N2 { set; get; }
        public int Sd1 { set; get; }
        public int Sd2 { set; get; }
        public double As
        {
            get { return 3.14 * this.N1 * this.Sd1 * this.Sd1 / 4 + 3.14 * this.N2 * this.Sd2 * this.Sd2 / 4; }
        }
        public double Deq
        {
            get
            {
                return (this.N1 * this.Sd1 * this.Sd1 + this.N2 * this.Sd2 * this.Sd2) / (this.N1 * this.Sd1 + this.N2 * this.Sd2);
            }
        }
        private double cs;
        public double Cs
        {
            set
            {
                if (value < 20)
                {
                    cs = 20;
                }
                else if (value > 65)
                {
                    cs = 65;
                }
                else
                {
                    cs = value;
                }
            }
            get
            {
                return cs;
            }
        }
        public double Fai
        {
            get
            {
                double temp = 1.1 - 0.65 * this.Ftk / (this.Pte * this.Stress);
                if (temp < 0.2)
                {
                    return 0.2;
                }
                else if (temp > 1)
                {
                    return 1.0;
                }
                else
                {
                    return temp;
                }

            }

        }
        public double Ate 
        { 
            get
            {
                return (0.5 * this.B * this.H);
            }
            }
        public double Pte
        {
            get { return this.As / this.Ate; }
        }
        public double Moment { set; get; }
        public double Stress
        { 
            get
            { 
               return this.Moment/(0.87*this.As*(this.H-this.Cas)) ;
            } 
        }
        public double B { set; get; }
        public double H { set; get; }
        public double CalWma(double acr, double es)
        {
            return acr * this.Fai * this.Stress / es * (1.9 * this.Cs + 0.08 * this.Deq / this.Pte);
        }
    }
}
