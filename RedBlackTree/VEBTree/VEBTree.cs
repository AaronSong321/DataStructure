using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aaron.DataStructure.VEBTree
{
    public class VEBTree
    {
        private uint? Min { get; set; }
        private uint? Max { get; set; }
        private uint U { get; }
        private uint UpperU { get; }
        private uint LowerU { get; }
        private uint UpperK { get; }
        private uint LowerK { get; }

        private VEBTree Summary { get; set; }
        private VEBTree[] Cluster { get; set; }

        public VEBTree(uint k)
        {
            U = Convert.ToUInt32(Math.Pow(2, k));
            if (k % 2 == 0)
            {
                LowerK = UpperK = k / 2;
                UpperU = LowerU = Convert.ToUInt32(Math.Pow(2, k / 2));
            }
            else
            {
                LowerK = k / 2;
                UpperK = LowerK + 1;
                LowerU = Convert.ToUInt32(Math.Pow(2, k / 2));
                UpperU = LowerU << 1;
            }
            if (U == 2)
            {
                Summary = null;
                Cluster = null;
            }
            else
            {
                Summary = new VEBTree(UpperK);
                Cluster = new VEBTree[UpperU];
                for (uint i = 0; i < UpperU; i++)
                    Cluster[i] = new VEBTree(LowerK);
            }
        }

        public uint High(uint x) => x / LowerU;
        public uint Low(uint x) => x - x / LowerU * LowerU;
        public uint Index(uint x, uint y) => x * LowerU + y;

        public uint? Minimum() => Min;
        public uint? Maximum() => Max;
        public bool IsMember(uint x)
        {
            if (x == Min || x == Max) return true;
            else if (U == 2) return false;
            else
                return Cluster[High(x)].IsMember(Low(x));
        }

        public uint? Successor(uint x)
        {
            if (U == 2)
            {
                if (x == 0 && Max == 1)
                    return 1;
                else
                    return null;
            }
            else if (Min != null && x < Min)
            {
                return Min;
            }
            else
            {
                var maxlow = Cluster[High(x)].Maximum();
                if (maxlow != null && Low(x) < maxlow)
                {
                    var offset = Cluster[High(x)].Successor(Low(x));
                    if (offset == null) throw new ArgumentNullException(nameof(offset), "Code is wrong somehow. Offset is null.");
                    return Index(High(x), offset.Value);
                }
                else
                {
                    var succCluster = Summary.Successor(High(x));
                    if (succCluster == null)
                        return null;
                    else
                    {
                        var offset = Cluster[succCluster.Value].Minimum();
                        return Index(succCluster.Value, offset.Value);
                    }
                }
            }
        }

        public uint? Predecessor(uint x)
        {
            if (U == 2)
            {
                if (x == 1 && Min == 0)
                    return 0;
                else return null;
            }
            else if (Max != null && x > Max)
                return Max;
            else
            {
                var minlow = Cluster[High(x)].Minimum();
                if (minlow != null && Low(x) > minlow)
                {
                    var offset = Cluster[High(x)].Predecessor(Low(x));
                    return Index(High(x), offset.Value);
                }
                else
                {
                    var predCluster = Summary.Predecessor(High(x));
                    if (predCluster == null)
                    {
                        if (Min != null && x > Min)
                        {
                            return Min;
                        }
                        else return null;
                    }
                    else
                    {
                        var offset = Cluster[predCluster.Value].Maximum();
                        return Index(predCluster.Value, offset.Value);
                    }
                }
            }
        }

        public void Insert(uint x)
        {
            if (Min == null)
                Min = Max = x;
            else
            {
                if (x < Min)
                {
                    var t = x;
                    x = Min.Value;
                    Min = t;
                }
                if (U > 2)
                {
                    if (Cluster[High(x)].Minimum() == null)
                    {
                        Summary.Insert(High(x));
                        Cluster[High(x)].Min = Low(x);
                        Cluster[High(x)].Max = Low(x);
                    }
                    else
                    {
                        Cluster[High(x)].Insert(Low(x));
                    }
                }
                if (x > Max)
                    Max = x;
            }
        }

        public void Delete(uint x)
        {
            if (Min == Max)
            {
                Min = null;
                Max = null;
            }
            else if (U == 2)
            {
                if (x == 0)
                    Min = 1;
                else Min = 0;
                Max = Min;
            }
            else
            {
                if (x == Min)
                {
                    var firstCluster = Summary.Minimum();
                    x = Index(firstCluster.Value, Cluster[firstCluster.Value].Minimum().Value);
                    Min = x;
                }
                Cluster[High(x)].Delete(Low(x));
                if (Cluster[High(x)].Minimum() == null)
                {
                    Summary.Delete(High(x));
                    if (x == Max)
                    {
                        var summaryMax = Summary.Maximum();
                        if (summaryMax == null)
                            Max = Min;
                        else
                            Max = Index(summaryMax.Value, Cluster[summaryMax.Value].Maximum().Value);
                    }
                }
                else
                {
                    if (x == Max)
                        Max = Index(High(x), Cluster[High(x)].Maximum().Value);
                }
            }
        }
    }
}
