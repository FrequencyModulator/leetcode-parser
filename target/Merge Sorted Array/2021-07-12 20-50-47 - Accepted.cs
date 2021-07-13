/*
Status: Accepted
Runtime: 424 ms
Memory: 31.2 MB
URL: http://leetcode.com/submissions/detail/521592934/
Submission DateTime: July 12, 2021 8:50:47 PM
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
            if(nums1[m] > nums2[n])
              nums1[p--] = nums1[m--];
            else
              nums1[p--] = nums2[n--];
          }
          else if(m >= 0)
            nums1[p--] = nums1[m--];
          else if(n >= 0)
            nums1[p--] = nums2[n--];                                        
        }
    }
}
