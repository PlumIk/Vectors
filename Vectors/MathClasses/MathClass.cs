using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vectors.Elements;
using Vectors.Interfaces;

namespace Vectors.MathClasses
{
    internal class MathClass : IIntersect

    {
        //Проверяем, существует ли плоскость, в которой лежат обе прямые
        public bool HasIntersect(Segment3D one, Segment3D two)
        {

            var matrix = new Matrix(3, 3, new double[] {
                one.End.GetX - one.Start.GetX , one.End.GetY - one.Start.GetY , one.End.GetZ - one.Start.GetZ,
                two.Start.GetX - one.Start.GetX, two.Start.GetY - one.Start.GetY, two.Start.GetZ - one.Start.GetZ,
                two.End.GetX - one.Start.GetX,two.End.GetY - one.Start.GetY,two.End.GetZ - one.Start.GetZ
            });
            //matrix.print();
            if (matrix.CalculateDeterminant() == 0)
            {
                return true;
            }


            return false;
        }

        //Считаем коэффициенты
        public double[] GetAB(Segment3D one, Segment3D two)
        {
            Matrix matrix;
            double[] rep;
            genMat(one, two, out matrix, out rep);
            var det = matrix.CalculateDeterminant();
            if (det== 0)
            {
                return overlay(one, two);
            }
            var ret=new double[2];
            ret[0] = matrix.ReplaceMatrixColumn(0,rep).CalculateDeterminant()/det;
            ret[1] = matrix.ReplaceMatrixColumn(1,rep).CalculateDeterminant()/det;
            if (ret[0]>=0 && ret[0]<=1&& ret[1] >= 0 && ret[1] <= 1)
            {
                return ret;
            }


            return null;
        }

        //Считаем координаты точки
        public double[] CountDot(Segment3D one, double a)
        {

            return new double[]
            {
                a*(one.Start.GetX-one.End.GetX) + one.End.GetX,
                a*(one.Start.GetY-one.End.GetY) + one.End.GetY,
                a*(one.Start.GetZ-one.End.GetZ) + one.End.GetZ

            };
        }

        //Находиться ли точка между 2 другими
        private bool between(double left,double right, double value)
        {
            if (left > right)
            {
                var swap = left;
                left = right; 
                right = swap;
            }
            if(value>=left&& value <= right)
            {
                return true;
            }
            return false;
        }

        //Какую часть составляетотрезок value - left при наложении прямых
        private double part(double left, double right, double value)
        {
            if (left > right)
            {
                var swap = left;
                left = right;
                right = swap;
            }
            return 1 - (value - left) / (right - left);
        }

        //получаем матрицу для составления уравнений такую, что её определитель не 0, если это возможно
        private void genMat(Segment3D one, Segment3D two, out Matrix matrix, out double[] rep)
        {
            matrix = new Matrix(2, 2, new double[] {
                one.Start.GetX-one.End.GetX, two.End.GetX - two.Start.GetX,
                one.Start.GetY-one.End.GetY, two.End.GetY - two.Start.GetY
            });
            if (matrix.CalculateDeterminant() != 0)
            {
                rep = new double[] {
                two.End.GetX-one.End.GetX,
                two.End.GetY-one.End.GetY,
                };
                return;
            }

            matrix = new Matrix(2, 2, new double[] {
                one.Start.GetX-one.End.GetX, two.End.GetX - two.Start.GetX,
                one.Start.GetZ-one.End.GetZ, two.End.GetZ - two.Start.GetZ
            });
            if (matrix.CalculateDeterminant() != 0)
            {
                rep = new double[] {
                two.End.GetX-one.End.GetX,
                two.End.GetZ-one.End.GetZ,
                };
                return;
            }

            matrix = new Matrix(2, 2, new double[] {
                one.Start.GetY-one.End.GetY, two.End.GetY - two.Start.GetY,
                one.Start.GetZ-one.End.GetZ, two.End.GetZ - two.Start.GetZ
            });
            rep = new double[] {
            two.End.GetY-one.End.GetY,
            two.End.GetZ-one.End.GetZ,
            };
            return;
        }

        //Если прямые накладываются,берём в качестве искомой точки начало или конец одно из отрезков в пересечении. Если накладываются не отрезки, а ветора, возвращаеи null
        private double[] overlay(Segment3D one, Segment3D two)
        {
            if (between(two.Start.GetX, two.End.GetX, one.Start.GetX) &&
                   between(two.Start.GetY, two.End.GetY, one.Start.GetY) &&
                   between(two.Start.GetZ, two.End.GetZ, one.Start.GetZ)
                   )
            {
                return new double[] { 1, part(two.Start.GetX, two.End.GetX, one.Start.GetX) };
            }
            else if (between(two.Start.GetX, two.End.GetX, one.End.GetX) &&
                between(two.Start.GetY, two.End.GetY, one.End.GetY) &&
                between(two.Start.GetZ, two.End.GetZ, one.End.GetZ)
                )
            {
                return new double[] { 0, part(two.Start.GetX, two.End.GetX, one.End.GetX) };
            }else if (between(one.Start.GetX, one.End.GetX, two.Start.GetX) &&
                   between(one.Start.GetY, one.End.GetY, two.Start.GetY) &&
                   between(one.Start.GetZ, one.End.GetZ, two.Start.GetZ)
                   )
            {
                return new double[] {  part(one.Start.GetX, one.End.GetX, two.Start.GetX) ,1 };
            }
            else if (between(one.Start.GetX, one.End.GetX, two.End.GetX) &&
                   between(one.Start.GetY, one.End.GetY, two.End.GetY) &&
                   between(one.Start.GetZ, one.End.GetZ, two.End.GetZ)
                   )
            {
                return new double[] {  part(one.Start.GetX, one.End.GetX, two.End.GetX) ,0};
            }

            return null;
            
        }
    }
}
