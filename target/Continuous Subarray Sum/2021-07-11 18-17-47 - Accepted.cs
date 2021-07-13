/*
Status: Accepted
Runtime: 472 ms
Memory: 45.4 MB
URL: http://leetcode.com/submissions/detail/521003498/
Submission DateTime: July 11, 2021 6:17:47 PM
*/
public class Solution {
    public bool CheckSubarraySum(int[] nums, int k) 
    {
        var sumModSet = new HashSet<int>();
        int sumMod = 0;
        foreach(var n in nums)
        {
            int t = sumMod;
            sumMod += n;
            if (k != 0)
                sumMod %= k;
            
            if(sumModSet.Contains(sumMod))
                return true;
            sumModSet.Add(t);
        }
        return false;
    }
}
