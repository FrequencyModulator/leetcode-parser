/*
Status: Accepted
Runtime: 260 ms
Memory: 45.5 MB
URL: http://leetcode.com/submissions/detail/521006017/
Submission DateTime: July 11, 2021 6:25:46 PM
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
