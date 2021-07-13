/*
Status: Accepted
Runtime: 164 ms
Memory: 26.5 MB
URL: http://leetcode.com/submissions/detail/448199872/
Submission DateTime: January 26, 2021 3:36:16 PM
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
      if(head == null)
        return false;
      ListNode slowNode = head, fastNode = head.next;                        
      while(fastNode != slowNode)
      {
        if(fastNode == null || slowNode == null)
          return false;                        
        slowNode = slowNode?.next;
        fastNode = fastNode?.next?.next;        
      }
      
      return true;
    }    
}
