/*
Status: Accepted
Runtime: 188 ms
Memory: 26.5 MB
URL: http://leetcode.com/submissions/detail/448200321/
Submission DateTime: January 26, 2021 3:37:41 PM
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
      if(head?.next == null)
        return false;
      
      ListNode slowNode = head, fastNode = head.next;                        
      while(fastNode != slowNode)
      {        
        slowNode = slowNode?.next;
        fastNode = fastNode?.next?.next;        
        if(fastNode == null)
          return false;                        
      }
      
      return true;
    }    
}
