/*
Status: Accepted
Runtime: 220 ms
Memory: 31.1 MB
URL: http://leetcode.com/submissions/detail/438135736/
Submission DateTime: January 3, 2021 4:00:04 PM
*/
public class Solution {
    public void Merge(int[] nums1, int m, int[] nums2, int n) {
        var nums1_copy = new int[m];
        Array.Copy(nums1, nums1_copy, m);
        
        int p1 = 0, p2 = 0, pc = 0;
        while(pc < m && p2 < n)
        {
          if(nums1_copy[pc] <= nums2[p2])
            nums1[p1++] = nums1_copy[pc++];            
          else
            nums1[p1++] = nums2[p2++];
        }
        if(pc < m)
          Array.Copy(nums1_copy, pc, nums1, p1, m - pc);      
        if(p2 < n)
          Array.Copy(nums2, p2, nums1, p1, n - p2);
    }
}
