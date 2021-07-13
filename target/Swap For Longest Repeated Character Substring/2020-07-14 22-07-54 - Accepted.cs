/*
Status: Accepted
Runtime: 168 ms
Memory: 26.3 MB
URL: http://leetcode.com/submissions/detail/366746878/
Submission DateTime: July 14, 2020 10:07:54 PM
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
            int swappedPos = -1;
            for(righ = 0; righ < text.Length; righ++)
            {                
                // if been swapped and right is on swapped value
                if(isSwapped && righ == swappingPos)
                {
                    max = Math.Max(max, charCounts.Select(a => a.Value).Max());                    
                    righ++;
                    if(righ >= text.Length)
                        return max;
                    left = righ;
                    charCounts.Clear();                    
                    isSwapped = false;
                    swappingPos = -1; 
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
                        var l = left;
                        var r = righ;
                        // !
                        if(charCounts[text[l]] == charCounts[text[r]] && r + 1 < text.Length && text[r + 1] == text[r])
                            (l, r) = (r, l);
                        //--
                        var (minPos, maxPos) = minmaxPos[text[l]];
                        if(minPos < left) // minPos for swap first
                        {
                            charCounts.Remove(text[r]);
                            charCounts[text[l]] += 1;
                            swappedPos = righ;
                            isSwapped = true;
                        }
                        else if(righ < maxPos) // then maxPos
                        {
                            charCounts.Remove(text[r]);
                            charCounts[text[l]] += 1;
                            swappingPos = maxPos;
                            swappedPos = righ;
                            isSwapped = true;
                        }
                        else
                        {
                            max = Math.Max(max, charCounts.Select(a => a.Value).Max());
                            charCounts.Clear();                            
                            left = righ;
                            charCounts[text[left]] = 1;
                        }
                    }
                    // 2 - has been swapped -> reset swaping context
                    else
                    {
                        max = Math.Max(max, charCounts.Select(a => a.Value).Max());
                        righ = swappedPos;                        
                        left = swappedPos;                        
                        charCounts.Clear();
                        charCounts[text[left]] = 1;
                        isSwapped = false;
                        swappingPos = -1; 
                    }
                }
                else
                    max = Math.Max(max, charCounts.Select(a => a.Value).Max());
            }

            return max;
        }
}
