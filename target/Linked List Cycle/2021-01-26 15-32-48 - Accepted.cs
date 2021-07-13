/*
Status: Accepted
Runtime: 180 ms
Memory: 26.3 MB
URL: http://leetcode.com/submissions/detail/448198829/
Submission DateTime: January 26, 2021 3:32:48 PM
*/
/**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int x) {
 *         val = x;
 *         next = null;
 *     }
 * }
 */
public class Solution {
    public bool HasCycle(ListNode head) {
      // Fast/Slow pointer      
      ListNode slowNode = head, fastNode = head;                        
      while(true)
      {
        slowNode = slowNode?.next;
        if(slowNode == null)
          return false;        

        fastNode = fastNode?.next?.next;        
        if(fastNode == null)
          return false;                        
        
        if(fastNode == slowNode)
          return true;
      }
      
      return false;
    }    
}
