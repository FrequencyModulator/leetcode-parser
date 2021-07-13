/*
Status: Accepted
Runtime: 156 ms
Memory: 25.4 MB
URL: http://leetcode.com/submissions/detail/450300470/
Submission DateTime: January 31, 2021 5:01:13 PM
*/
public class Solution {
    public int FindMin(int[] nums) {
        // --- Pivot point is min value - so search in part contains pivot
        // ----
        int left = 0;
        int right = nums.Length - 1;
        int mid = 0;
        if(nums[left] <= nums[right]) // if array is sorted return 0 element 
          return nums[0];
        while(left < right)
        {
          mid = left + (right - left) / 2;
          if(nums[mid] > nums[mid + 1])
            return nums[mid + 1];
          if(nums[mid - 1] > nums[mid])
            return nums[mid];
          if(nums[left] > nums[mid])
            right = mid;
          else if(nums[left] < nums[mid])
            left = mid;
          else
            left++;
        }
        return -1;
    }
}
