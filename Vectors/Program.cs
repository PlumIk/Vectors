using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vectors.Elements;
using Vectors.MathClasses;

namespace Vectors
{
    public class Program
    {
        static public void Main(string[] args)
        {
            try
            {
                //проверяем, достаточно ли точек
                if (args.Length != 12)
                {
                    Console.WriteLine("Not enaught points");
                    return;
                }
                double[] pars = new double[12];
                //Конфертируем точки в double и, если хоть она не double, останавливаемся
                for (int i = 0; i < 12; i++)
                {
                    bool isDouble = Double.TryParse(args[i], out pars[i]);
                    if (!isDouble)
                    {
                        Console.WriteLine("Not double");
                        return;
                    }

                }
                //Создаём сегменты и вспомогательный класс
                Segment3D one = new Segment3D(new Vector3D(pars[0], pars[1], pars[2]), new Vector3D(pars[3], pars[4], pars[5]));
                Segment3D two = new Segment3D(new Vector3D(pars[6], pars[7], pars[8]), new Vector3D(pars[9], pars[10], pars[11]));
                var forMath = new MathClass();
                //проверяем, лежат ли линии в одной плоскости
                if (forMath.HasIntersect(one, two))
                {
                    //считаем коэфициенты для точки пересечения. Если null, то прямые не пересекаются
                    var ab = forMath.GetAB(one, two);
                    //считаем точку пересечения
                    if (ab != null)
                    {
                        double[] vec = forMath.CountDot(one, ab[0]);
                        for (int i = 0; i < vec.Length; i++)
                        {
                            Console.Write(vec[i].ToString() + ";");
                        }
                        Console.WriteLine();
                        /*
                        vec = forMath.CountDot(two, ab[1]);
                        for (int i = 0; i < vec.Length; i++)
                        {
                            Console.Write(vec[i].ToString() + ";");
                        } */
                        return;
                    }
                }
                Console.WriteLine("Not intersect");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Some error: " + ex.ToString());
            }
            finally
            {

                Console.ReadKey();
            }
        }
    }
}
