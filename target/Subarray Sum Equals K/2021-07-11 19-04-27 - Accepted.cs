/*
Status: Accepted
Runtime: 256 ms
Memory: 41 MB
URL: http://leetcode.com/submissions/detail/521017687/
Submission DateTime: July 11, 2021 7:04:27 PM
*/
public class Solution {
    public int SubarraySum(int[] nums, int k) 
    {
        var prefixSums = new Dictionary<int, int>(){[0] = 1};
        int sum = 0;
        int count = 0; 
        foreach(int n in nums)
        {
            sum += n;
            if(prefixSums.ContainsKey(sum - k))
                count += prefixSums[sum - k];
            if(prefixSums.ContainsKey(sum))
                prefixSums[sum]++;
            else
                prefixSums[sum] = 1;
        }
        return count;
    }
}
