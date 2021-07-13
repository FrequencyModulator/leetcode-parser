/*
Status: Accepted
Runtime: 112 ms
Memory: 22.9 MB
URL: http://leetcode.com/submissions/detail/366167111/
Submission DateTime: July 13, 2020 3:42:29 PM
*/
public class Solution {
    public int LengthOfLongestSubstringKDistinct(string s, int k)
    {
        if ((s?.Length ?? 0) == 0)
            return 0;
        if (k == 0)
            return 0;

        Dictionary<char, int> distCharCounts = new Dictionary<char, int>();
        int left = 0, righ = 0, max = 0;
        for (righ = 0; righ < s.Length; righ++)
        {
            if (!distCharCounts.ContainsKey(s[righ]))
                distCharCounts[s[righ]] = 0;
            distCharCounts[s[righ]] += 1;

            while (distCharCounts.Count > k && righ > left)
            {
                distCharCounts[s[left]] -= 1;
                if (distCharCounts[s[left]] == 0)
                    distCharCounts.Remove(s[left]);
                left++;
            }
            max = Math.Max(max, righ - left + 1);
        }

        return max;
    }

}
