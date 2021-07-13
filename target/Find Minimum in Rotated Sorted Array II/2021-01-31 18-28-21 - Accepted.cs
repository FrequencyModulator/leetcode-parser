/*
Status: Accepted
Runtime: 152 ms
Memory: 25.6 MB
URL: http://leetcode.com/submissions/detail/450330876/
Submission DateTime: January 31, 2021 6:28:21 PM
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
          
          if(nums[mid] > nums[right])  
            left = mid + 1;
          else if (nums[mid] < nums[right])
                right = mid;            
            else
                right--;
        }
        return nums[left];
      }
}
