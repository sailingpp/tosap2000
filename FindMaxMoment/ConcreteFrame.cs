using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMaxMoment
{
    public class ConcreteFrame
    {
        public string Name { set; get; }
        public double Length { set; get; }
        public double B { set; get; }
        public double H { set; get; }
        public string  ConcreteLabel { set; get; }
        public double[] Moment;
        public List<double> MaxMomentList = new List<double>();
        public List<double> MinMomentList = new List<double>();
        public double MaxMoment 
        {
            get 
            {
                return this.MaxMomentList.Max();
            }
        }
        public double MinMoment
        {
            get
            {
                return this.MinMomentList.Max();
            }
        }
       
    }

}
