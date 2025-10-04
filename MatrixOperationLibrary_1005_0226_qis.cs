// 代码生成时间: 2025-10-05 02:26:26
using System;
using System.Linq;

namespace MatrixOperationLibrary
{
    public class Matrix
# 添加错误处理
    {
# 改进用户体验
        public int[][] Values { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public Matrix(int[][] values)
        {
            if (values == null || values.Length == 0)
# NOTE: 重要实现细节
                throw new ArgumentException("Matrix cannot be null or empty.");

            Rows = values.Length;
            Columns = values[0].Length;

            for (int i = 1; i < values.Length; i++)
            {
                if (values[i].Length != Columns)
                    throw new ArgumentException("All rows must have the same number of columns.");
            }

            Values = values;
        }

        // Adds two matrices of the same dimensions.
        public Matrix Add(Matrix other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other), "The other matrix cannot be null.");
# TODO: 优化性能

            if (Rows != other.Rows || Columns != other.Columns)
                throw new ArgumentException("Matrices must be of the same dimensions to add.");
# NOTE: 重要实现细节

            int[][] result = new int[Rows][];
            for (int i = 0; i < Rows; i++)
            {
# TODO: 优化性能
                result[i] = new int[Columns];
                for (int j = 0; j < Columns; j++)
# FIXME: 处理边界情况
                {
                    result[i][j] = Values[i][j] + other.Values[i][j];
                }
            }

            return new Matrix(result);
        }

        // Multiplies two matrices.
        public Matrix Multiply(Matrix other)
        {
# 扩展功能模块
            if (other == null)
# 改进用户体验
                throw new ArgumentNullException(nameof(other), "The other matrix cannot be null.");

            if (Columns != other.Rows)
                throw new ArgumentException("Number of columns in the first matrix must be equal to the number of rows in the second matrix to multiply.");

            int[][] result = new int[Rows][];
            for (int i = 0; i < Rows; i++)
            {
                result[i] = new int[other.Columns];
                for (int j = 0; j < other.Columns; j++)
# 优化算法效率
                {
                    result[i][j] = 0;
                    for (int k = 0; k < Columns; k++)
                    {
                        result[i][j] += Values[i][k] * other.Values[k][j];
                    }
                }
            }

            return new Matrix(result);
        }

        // Transposes a matrix.
        public Matrix Transpose()
        {
            int[][] result = new int[Columns][];
            for (int i = 0; i < Columns; i++)
            {
                result[i] = new int[Rows];
                for (int j = 0; j < Rows; j++)
                {
                    result[i][j] = Values[j][i];
                }
            }
# 添加错误处理

            return new Matrix(result);
# TODO: 优化性能
        }

        // Converts the matrix to a string representation.
# 添加错误处理
        public override string ToString()
        {
            return string.Join("
", Values.Select(row => string.Join(" 	", row)));
        }
    }
}
