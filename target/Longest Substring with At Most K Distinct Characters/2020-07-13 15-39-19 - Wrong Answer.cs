/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/366166000/
Submission DateTime: July 13, 2020 3:39:19 PM
*/
public class Solution {
    public int LengthOfLongestSubstringKDistinct(string s, int k) 
    {
        if((s?.Length ?? 0) < k)
            return 0;

        Dictionary<char, int> distCharCounts = new Dictionary<char, int>();
        int start = 0, end = 0, max = 0;
        for(end = 0; end < s.Length; end++)
        {                
            if(!distCharCounts.ContainsKey(s[end]))
                distCharCounts[s[end]] = 0;
            distCharCounts[s[end]] += 1;

            while(distCharCounts.Count > k && end > start)
            {
                distCharCounts[s[start]] -= 1;
                if(distCharCounts[s[start]] == 0)
                    distCharCounts.Remove(s[start]);
                start++;
            }
            max = Math.Max(max, end - start + 1);
        }

        return max;
    }

}
