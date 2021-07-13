/*
Status: Compile Error
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/450329583/
Submission DateTime: January 31, 2021 6:24:39 PM
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
          
          if(nums[mid] > nums[rigth])  
            left = mid + 1;
          else if (nums[mid] < nums[right])
                right = mid;            
            else
                right--;
        }
        return nums[mid];
      }
}
