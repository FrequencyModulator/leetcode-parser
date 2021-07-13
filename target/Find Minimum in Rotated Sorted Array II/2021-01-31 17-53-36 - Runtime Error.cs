/*
Status: Runtime Error
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/450318752/
Submission DateTime: January 31, 2021 5:53:36 PM
*/
public class Solution {
    public int FindMin(int[] nums) {
             int left = 0;
            int right = nums.Length - 1;
            int mid = 0;            
            while (left < right)
            {
              mid = left + (right - left) / 2;

              if (mid == 0 || nums[mid - 1] < nums[mid])
                    return nums[mid - 1];
              if (nums[mid] > nums[mid + 1])
                    return nums[mid + 1];  
                
                if (nums[left] > nums[mid])
                    right = mid;                    
                else if (nums[left] < nums[mid])
                    left = mid;
                else
                    left--;
            }
            return nums[mid];
        }
}
