/*
Status: Runtime Error
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/521591252/
Submission DateTime: July 12, 2021 8:45:18 PM
*/
public class Solution {
    public void Merge(int[] nums1, int m, int[] nums2, int n) 
    {
        int p = m + n - 1; // insert position (from the end of nums1(result) array)
        // given numbers positions
        n -= 1;
        m -= 1;
      
        while(n >= 0 || m >= 0)
        {
          if(n >= 0 && m >=0)
          {
            if(nums1[n] > nums2[m])
              nums1[p--] = nums1[n--];
            else
              nums1[p--] = nums2[m--];
          }
          else if(n >= 0)
            nums1[p--] = nums1[n--];
          else
            nums1[p--] = nums2[m--];                                        
        }
    }
}
