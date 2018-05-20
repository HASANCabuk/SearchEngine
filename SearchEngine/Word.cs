using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngine
{
    class Word : IComparable<Word>
    {

        string name = "";
        int count = 0;
        double tf = 0;
        double tfIdf = 0;
        double nextIdf = 0;
        public Word()
        {
        }

        public Word(string name)
        {
            this.name = name;
        }

        public string Name { get => name; set => name = value; }
        public int Count { get => count; set => count = value; }
        public double TfIdf { get => tfIdf; set => tfIdf = value; }
        public double NextIdf { get => nextIdf; set => nextIdf = value; }
        public double Tf { get => tf; set => tf = value; }

        public int CompareTo(Word other)
        {
            if (count > other.count)
            {
                return 1;
            }
            else if (count == other.count)
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
