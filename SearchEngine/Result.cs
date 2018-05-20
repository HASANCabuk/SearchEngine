using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    class Result : IComparable<Result>
    {
        double cosine = 0;
        int i = 0;
        int j = 0;

        public double Cosine { get => cosine; set => cosine = value; }
        public int I { get => i; set => i = value; }
        public int J { get => j; set => j = value; }

        public int CompareTo(Result other)
        {
            if (other.Cosine > Cosine)
            {
                return 1;
            }
            else if (other.Cosine == Cosine)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }
}
