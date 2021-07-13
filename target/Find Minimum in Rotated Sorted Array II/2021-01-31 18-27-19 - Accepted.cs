/*
Status: Accepted
Runtime: 144 ms
Memory: 26 MB
URL: http://leetcode.com/submissions/detail/450330519/
Submission DateTime: January 31, 2021 6:27:19 PM
*/
public class Solution {
    public int FindMin(int[] nums) {
        /// --- Pivot point is min value - so search in part contains pivot
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
          
          if(nums[mid] > nums[right])  
            left = mid + 1;
          else if (nums[mid] < nums[right])
                right = mid;            
            else
                right--;
        }
        return nums[mid];
      }
}
