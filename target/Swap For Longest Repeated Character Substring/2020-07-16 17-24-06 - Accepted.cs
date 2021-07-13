/*
Status: Accepted
Runtime: 136 ms
Memory: 23.3 MB
URL: http://leetcode.com/submissions/detail/367580952/
Submission DateTime: July 16, 2020 5:24:06 PM
*/
public class Solution {
   public int MaxRepOpt1(string text) 
        {
            if (string.IsNullOrEmpty(text))
                return 0;
            
            // get same char groups: aaabaaa -> {"a", 3}, {"b", 1}, {"c", 3}
            var charGroups = new List<(char c, int l)>();
            char curC = '\0';
            int li = 0;            
            foreach(var c in text)
            {
                if(curC != c)
                {                    
                    curC = c;
                    charGroups.Add((curC, 0));
                    li = charGroups.Count - 1;                    
                }
                charGroups[li] = (charGroups[li].c, charGroups[li].l + 1);
            }
            
            // get chars total count: aaabaaa -> {"a", 6}, {"b", 1}
            var charCounts = new Dictionary<char, int>();
            foreach(var c in text)
            {
                if(!charCounts.ContainsKey(c))
                    charCounts.Add(c, 0);
                charCounts[c]++;
            }
            
            int max = 0;
            max = charGroups.Select(s => Math.Min(s.l + 1, charCounts[s.c])).Max();            
            // case when group is splited by one char group
            for(int i = 1; i < charGroups.Count - 1; i++)
            {
                if(charGroups[i].l == 1 && charGroups[i - 1].c == charGroups[i + 1].c)
                    max = Math.Max(max, Math.Min(charGroups[i - 1].l + charGroups[i + 1].l + 1, charCounts[charGroups[i + 1].c]));
            }


            return max;
        }
}
