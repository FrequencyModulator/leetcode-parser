/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/449913807/
Submission DateTime: January 30, 2021 8:55:47 PM
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
          if(nums[left] == nums[right])
          {
            left++;
            right--;
          }
          if(left == right)
            return nums[left];
            
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
    
    public int search(int[] nums, int left, int right)
    {
      if(left <= right)
        return nums[0];
      int mid = left + (right - left)/2;
      if(nums[mid] > nums[mid + 1])
        return nums[mid + 1];
      if(nums[mid - 1] >= nums[mid])
        return nums[mid];      
      if(nums[left] > nums[mid])
        return search(nums, left, mid - 1);
      else if(nums[left] < nums[mid] )
        return search(nums, mid + 1, right);
      else 
        return -1;
    }
}
