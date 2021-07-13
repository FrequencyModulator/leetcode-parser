/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/366687578/
Submission DateTime: July 14, 2020 6:46:52 PM
*/
public class Solution {
   public int MaxRepOpt1(string text) 
   {
        if(string.IsNullOrEmpty(text))
            return 0;

        // 1 - get max and min possitions of each symbol in string
        var minmaxPos = new Dictionary<char, (int min, int max)>();
        for(int i = 0; i < text.Length; i++)
        {
            char s = text[i];
            if(!minmaxPos.ContainsKey(s))
                minmaxPos.Add(s, (i, i));
            else
                minmaxPos[s] = (minmaxPos[s].min, i);
        }

        // 2 - sliding window to find longest same char substring 
        Dictionary<char, int> charCounts = new Dictionary<char, int>(2);
        int left = 0, righ = 0, max = 0;
        bool isSwapped = false;
        int swappingPos = -1; 
        for(righ = 0; righ < text.Length; righ++)
        {                
            // if been swapped and right is on swapped value
            if(isSwapped && righ == swappingPos)
            {
                isSwapped = false;
                swappingPos = -1; 
                max = Math.Max(max, righ - left);
                left = righ;
                charCounts.Clear();
            }                
            // increase char count in window
            if (!charCounts.ContainsKey(text[righ]))
                charCounts[text[righ]] = 0;
            charCounts[text[righ]] += 1;

            if(charCounts.Count > 1) // new char is another
            {
                // 1 - if not swap -> swap + memorize swapping context
                if(!isSwapped)
                {
                    var (minPos, maxPos) = minmaxPos[text[left]];
                    if(minPos < left) // minPos for swap first
                    {
                        charCounts.Remove(text[righ]);
                        isSwapped = true;
                    }
                    else if(righ < maxPos) // then maxPos
                    {
                        charCounts.Remove(text[righ]);
                        swappingPos = maxPos;
                        isSwapped = true;
                    }
                    else
                    {
                        max = Math.Max(max, righ - left);                            
                        left = righ - 1;
                        charCounts.Clear();
                        charCounts[text[righ]] = 1;
                    }
                }
                // 2 - has been swapped -> reset swaping context
                else
                {
                    max = Math.Max(max, righ - left);
                    left = righ - 1;
                    charCounts.Clear();
                    charCounts[text[righ]] = 1;
                    isSwapped = false;
                    swappingPos = -1; 
                }
            }
            else
                max = Math.Max(max, righ - left + 1); 
        }

        return max;
    }    
}
