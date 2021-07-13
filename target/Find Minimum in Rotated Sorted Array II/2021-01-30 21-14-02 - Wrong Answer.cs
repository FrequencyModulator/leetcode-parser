/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/449919480/
Submission DateTime: January 30, 2021 9:14:02 PM
*/
public class Solution {
    public int FindMin(int[] nums) {
        // --- Pivot point is min value - so search in part contains pivot
        // ----
        int left = 0;
        int right = nums.Length - 1;
        int mid = 0;    
        if(nums.Length == 0)
          return -1;
        if(nums.Length == 1)
          return nums[0];
        if(nums.Length == 2)
        {
          if(nums[left] <= nums[right])
            return nums[0];
          else
            return nums[1];
        }          
        if(nums[left] < nums[right])
            return nums[0];
        while(left < right)
        {                              
          if(nums[left] == nums[right])
          {
            left++;            
            if(left == right)
              return nums[left];
            continue;
          }
                                
          mid = left + (right - left) / 2;
          if(nums[mid] > nums[mid + 1])
            return nums[mid + 1];
          if(nums[mid - 1] > nums[mid])
            return nums[mid];
          
          if(nums[left] > nums[mid]) // left part has pivot point - search in left part
            right = mid - 1;
          else // right part has pivot point - search in right
            left = mid + 1;
        }
        return -1;
    }     
}
