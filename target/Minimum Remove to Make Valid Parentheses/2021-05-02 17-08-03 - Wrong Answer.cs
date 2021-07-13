/*
Status: Wrong Answer
Runtime: N/A
Memory: N/A
URL: http://leetcode.com/submissions/detail/488029185/
Submission DateTime: May 2, 2021 5:08:03 PM
*/
public class Solution {
    public string MinRemoveToMakeValid(string s) {
        var stack = new Stack<int>(); // strore indexes of '('
        var result = new StringBuilder();        
        for (int i = 0; i < s.Length; i++)
        {
          if(stack.Count == 0 && s[i] != '(' && s[i] != ')')
            result.Append(s[i]);
          else
          {
            if(s[i] == '(')
              stack.Push(i);          
            else if(s[i] == ')')
            {
              if(stack.Count > 0)
              {              
                var b = stack.Pop(); // get index of '('
                if(stack.Count == 0) 
                  result.Append(s.Substring(b, i - b + 1)); // get substring from first '(' to the last '('
              }
            }
          }
        }
      
        return result.ToString();
    }
}
