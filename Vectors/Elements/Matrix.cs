using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vectors.Elements
{
    public class Matrix
    {
        double[] matrix;
        int N,//Колонок
            M;//строк
        public Matrix(int N, int M, double[] matrix)
        {
            this.matrix = matrix;
            this.N = N;
            this.M = M;
            //Console.WriteLine(matrix.Length);
            if (matrix == null || N*M != matrix.Length)
            {
                throw new ArgumentException("unexprcted matrix");
            }
        }

        
        public double CalculateDeterminant()
        {
            if (N != M) 
            {
                throw new ArgumentException("matrix not square");
            }
            if (N == 2)
            {
                return (matrix[0] * matrix[3]) - (matrix[ 1] * matrix[2]);
            }
            double result = 0;
            for (var j = 0; j < N; j++)
            {
                result += (j % 2 == 0 ? 1 : -1) * matrix[j] *
                    CreateMatrixWithoutColumn(j).
                    CreateMatrixWithoutRow(0).CalculateDeterminant();
            }
            return result;
        }

        protected Matrix CreateMatrixWithoutColumn(int column)
        {
            if (column < 0 || column >= this.N)
            {
                throw new ArgumentException("invalid column index");
            }
            var result = new double[(N - 1) * M];

            for (int i = 0; i < N-1; i++)
            {
                for (int j = 0; j < M ; j++)
                {
                    result[j * (N-1) + i] = i < column ? matrix[j * N  + i] : matrix[j  * N  + i+1];
                }
            }
            Matrix ret = new Matrix(N-1,M,result);

            return ret;
        }

        protected Matrix CreateMatrixWithoutRow(int row)
        {
            if (row < 0 || row >= this.M)
            {
                throw new ArgumentException("invalid row index");
            }
            var result = new double[(M - 1) * N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M-1; j++)
                {
                    result[j *N + i] = j < row ? matrix[j * N + i] : matrix[(j+1) *N + i];
                }
            }
            Matrix ret = new Matrix(N, M-1, result);

            return ret;
        }

        public Matrix ReplaceMatrixColumn(int column, double[] repColumn)
        {
            if (column < 0 || column >= this.N)
            {
                throw new ArgumentException("invalid column index");
            }
            if (repColumn == null || repColumn.Length != M)
            {
                throw new ArgumentException("invalid replaced column");
            }
            var result = new double[N  * M];

            for (int i = 0; i < N ; i++)
            {
                for (int j = 0; j < M; j++)
                {
                    result[j * N  + i] = i != column ? matrix[j * N + i] : repColumn[j];
                }
            }
            Matrix ret = new Matrix(N , M, result);

            return ret;
        }

        public Matrix print()
        {
            Console.Write("###############################\n");
            Console.Write("N="+N.ToString()+", M="+M.ToString()+"\n\n");
            for (int i = 0; i < M; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(matrix[i * N + j].ToString() + ";");
                }
                Console.Write("\n");
            }
            Console.Write("###############################\n");
            return this;
        }

    }
}
