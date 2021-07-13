/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/520515061/
Submission DateTime: July 10, 2021 8:11:00 PM
*/
public class Solution {
    public bool IsAlienSorted(string[] words, string order) 
    {
        var orderDict = new Dictionary<char, int>(26);
        for(int i = 0; i < order.Length; i++)
        {
          orderDict.Add(order[i], i);
        }        
        for(int p = 0; p < words.Length - 2; p++)
        {
                              
          string w1 = words[p];
          string w2 = words[p + 1];
          int i = 0;
          while(i < w1.Length && i < w2.Length)
          {            
            if(orderDict[w1[i]] > orderDict[w2[i]])
              return false;
            i++;
          }
          if(w1.Length > w2.Length)
            return false;
        }
      
        return true;                          
    }
}
