using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        int[] nums = { 2000, 1010, 3201, 782, 2012, 3489, 1000, 2000, 2000, 20 };
        int target = 5020;
        var result = FindClosestSum(nums, target);
        Console.WriteLine("最接近的组合和: " + result.Item1);
        Console.WriteLine("组合: " + string.Join(", ", result.Item2));
    }

    static Tuple<int, List<int>> FindClosestSum(int[] nums, int target)
    {
        Array.Sort(nums);
        int closestSum = int.MaxValue;
        List<int> closestCombination = new List<int>();

        for (int i = 0; i < nums.Length - 2; i++)
        {
            int left = i + 1;
            int right = nums.Length - 1;

            while (left < right)
            {
                int currentSum = nums[i] + nums[left] + nums[right];

                if (Math.Abs(currentSum - target) < Math.Abs(closestSum - target))
                {
                    closestSum = currentSum;
                    closestCombination = new List<int> { nums[i], nums[left], nums[right] };
                }

                if (currentSum < target)
                {
                    left++;
                }
                else if (currentSum > target)
                {
                    right--;
                }
                else
                {
                    // 如果找到正好等于target的组合，直接返回
                    return Tuple.Create(currentSum, closestCombination);
                }
            }
        }

        return Tuple.Create(closestSum, closestCombination);
    }
}