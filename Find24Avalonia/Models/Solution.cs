using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find24Avalonia.Models
{
    public class Solution
    {
        public List<string> Find24Solutions(int[] numbers)
        {
            var solutions = new List<string>();
            var permutations = GetUniquePermutations(numbers);
            char[] ops = ['+', '-', '*', '/'];

            foreach (var perm in permutations)
            {
                int a = perm[0], b = perm[1], c = perm[2], d = perm[3];
                foreach (var op1 in ops)
                    foreach (var op2 in ops)
                        foreach (var op3 in ops)
                        {
                            // 结构1: ((a op1 b) op2 c) op3 d
                            try
                            {
                                double result = ApplyOp(ApplyOp(a, b, op1), c, op2);
                                result = ApplyOp(result, d, op3);
                                if (Math.Abs(result - 24) < 1e-6)
                                    solutions.Add($"(({a}{op1}{b}){op2}{c}){op3}{d}");
                            }
                            catch { }

                            // 结构2: (a op1 (b op2 c)) op3 d
                            try
                            {
                                double inner = ApplyOp(b, c, op2);
                                double result = ApplyOp(ApplyOp(a, inner, op1), d, op3);
                                if (Math.Abs(result - 24) < 1e-6)
                                    solutions.Add($"({a}{op1}({b}{op2}{c})){op3}{d}");
                            }
                            catch { }

                            // 结构3: a op1 ((b op2 c) op3 d)
                            try
                            {
                                double inner = ApplyOp(ApplyOp(b, c, op2), d, op3);
                                double result = ApplyOp(a, inner, op1);
                                if (Math.Abs(result - 24) < 1e-6)
                                    solutions.Add($"{a}{op1}(({b}{op2}{c}){op3}{d})");
                            }
                            catch { }

                            // 结构4: a op1 (b op2 (c op3 d))
                            try
                            {
                                double inner = ApplyOp(c, d, op3);
                                double middle = ApplyOp(b, inner, op2);
                                double result = ApplyOp(a, middle, op1);
                                if (Math.Abs(result - 24) < 1e-6)
                                    solutions.Add($"{a}{op1}({b}{op2}({c}{op3}{d}))");
                            }
                            catch { }

                            // 结构5: (a op1 b) op2 (c op3 d)
                            try
                            {
                                double left = ApplyOp(a, b, op1);
                                double right = ApplyOp(c, d, op3);
                                double result = ApplyOp(left, right, op2);
                                if (Math.Abs(result - 24) < 1e-6)
                                    solutions.Add($"({a}{op1}{b}){op2}({c}{op3}{d})");
                            }
                            catch { }
                        }
            }

            return [.. solutions.Distinct()];
        }

        private static double ApplyOp(double a, double b, char op)
        {
            return op switch
            {
                '+' => a + b,
                '-' => a - b,
                '*' => a * b,
                '/' => a / b,
                _ => throw new ArgumentException("无效的运算符")
            };
        }

        private static List<int[]> GetUniquePermutations(int[] nums)
        {
            var result = new List<int[]>();
            var seen = new HashSet<string>();
            Permute(nums, 0, seen, result);
            return result;
        }

        private static void Permute(int[] nums, int start, HashSet<string> seen, List<int[]> result)
        {
            if (start == nums.Length)
            {
                string key = string.Join(",", nums);

                seen.Add(key);
                result.Add((int[])nums.Clone());
                return;
            }

            for (int i = start; i < nums.Length; i++)
            {
                Swap(nums, start, i);
                Permute(nums, start + 1, seen, result);
                Swap(nums, start, i);
            }
        }

        private static void Swap(int[] nums, int i, int j)
        {
            (nums[j], nums[i]) = (nums[i], nums[j]);
        }
    }
}
