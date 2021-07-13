/*
Status: Accepted
Runtime: 88 ms
Memory: 25.3 MB
URL: http://leetcode.com/submissions/detail/450305868/
Submission DateTime: January 31, 2021 5:16:17 PM
*/
public class Solution {
    public int FindMin(int[] nums) {
        // --- Pivot point is min value - so search in part contains pivot
        // ----
        int left = 0;
        int right = nums.Length - 1;
        int mid = 0;            
        while (left < right)
        {
          mid = left + (right - left) / 2;

          if (nums[mid] > nums[mid + 1])
                return nums[mid + 1];  
          if (mid == 0 || nums[mid - 1] > nums[mid])
                return nums[mid];

            if (nums[right] > nums[mid])
                right = mid;
            else if (nums[right] < nums[mid])
                left = mid;
            else
                right--;
        }
        return nums[mid];
      }
}
