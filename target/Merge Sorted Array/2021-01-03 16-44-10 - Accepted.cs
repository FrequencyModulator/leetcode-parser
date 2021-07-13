/*
Status: Accepted
Runtime: 244 ms
Memory: 31 MB
URL: http://leetcode.com/submissions/detail/438150647/
Submission DateTime: January 3, 2021 4:44:10 PM
*/
public class Solution {
    public void Merge(int[] nums1, int m, int[] nums2, int n) {                        
        int pw = m + n - 1;
        m--;
        n--;
        
        while(m >= 0 || n >= 0)
        {
          if(m < 0)
              nums1[pw--] = nums2[n--];
          else if(n < 0)
              nums1[pw--] = nums1[m--];
          else if (nums1[m] >= nums2[n])                                
              nums1[pw--] = nums1[m--];
          else
              nums1[pw--] = nums2[n--];
        }
      
    }
}
