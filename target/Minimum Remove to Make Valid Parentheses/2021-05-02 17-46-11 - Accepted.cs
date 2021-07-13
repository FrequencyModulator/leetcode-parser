/*
Status: Accepted
Runtime: 100 ms
Memory: 32.1 MB
URL: http://leetcode.com/submissions/detail/488041030/
Submission DateTime: May 2, 2021 5:46:11 PM
*/
public class Solution {
    public string MinRemoveToMakeValid(string s) {
        var stack = new Stack<int>(); // strore indexes of '('
        var ignoreCharIndexes = new HashSet<int>(); // indexes of ignored breaks       
        for (int i = 0; i < s.Length; i++)
        {          
          if(s[i] == '(')
            stack.Push(i);            
          else if(s[i] == ')')
          {
            if(stack.Count > 0)
              stack.Pop(); // leave '(' if there is a pair ')'
            else
              ignoreCharIndexes.Add(i); // if ')' has no open bracket pair
          }
                                  
        }
      
        while(stack.Count > 0) // copy all indexes from stack to ignore indexes set 
        {
          ignoreCharIndexes.Add(stack.Pop());
        }
        if(ignoreCharIndexes.Count > 0) 
        {     
          var result = new StringBuilder();
          for (int i = 0; i < s.Length; i++)
          {
            if(!ignoreCharIndexes.Contains(i))
              result.Append(s[i]);
          }
          return result.ToString();  
        }
        else
          return s;
        
    }
}
