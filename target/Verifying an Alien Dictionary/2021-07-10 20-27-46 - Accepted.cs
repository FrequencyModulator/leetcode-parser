/*
Status: Accepted
Runtime: 92 ms
Memory: 25.8 MB
URL: http://leetcode.com/submissions/detail/520519381/
Submission DateTime: July 10, 2021 8:27:46 PM
*/
public class Solution {
    public bool IsAlienSorted(string[] words, string order) 
    {
        if(words.Length < 2)
          return true;
        var orderDict = new Dictionary<char, int>(26);
        for(int i = 0; i < order.Length; i++)
        {
          orderDict.Add(order[i], i);
        }        
        for(int p = 0; p < words.Length - 1; p++)
        {
                                        
          string w1 = words[p];
          string w2 = words[p + 1];
          int i = 0;
          bool isBroke = false;
          while(i < w1.Length && i < w2.Length)
          {            
            if(orderDict[w1[i]] > orderDict[w2[i]])
              return false;
            if(orderDict[w1[i]] < orderDict[w2[i]])
            {
              isBroke = true;
              break;
            }
            i++;
          }
          if(!isBroke && w1.Length > w2.Length)
            return false;
        }
      
        return true;
    }
}
