using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;

namespace Jewelry_Store_e.a.Models
{
    public class Knn
    {
        public static List<int> Distance(int[,] vectors,List<Product> products)
        {
            double min = double.MaxValue;
            List<Tuple<double, int>> distance = new List<Tuple<double, int>>();
            foreach (var vector in products)
            {
                //minimom
                for (int i = 0; i < vectors.GetLength(0); i++)
                {
                    double d = Math.Sqrt(Math.Pow(vectors[i, 0] - (int)vector.Title, 2) + Math.Pow(vectors[i, 1] - (int)vector.Color, 2) + Math.Pow(vectors[i, 2] - (int)vector.price, 2));
                    if (d < min)
                    {
                        min = d;
                    }
                    
                }
                distance.Add(new Tuple<double, int>(min, vector.ID));
            }
            return Majority(distance);
        }
        //if the customer bought the hole store need to check !
        public static List<int> Majority(List<Tuple<double, int>> recomanded)
        {
            if (recomanded.Count > 3)
            {
                return recomanded.OrderByDescending(a => a.Item1).Take(3).Select(t => t.Item2).ToList();
            }
            return recomanded.OrderByDescending(a => a.Item1).Select(t => t.Item2).ToList();
        }
    }
}